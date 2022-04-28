using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackHurtE : GeneralStateBaseE
{
    public override void EnterState(Enemy em)
    {
        em.gStateShow = Enemy.generalStates.hurt;
        
    }

    public override void Update(Enemy em)
    {
        /*if (pl.comboCount == 1 && pl.comboed)
        {
            pl.playerAnim.SetInteger("AttackStateLight", 0);
        }*/
    }

    public override void FixedUpdate(Enemy em)
    {
        
    }

    public override void LeaveState(Enemy em)
    {
        em.horizontalMovement = em.transform.localScale.x * -1;
    }
}
