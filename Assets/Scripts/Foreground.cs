using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreground : MonoBehaviour
{
    [SerializeField] SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            
            spr.color = new Color(255, 255, 255, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            
            spr.color = new Color(255, 255, 255, 1f);
        }
    }
}
