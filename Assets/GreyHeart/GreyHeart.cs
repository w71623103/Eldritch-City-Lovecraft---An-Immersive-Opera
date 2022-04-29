using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyHeart : NPC
{
    //[SerializeField] private GameObject dialog;
    //[SerializeField] private GameObject buttonIcon;
    [SerializeField] private Animator animator;
    [SerializeField] private bool interacted = false;
    [SerializeField] private GameObject gm;
    [SerializeField] private string eventKey;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        animator = GetComponent<Animator>();
        if (gm.GetComponent<GameManager>().checkEventHappend(eventKey)) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialog.activeSelf == false && interacted)
        {
            animator.SetTrigger("die");
            if (!gm.GetComponent<GameManager>().setEventHappend(eventKey))
                Debug.Log("failed to set event happened");
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(true);

        }
    }*/

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(false);

        }
    }*/

    public override void interact()
    {
        dialog.SetActive(true);
        interacted = true;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeGeneralState(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().dialogState);
    }
}
