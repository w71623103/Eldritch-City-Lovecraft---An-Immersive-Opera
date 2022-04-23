using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackEndE : GeneralStateBaseE
{
    public override void EnterState(Enemy em)
    {
        em.gStateShow = Enemy.generalStates.attackEnd;
        em.ChangeGeneralState(em.movementState);
    }

    public override void Update(Enemy em)
    { }

    public override void FixedUpdate(Enemy em)
    {
        em.enemyRB.velocity = new Vector2(em.horizontalMovement * em.hspeed, em.enemyRB.velocity.y);
    }

    public override void LeaveState(Enemy em)
    {
        em.enemyAnim.SetInteger("AttackStateLight", 0);
    }
}
