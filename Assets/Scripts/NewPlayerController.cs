using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    public float forwardSpeed;

    public float laneDistance = 3;
    private int currentLane = 1;

    public float smoothTime = 30f;
    private float velocity;

    public float acceleration = 0f;
    public float accelerationFactor = 0f;
    public float maxAcceleration;
    public float accelerationSlowDownFactor;

    public float jumpForce = 10f;
    public float gravity = -10f;

    public float distance;

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        characterController.Move(direction * Time.fixedDeltaTime); //Vector driving player movement 

        direction.z = forwardSpeed;  //Default movement forward
        distance = (transform.position.z)/10; //calculating distance for UI

        if (characterController.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            } 
        }else
        {
            direction.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            currentLane--;
            if(currentLane <= 0)
            {
                currentLane = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
            if (currentLane >= 2)
            {
                currentLane = 2;
            }
        }
        // Smooth lane Switching (only resolves x component of vectors)
        Vector3 targetPosition = new Vector3(0,transform.position.y,transform.position.z);
        if (currentLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }

        if(currentLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }


        // Smooth lane Switching (custom LERPing and resolving z component of vector)
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * smoothTime * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                characterController.Move(moveDir);
            else
                characterController.Move(diff);
        }

        characterController.center = characterController.center; //resets the character controller's center so that collisions work properly (since movement is transform based) 
        
        /*
        forwardSpeed += acceleration;
        acceleration += accelerationFactor;
        accelerationFactor -= accelerationSlowDownFactor;
        if (accelerationFactor <= 0) { 
        
            acceleration = 0;
            accelerationFactor = 0;
            accelerationSlowDownFactor = 0;

        }
        */
    }

    private void FixedUpdate()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Death")
        {
            //He is Dead
            direction.y = 0;
            forwardSpeed = 0;
            GameMnger.isGameOver = true;
            
        }
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }
    private void playerMove()
    {

    }

}
