using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackHeavyE : GeneralStateBaseE
{
    public override void EnterState(Enemy em)
    {
        em.gStateShow = Enemy.generalStates.attack;
        em.comboCount = 0;
        em.enemyAnim.SetInteger("AttackStateHeavy", 1);
        em.enemyRB.velocity = Vector2.zero;
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
        //pl.canInput = false;
        em.enemyAnim.SetInteger("AttackStateHeavy", 0);
        em.comboCount = 0;
        em.attackTimer = em.attackCool;
        //pl.canInput = true;
    }
}
