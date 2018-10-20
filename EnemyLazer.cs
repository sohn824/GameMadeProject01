using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : MonoBehaviour
{
    private float speed = 3.0f;
    private Vector3 moveVelocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void Start ()
    {
        if (gameObject.transform.parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            moveVelocity = Vector3.left;
        }
        else
        {
            moveVelocity = Vector3.right;
        }
        Invoke("DestroySelf", 2.5f);
    }
	
	void Update ()
    {
        transform.position += moveVelocity * speed * Time.deltaTime;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

