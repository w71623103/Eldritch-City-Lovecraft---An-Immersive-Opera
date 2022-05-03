using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackEndB : GeneralStateBaseB
{
    public override void EnterState(DeathBringer db)
    {
        db.gStateShow = DeathBringer.generalStates.attackEnd;
        db.ChangeGeneralState(db.movementState);
    }

    public override void Update(DeathBringer db)
    { }

    public override void FixedUpdate(DeathBringer db)
    {
        db.enemyRB.velocity = new Vector2(db.horizontalMovement * db.hspeed, db.enemyRB.velocity.y);
    }

    public override void LeaveState(DeathBringer db)
    {
        db.enemyAnim.SetInteger("AttackStateLight", 0);
    }
}
