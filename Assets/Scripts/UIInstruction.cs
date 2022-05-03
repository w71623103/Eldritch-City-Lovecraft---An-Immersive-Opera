using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Control which kind of ui instruction is shown
public class UIInstruction : MonoBehaviour
{
    [SerializeField] private Sprite keyboard;
    [SerializeField] private Sprite controller;
    private GameObject gm;
    private SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.GetComponent<GameManager>().controlScheme == "keyboard")
        {
            spr.sprite = keyboard;
        }
        else
        {
            spr.sprite = controller;
        }
    }
}
