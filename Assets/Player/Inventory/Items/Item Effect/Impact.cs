using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : itemEffect
{
    [SerializeField] private float timer = 0f;
    [SerializeField] private float maxCD = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f) timer -= Time.deltaTime;
        cdPercent = timer / maxCD;
    }

    public override void use(Item item)
    {
        Debug.Log("222");
        var pl = GameObject.Find("Player").GetComponent<Player>();
        if (timer <= 0f && item.heldNum > 0)
        {
            Debug.Log("333");
            //pl call impact function
            pl.OnImpact();
            timer = maxCD;
            //item.heldNum--;
        }
        
    }
}
