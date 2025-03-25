using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
	[SerializeField] private float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
    bool onGround = false;
	bool crouch = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(onGround)
        {
        animator.SetFloat("Speed",MathF.Abs(horizontalMove));
        }

		if (Input.GetKey(KeyCode.UpArrow))
		{
            animator.SetBool("IsJumping",true);
			jump = true;
            onGround = false;
		}
		
		// if (Input.GetButtonDown("Crouch"))
		// {
		// 	crouch = true;
		// } 
        // else if (Input.GetButtonUp("Crouch"))
		// {
		// 	crouch = false;
		// }

	}

    public void OnLanding()
    {
        animator.SetBool("IsJumping",false);
        onGround = true;
    }
	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}
}
