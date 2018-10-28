using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    private SpriteRenderer swordSlashSprite;
    private SpriteRenderer middleBossSprite;
    private Vector3 moveVelocity;
    private float speed = 4.0f;
    // Use this for initialization
    void Start ()
    {
        Invoke("DestroySelf", 2.5f);
        swordSlashSprite = GetComponent<SpriteRenderer>();
        middleBossSprite = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        if(middleBossSprite.flipX == true)
        {
            swordSlashSprite.flipX = true;
            moveVelocity = Vector3.left;
        }
        else
        {
            swordSlashSprite.flipX = false;
            moveVelocity = Vector3.right;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += moveVelocity * speed * Time.deltaTime;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
