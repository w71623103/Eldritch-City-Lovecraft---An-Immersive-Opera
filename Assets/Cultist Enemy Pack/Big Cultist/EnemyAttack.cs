using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Script controlling enemy attack.
public class EnemyAttack : MonoBehaviour
{
    public int AttackDmg;
    public int knock;
    // Start is called before the first frame update
    void Start() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().OnHurt(AttackDmg, transform.position.x, knock);
            gameObject.SetActive(false);
        }
    }

    public void endEffect()
    {
        gameObject.SetActive(false);
    }
}
