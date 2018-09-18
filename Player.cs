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
        }
        if(Input.GetKey(KeyCode.A))
        {
            playerSprite.flipX = false;
            playerAnimator.SetBool("isWalk", true);
            moveVelocity = Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            playerSprite.flipX = true;
            playerAnimator.SetBool("isWalk", true);
            moveVelocity = Vector3.right;
        }
        
        transform.position += moveVelocity * speed * Time.deltaTime;
    }
}
