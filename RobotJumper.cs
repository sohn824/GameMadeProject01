using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJumper : MonoBehaviour
{
    BoxCollider2D colliderForJump;
    GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            transform.parent.GetComponent<Robot>().SetState(Robot.RobotState.Jump);
        }
    }
    void Start()
    {
        colliderForJump = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            colliderForJump.offset = new Vector2(-0.17f, 0.1f);
        }
        else
        {
            colliderForJump.offset = new Vector2(0.17f, 0.1f);
        }
    }
}
