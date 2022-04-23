using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnim : MonoBehaviour
{
    [SerializeField] private float timer = 0f;
    private float maxTimer = 5f;
    private Animator anim;
    private int currentID = 0;
    private int maxVarNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > maxTimer)
        {
            timer = 0f;
            maxTimer = Random.Range(3f,6f);
            if(currentID < maxVarNum) currentID++;
            else currentID = 0;
            anim.SetInteger("state", currentID);
        }    
    }
}
