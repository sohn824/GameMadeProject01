using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator bombAnimator;
	void Start ()
    {
        bombAnimator = GetComponent<Animator>();
        Invoke("explosion", 2.0f);
	}

    private void explosion()
    {
        bombAnimator.SetBool("isExplosion", true);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
