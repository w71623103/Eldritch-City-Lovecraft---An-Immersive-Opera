using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Player pl;
    public List<Item> itemList = new List<Item>();
    public Dictionary<string, bool> itemHoldList = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        pl = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item newitem)
    {
        if(itemHoldList[newitem.itemName])
        {
            //itemList.Find(newitem).heldNum++;
        }
    }
}