using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackHurtB : GeneralStateBaseB
{
    public override void EnterState(DeathBringer db)
    {
        db.gStateShow = DeathBringer.generalStates.hurt;
        
    }

    public override void Update(DeathBringer db)
    {
        /*if (pl.comboCount == 1 && pl.comboed)
        {
            pl.playerAnim.SetInteger("AttackStateLight", 0);
        }*/
    }

    public override void FixedUpdate(DeathBringer db)
    {
        
    }

    public override void LeaveState(DeathBringer db)
    {
        db.horizontalMovement = db.transform.localScale.x * -1;
    }
}
