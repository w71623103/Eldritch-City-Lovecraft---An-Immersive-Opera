using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject buttonIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(false);

        }
    }

    public void interact()
    {
        dialog.SetActive(true);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeGeneralState(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().dialogState);
    }
}
