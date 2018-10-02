using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private Vector3 moveVelocity;
    private float speed = 8.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            destroySelf();
        }
    }
    // Use this for initialization
    void Start ()
    {
        playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (playerSprite.flipX == false)
        {
            moveVelocity = Vector3.left;
        }
        else
        {
            moveVelocity = Vector3.right;
        }
        Invoke("destroySelf", 2.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += moveVelocity * speed * Time.deltaTime;
    }

    void destroySelf()
    {
        Destroy(gameObject);
    }
}
