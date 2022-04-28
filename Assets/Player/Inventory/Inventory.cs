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
            if(newitem.heldNum < newitem.maxHeldNum) newitem.heldNum++;
        }else
        {
            itemList.Add(newitem);
        }
    }

    public void nextItem()
    {
        if (currentItem < itemList.Count - 1) currentItem++;
        else currentItem = 0;
    }

    public void prevItem()
    {
        if (currentItem > 1) currentItem--;
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