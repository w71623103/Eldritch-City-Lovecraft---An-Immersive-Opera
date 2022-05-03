using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackB : GeneralStateBaseB
{
    public override void EnterState(DeathBringer db)
    {
        db.gStateShow = DeathBringer.generalStates.attack;
        db.comboCount = 1;
        db.enemyAnim.SetInteger("AttackStateLight", 1);
        db.enemyRB.velocity = Vector2.zero;
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
        //pl.canInput = false;
        db.enemyAnim.SetInteger("AttackStateLight", 0);
        db.comboCount = 0;
        db.attackTimer = db.attackCool;
    }
}
