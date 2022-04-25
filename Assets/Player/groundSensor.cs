using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundSensor : MonoBehaviour
{
    [SerializeField] Player pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            pl.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pl.isGrounded = false;
        }
    }
}
