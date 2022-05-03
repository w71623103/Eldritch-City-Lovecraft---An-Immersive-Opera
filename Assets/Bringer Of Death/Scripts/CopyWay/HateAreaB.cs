using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HateAreaB : MonoBehaviour
{
    [SerializeField] GameObject owner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            owner.GetComponent<DeathBringer>().seePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            owner.GetComponent<DeathBringer>().seePlayer = false;
        }
    }
}
