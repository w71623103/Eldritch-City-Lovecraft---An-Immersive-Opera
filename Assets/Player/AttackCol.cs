using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    public int AttackDmg;
    public float knock;
    private void Update()
    {
        //if(GetComponent<BoxCollider2D>().enabled == false)
            //transform.parent.GetComponent<Animator>().speed = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHurt(AttackDmg, transform.position.x, knock);
            //collision.GetComponent<Animator>().speed = 0.5f;
            //transform.parent.GetComponent<Animator>().speed = 0.5f;
        }*/
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                collision.GetComponent<Enemy>().OnHurt(AttackDmg, transform.position.x, knock);
                break;
            case "DeathBringer":
                collision.GetComponent<DeathBringer>().OnHurt(AttackDmg, transform.position.x, knock);
                break;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //transform.parent.GetComponent<Animator>().speed = 1f;
        }
    }*/

    /*private void OnDisable()
    {
        transform.parent.GetComponent<Animator>().speed = 0.1f;
    }*/

}
