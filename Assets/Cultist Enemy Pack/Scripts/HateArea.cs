using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HateArea : MonoBehaviour
{
    [SerializeField] GameObject owner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            owner.GetComponent<Enemy>().seePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            owner.GetComponent<Enemy>().seePlayer = false;
        }
    }
}
