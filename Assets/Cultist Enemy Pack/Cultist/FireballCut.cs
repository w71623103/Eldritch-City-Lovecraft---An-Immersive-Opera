using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCut : MonoBehaviour
{
    [SerializeField] Animator anim;
    private int impactHash;
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
            anim.SetTrigger(impactHash);
        }
    }

    public void endEffect()
    {
        gameObject.SetActive(false);
    }
}
