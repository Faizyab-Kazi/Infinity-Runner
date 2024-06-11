using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private CharacterController characterController;
    
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;
    
    public LayerMask enemyLayers;
    //public Transform hitbox;
    public float attackRange;


    public float animationDampening = .1f;

    public float laneDistance = 3;
    private int currentLane = 1;

    public float smoothTime = 30f;
    private float velocity;

    public float acceleration = 0f;
    public float jumpAccelerationDampening;
    

    public float jumpForce = 10f;
    public float maxJumpForce;
    public float gravity = -10f;

    public float distance;
    public Animator animator;
    public bool isAlive=true;
    public float highScore;

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator.SetBool("isGrounded", true);
        highScore = PlayerPrefs.GetFloat("highscore",0);
    }
    private void Awake()
    {
        highScore = PlayerPrefs.GetFloat("highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {

        characterController.Move(direction); //Vector driving player movement 

        //Acceleration
        if ((forwardSpeed < maxSpeed) && (isAlive)){
            forwardSpeed += acceleration * Time.deltaTime;
        }

        float speedPercent = forwardSpeed/maxSpeed;
        animator.SetFloat("PlayerVelocity",speedPercent,Time.deltaTime,animationDampening);
        
        direction.z = forwardSpeed * Time.deltaTime;  //Resultant driving forward movement
        distance = (transform.position.z)/10; //calculating distance for UI

        if (isAlive) { 
        if (characterController.isGrounded)
        {
            animator.SetBool("isGrounded", true);

                direction.y = (float)-0.01; //REFER TO HERE FOR Y DIR THING
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                animator.SetBool("isGrounded", false);
                
            } 
        }else
        {
            direction.y -= (gravity * Time.deltaTime);
            animator.SetBool("isGrounded", false);
                
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            currentLane--;
            if(currentLane <= 0)
            {
                currentLane = 0;
            }
                if (characterController.isGrounded) { animator.Play("SideStepLeft"); }
                
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
            if (currentLane >= 2)
            {
                currentLane = 2;
            }
                if (characterController.isGrounded) { animator.Play("SideStepRight"); }
        }

            
            
            if (Input.GetKeyDown(KeyCode.S))
            {
               
                if (characterController.isGrounded){
                    Attack();
                    
                }
            } 
        }



        // lane Switching (only resolves x component of vectors)
        Vector3 targetPosition = new Vector3(0,transform.position.y,transform.position.z);
        if (currentLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }

        if(currentLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }


        // Smoothing lane Switching (custom LERPing and resolving z component of vector)
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * smoothTime * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                characterController.Move(moveDir);
            else
                characterController.Move(diff);
        }

        characterController.center = characterController.center; //resets the character controller's center after every single transform so that collisions work properly

        
        //Jumpforce adjusting
        if (jumpForce < maxJumpForce)
        {
            jumpForce += (forwardSpeed * Time.deltaTime)/jumpAccelerationDampening;
        }else if (jumpForce > maxJumpForce)
        {

            jumpForce = maxJumpForce;
        }



        


        
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Death")
        {
            //He is Dead
            
            direction.y = 0;
            forwardSpeed = 0;
            GameMnger.isGameOver = true;
            isAlive = false;
            if (distance > PlayerPrefs.GetFloat("highscore",0))
            {
                highScore = distance;
                PlayerPrefs.SetFloat("highscore", distance);
                
            }
            PlayerPrefs.Save();
            


        }
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void Attack() {

        animator.SetTrigger("IsHitting");
        //Collider[] hitEnemies = Physics.OverlapSphere(hitbox.position, attackRange, enemyLayers);
        
    }

    /*private void OnDrawGizmosSelected()
    {
        if (hitbox == null) {
            return;
        }
        
        Gizmos.DrawSphere(hitbox.position,attackRange);
    }*/


    private void playerMove()
    {

    }

}
