using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables for jumping
    public float gravity;
    public Vector3 velocity;
    public float acceleration;
    public float accelerationFactor;
    public float distance;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;
    public float smoothTimeForLaneSwitching = 0.3f;
    //public GameObject[] lanes;
   // [SerializeField] private int currentLane = 1;
  //  private Vector3 laneVelocity = Vector3.zero;

    //holding jump
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundThreshold = 1; //jump buffering


    void Start()
    {

    }

    void Update()
    {
        Vector3 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }


            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            //RayCast

            /*
            Vector3 rayOrigin = new Vector3(pos.x, pos.y,pos.z + 0.7f);
            Vector3 rayDir = Vector3.up * Time.deltaTime;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit hit = Physics.Raycast(rayOrigin, rayDir,rayDistance);
           
            if(hit.collider != null)
            {

            }*/

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }


        transform.position = pos;
        //velocity.z += acceleration;
        //acceleration += accelerationFactor;
        distance += 0.3f;
    }

}
