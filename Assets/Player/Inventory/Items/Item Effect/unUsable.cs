using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unUsable : itemEffect
{

    public override void  use(Item item)
    {
        Debug.Log("This Item " + item.itemName + " is not usable");
    }
}
