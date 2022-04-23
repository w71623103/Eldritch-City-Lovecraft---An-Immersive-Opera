using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject condition;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool started = false;
    /*[SerializeField] private AudioClip se;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(condition.GetComponent<DialogSystem>().diaCount == 0 && condition.GetComponent<DialogSystem>().count == 8)
        {
            background.GetComponent<AudioSource>().Stop();
            audioSource.Play();
            started = true;
        }

        if(condition.activeSelf == false && started)
        {
            audioSource.Stop();
            background.GetComponent<AudioSource>().Play();
            started = false;
        }
    }
}
