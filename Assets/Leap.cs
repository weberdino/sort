using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor.Presets;
using UnityEngine;

public class Leap : MonoBehaviour
{
    public GetNearestEnemy ge;

    private Vector3 endPos;
    private float debug;
    public float jumpHeight = 5f;    // Einstellbare Höhe des Sprungs
    public float jumpSpeed = 1f;     // Geschwindigkeit des Sprungs

    public Vector3 adjustEnd;
    CharacterController controller;
    public GameObject explovfx;
    public Vector3 offset;
    public bool externAcces = false;


    private void Start()
    {
        ge = PlayerManager.instance.enemyTracker;
    }

    public void LeapButton()
    {
        // Aktualisiert die Endposition beim Start des Sprungs
        endPos = ge.closestEnemy.transform.position + adjustEnd;
        debug = Vector3.Distance(transform.position, endPos);  // Setzt die Distanz korrekt
        StartCoroutine(LeapMotion(PlayerManager.instance.player));
        controller = PlayerManager.instance.player.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        bool pressed = AbilityManager.instance.buttonPressed;
        if (pressed && !externAcces) //&& proj == null
        {
            //if (cd < 0)
            {
                LeapButton();
                controller.enabled = false;
                //cd = maxCd;
            }
        }
    }

    private IEnumerator LeapMotion(GameObject player)
    {
        Vector3 startPos = player.transform.position;
        float duration = debug / 20 / jumpSpeed;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Parabelbewegung mit Sinuskurve für die Höhe
            player.transform.position = Vector3.Lerp(startPos, endPos, t)
                                        + new Vector3(0, jumpHeight * Mathf.Sin(t * Mathf.PI), 0);

            yield return null;
        }

        // Endposition sicherstellen
        var obj = Instantiate(explovfx, PlayerManager.instance.player.transform.position, Quaternion.identity);
        obj.transform.position += offset;
        obj.transform.localScale = obj.transform.localScale * 1.5f;
        Destroy(obj, .75f);

        player.transform.position = endPos;
        controller.enabled = true;
        yield return null;
    }

    private void OnDrawGizmos()
    {
        if (ge == null || ge.closestEnemy == null) return;

        Gizmos.color = Color.red;
        Vector3 startPos = transform.position;
        Vector3 endPos = ge.closestEnemy.transform.position + adjustEnd;

        int segments = 20;
        Vector3 previousPoint = startPos;

        for (int i = 1; i <= segments; i++)
        {
            float t = (float)i / segments;
            Vector3 pointOnCurve = Vector3.Lerp(startPos, endPos, t)
                                   + new Vector3(0, jumpHeight * Mathf.Sin(t * Mathf.PI), 0);

            Gizmos.DrawLine(previousPoint, pointOnCurve);
            previousPoint = pointOnCurve;
        }

        // Zielposition markieren
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(endPos, 0.2f);
    }
}
