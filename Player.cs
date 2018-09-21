using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer playerSprite;
    Rigidbody2D playerRigid;
    Animator playerAnimator;
    private float speed = 3.0f;
    private Vector3 movement;
    private Vector3 moveVelocity;
    private bool isWalk = false;
    private bool isJump = false;
    private float jumpPower = 6.0f;
	// Use this for initialization
	void Start ()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            playerAnimator.SetBool("isJump", false);
            isJump = false;
        }
    }

    private void FixedUpdate()
    {
        process();
    }

    private void process()
    {
        moveVelocity = Vector3.zero;
        if(moveVelocity == Vector3.zero)
        {
            playerAnimator.SetBool("isWalk", false);
            isWalk = false;
        }
        if(Input.GetKey(KeyCode.A))
        {
            playerSprite.flipX = false;
            playerAnimator.SetBool("isWalk", true);
            isWalk = true;
            moveVelocity = Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            playerSprite.flipX = true;
            playerAnimator.SetBool("isWalk", true);
            isWalk = true;
            moveVelocity = Vector3.right;
        }
        if(Input.GetKey(KeyCode.S))
        {
            playerAnimator.SetBool("isCrouch", true);
            moveVelocity = Vector3.zero;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("isCrouch", false);
        }
        if(Input.GetKey(KeyCode.W))
        {
            playerAnimator.SetBool("isUpside", true);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.SetBool("isUpside", false);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump == false)
            {
                playerAnimator.SetBool("isJump", true);
                isJump = true;
                playerRigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }
        }
        
        transform.position += moveVelocity * speed * Time.deltaTime;
    }
}
