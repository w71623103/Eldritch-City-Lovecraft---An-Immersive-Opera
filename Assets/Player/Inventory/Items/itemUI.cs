using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private TMP_Text itemNum;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject coolDown;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        inventory = GameObject.Find("Inventory");
    }

    void Update()
    {
        sp.sprite = inventory.GetComponent<Inventory>().itemList[inventory.GetComponent<Inventory>().currentItem].icon;
        itemNum.text = inventory.GetComponent<Inventory>().itemList[inventory.GetComponent<Inventory>().currentItem].heldNum.ToString();
        var currentItem = GameObject.Find(inventory.GetComponent<Inventory>().itemList[inventory.GetComponent<Inventory>().currentItem].itemName);
        coolDown.transform.localScale = new Vector3(coolDown.transform.localScale.x, currentItem.GetComponent<itemEffect>().cdPercent, coolDown.transform.localScale.z);
    }
}
