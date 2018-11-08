using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField]
    GameObject[] itemArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            int seed = Random.Range(1, 4);
            switch (seed)
            {
                case 1:
                    Instantiate(itemArray[0], transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(itemArray[1], transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(itemArray[2], transform.position, Quaternion.identity);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
