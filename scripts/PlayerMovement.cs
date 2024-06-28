using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Vector3 PlayerVelocity;
    public float speed = 5f;
    bool isGrounded;
    bool isSprinting=false;
    bool isCrouching=false;
    float crouchTimer = 1f;
    bool lerpCrouch=false;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = controller.isGrounded;
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if(isCrouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }
            if(p>1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 MoveDir = Vector3.zero;
        MoveDir.x = input.x;
        MoveDir.z = input.y;
        controller.Move(transform.TransformDirection(MoveDir*speed*Time.deltaTime));
        PlayerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && PlayerVelocity.y < 0)
            PlayerVelocity.y = -2f;
        controller.Move(PlayerVelocity*Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
         PlayerVelocity.y = Mathf.Sqrt(jumpHeight *-3.0f * gravity);
        }
    }
    public void Sprint()
    {
       isSprinting = !isSprinting;
        if (isSprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    } public void Crouch()
    {
       isCrouching = !isCrouching;
       crouchTimer = 0;
       lerpCrouch = true;
    }
   
}
