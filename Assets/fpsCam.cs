using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 300f; // Maus-Sensitivität
    public Transform playerBody; // Das Objekt, das sich mit der Kamera drehen soll

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Sperrt den Mauszeiger im Spiel
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Begrenzung der Vertikalrotation

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
