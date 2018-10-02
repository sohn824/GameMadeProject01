using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummySight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.transform.parent.GetComponent<Mummy>().SetState(Mummy.EnemyState.Walk);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.transform.parent.GetComponent<Mummy>().SetState(Mummy.EnemyState.Idle);
        }
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
