using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool Flag = false;
    [SerializeField]
    private GameObject mainCamera;
    FollowCamera followCamera;

    void Start()
    {
        followCamera = mainCamera.GetComponent<FollowCamera>();
    }

    void Update ()
    {
		if(Flag == true)
        {
            transform.position += Vector3.down * 1.0f * Time.deltaTime;
            followCamera.enabled = false;
            mainCamera.transform.position = new Vector3(111.0f, 2.5f, -10f);

        }
        if(transform.position.y < -2.5f)
        {
            followCamera.enabled = true;
            Destroy(gameObject);
        }
	}
}
