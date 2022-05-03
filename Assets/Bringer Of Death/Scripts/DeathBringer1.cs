using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringer1 : Enemy
{
    //public new GeneralStateMovementE movementState = new GeneralStateMovementBoss();
    // Start is called before the first frame update
    void Start()
    {
        setUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    new void FixedUpdate()
    {
        generalState.FixedUpdate(this);
    }
}
