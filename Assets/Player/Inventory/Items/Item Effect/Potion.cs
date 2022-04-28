using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : itemEffect
{
    public int healEffect;

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
        var pl = GameObject.Find("Player").GetComponent<Player>();
        if (timer <= 0f && item.heldNum > 0 && pl.Hp < pl.maxHp)
        {
            //Debug.Log("Used " + item.name);
            pl.transform.Find("heal").GetComponent<ParticleSystem>().Play();
            if (pl.Hp + healEffect <= pl.maxHp) pl.Hp += healEffect;
            else pl.Hp = pl.maxHp;
            timer = maxCD;
            item.heldNum--;
        }
        
    }
}
