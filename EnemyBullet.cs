using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 moveVelocity;
    private float speed = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    void Start ()
    {
        Invoke("DestroySelf", 2.5f);
        if(transform.parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            moveVelocity = Vector3.left;
        }
        else
        {
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
