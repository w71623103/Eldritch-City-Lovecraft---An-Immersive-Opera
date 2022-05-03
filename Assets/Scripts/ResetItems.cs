using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItems : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in items)
        {
            item.heldNum = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
