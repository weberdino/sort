using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class movementController : MonoBehaviour
{
    protected Joystick joystick;
    // CharacterStats myStats;
    
    float rotationSpeed;
    private float ySpeed;

    public CharacterController charController;
    public Vector3 moveDir2;
    public float speed;

    float horizontalInput;
    float verticalInput;

    public Transform cam;

    public AnimationHandleNew animation;
    bool wasWalking;

    GameObject ability;
    void Start()
    {
        charController = GetComponent<CharacterController>();        
    }
    public void Assign()
    {
        ability = PlayerManager.instance.player.GetComponent<PlayerStats>().ability;
    }

    void FixedUpdate()
    {
        float movementAmount = moveDir2.magnitude;
        //animator.SetFloat("Speed", movementAmount);
        walkFunc();

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rotationSpeed = 720f;

        moveDir2 = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 movementDirection = moveDir2;
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        charController.SimpleMove(movementDirection * magnitude);

        ySpeed += Physics.gravity.y * Time.deltaTime;
        Vector3 velocity = movementDirection * magnitude;
        velocity.y += ySpeed ;

        charController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)  //rotation to walk direction
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void walkFunc()
    {
        bool isWalkingNow = moveDir2.magnitude > 0.01f;
        if (isWalkingNow)
        {
            onWalk();
        }
        else
        {
            onStop();
        }
    }

    void onWalk()
    {
        if (!wasWalking)
        {
            animation.Play();
            wasWalking = true;
        }
        
    }

    void onStop()
    {
       
        if (wasWalking)
        {
            animation.Idle();
            wasWalking = false;
        }
    }

    public void BlockforSeconds(float seconds)
    {
        this.enabled = false;
        Invoke("over", seconds);
        charController.SimpleMove(Vector3.zero);
    }

    public void Button()
    {
       transform.position = PlayerManager.instance.respawnPoint.transform.position;
    }
}



