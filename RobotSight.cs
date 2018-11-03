using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (transform.parent.GetComponent<Robot>().isJumping == false)
            {
                transform.parent.GetComponent<Robot>().SetState(Robot.RobotState.Jump);
            }
        }
        if(collision.tag == "Player")
        {
            transform.parent.GetComponent<Robot>().SetState(Robot.RobotState.Shoot);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            transform.parent.GetComponent<Robot>().SetState(Robot.RobotState.Trace);
        }
    }
}
