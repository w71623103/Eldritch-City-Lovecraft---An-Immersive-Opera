using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //[SerializeField] private Player pl;
    public List<Item> itemList = new List<Item>();
    public int currentItem;
    //public Dictionary<string, bool> itemCheckList = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        //pl = GetComponent<Player>();
        DontDestroyOnLoad(gameObject);
        currentItem = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item newitem)
    {
        if(itemList.Contains(newitem))
        {
            Debug.Log("increase num");
            if (newitem.heldNum + newitem.numWhenCollect <= newitem.maxHeldNum) newitem.heldNum += newitem.numWhenCollect;
            else newitem.heldNum = newitem.maxHeldNum;
        }else
        {
            Debug.Log("add new");
            itemList.Add(newitem);
            newitem.heldNum += newitem.numWhenCollect;
        }
    }

    public void nextItem()
    {
        if (currentItem < itemList.Count - 1) currentItem++;
        else currentItem = 0;
    }

    public void prevItem()
    {
        if (currentItem > 0) currentItem--;
        else currentItem = itemList.Count - 1;
    }

    public void useCurrentItem()
    {
        if(itemList[currentItem] != null)
        {
            Debug.Log("111");
            GameObject.Find(itemList[currentItem].itemName).GetComponent<itemEffect>().use(itemList[currentItem]);
        }
        else
        {
            Debug.Log("CurrentItem is null");
        }
    }
}