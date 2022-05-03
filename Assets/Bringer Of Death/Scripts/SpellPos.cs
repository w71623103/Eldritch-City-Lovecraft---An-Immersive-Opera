using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPos : MonoBehaviour
{
    [SerializeField] private GameObject pl;
    [SerializeField] private Vector3 plPos;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player");
    }

    //Animation used function, follow the player
    public void lockPlayer()
    {
        if (pl != null)
        {
            plPos = pl.transform.position;
        }
        transform.position = plPos;
    }
}
