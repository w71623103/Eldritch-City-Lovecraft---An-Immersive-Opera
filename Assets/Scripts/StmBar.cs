using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StmBar : MonoBehaviour
{
    private float defaultXScale;

    public float percent;

    // Start is called before the first frame update
    void Start()
    {
        defaultXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(percent * defaultXScale, transform.localScale.y, transform.localScale.z);
    }
}
