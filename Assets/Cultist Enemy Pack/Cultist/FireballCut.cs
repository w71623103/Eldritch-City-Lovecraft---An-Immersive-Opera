using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCut : MonoBehaviour
{
    [SerializeField] Animator anim;
    private int impactHash;
    public int AttackDmg;
    public int knock;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        impactHash = Animator.StringToHash("Impact");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger(impactHash);
            collision.gameObject.GetComponent<Player>().OnHurt(AttackDmg, transform.position.x, knock);
        }
    }

    public void endEffect()
    {
        gameObject.SetActive(false);
    }
}
