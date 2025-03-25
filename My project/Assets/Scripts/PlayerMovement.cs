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
    void Update()
    {
        animator.SetFloat("Speed", MathF.Abs(horizontalMove));

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            animator.SetBool("IsJumping", true);
            jump = true;
            onGround = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        onGround = true;
    }

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {
            UIManager.Instance.ShowGameOverUI();
        }
    }
}
