using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
	void Start ()
    {
        Invoke("destroySelf", 0.5f);
	}
	
    void destroySelf()
    {
        Destroy(gameObject);
    }
}
