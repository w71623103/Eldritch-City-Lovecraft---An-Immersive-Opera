using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class itemEffect : MonoBehaviour
{
    public float cdPercent = 0f;
    public abstract void use(Item item);
}
