using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [Header("Dialog")]
    [SerializeField] private List<Sprite[]> dialogs;
    /*[SerializeField] private Sprite[] dialog1;
    [SerializeField] private Sprite[] dialog2;*/
    public int count = 0;
    public int diaCount = 0;
    [SerializeField] private int maxDialog = 2;
    private void Awake()
    {
        //count = 0;
        //diaCount = 0;
    }
    private void OnEnable()
    {
        if(diaCount > 0)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeGeneralState(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().dialogState);
    }
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeGeneralState(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().dialogState);
        dialogs = new List<Sprite[]>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        for(int i = 0; i < maxDialog; i++)
        {
            dialogs.Add(transform.Find("dialog" + (i+1)).GetComponent<DialogHolder>().dialog);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = dialogs[diaCount][count];
    }

    public void next()
    {
        if(dialogs != null && (count < dialogs[diaCount].Length-1))
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeGeneralState(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().dialogState);

            count++;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeGeneralState(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().movementState);
            if(diaCount < dialogs.Count-1) diaCount++;
            count = 0;
            gameObject.SetActive(false);
        }
    }
}
