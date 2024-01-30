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

    public float jumpForce = 10f;
    public float gravity = -10f;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        characterController.Move(direction * Time.fixedDeltaTime);

        direction.z = forwardSpeed; 
        distance = transform.position.z;

        if(characterController.isGrounded)
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

        
        
        //Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        
        Vector3 targetPosition = new Vector3(0,transform.position.y,transform.position.z);
        if (currentLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }

        if(currentLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }


        //Vector3 tempPos = transform.position;
        //tempPos.x = Mathf.Lerp(transform.position.x, targetPosition.x, smoothTime);
        //transform.position = tempPos;

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * smoothTime * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                characterController.Move(moveDir);
            else
                characterController.Move(diff);
        }


        //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        characterController.center = characterController.center;
    }

    private void FixedUpdate()
    {
        
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Death")
        {
            //He is Dead
            forwardSpeed = 0;
            GameMnger.isGameOver = true;
        }
    }
}
