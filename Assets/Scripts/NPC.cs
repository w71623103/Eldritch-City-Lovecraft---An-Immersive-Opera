using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    [SerializeField] protected GameObject dialog;
    [SerializeField] protected GameObject buttonIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(dialog.activeSelf == false)
        {
            Debug.Log("die");
        }*/
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(true);

        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(false);

        }
    }

    public abstract void interact();

    /*public abstract void interact()
    {
        dialog.SetActive(true);
    }*/
}
