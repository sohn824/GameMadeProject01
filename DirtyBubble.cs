using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyBubble : MonoBehaviour
{
    Rigidbody2D bubbleRigid;
    Animator bubbleAnimator;
    Vector3 direction;
    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision) //이건 Trigger 체크용 BoxCollider
    {
        if(collision.tag == "Ground")
        {
            bubbleRigid.AddForce(Vector3.up * 6.0f, ForceMode2D.Impulse);
        }
        if(collision.tag == "Bullet" || collision.tag == "Player")
        {
            bubbleAnimator.SetBool("isPop", true);
        }

    }
    // Use this for initialization
    void Start ()
    {
        bubbleRigid = GetComponent<Rigidbody2D>();
        bubbleAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        if(player.transform.position.x < transform.position.x)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }
        Invoke("DestroySelf", 3.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += direction * 4.0f * Time.deltaTime;
    }

    public void DestroySelf() //Pop 애니메이션 이벤트 용
    {
        Destroy(gameObject);
    }
}
