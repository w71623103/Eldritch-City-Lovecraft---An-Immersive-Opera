using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackEnd : GeneralStateBase
{
    public override void EnterState(Player pl)
    {
        pl.gStateShow = Player.generalStates.attackEnd;
        //pl.ChangeGeneralState(pl.movementState);
    }

    public override void Update(Player pl)
    { }

    public override void FixedUpdate(Player pl)
    {
        pl.playerRB.velocity = new Vector2(pl.horizontalMovement * pl.hspeed, pl.playerRB.velocity.y);
    }

    public override void LeaveState(Player pl)
    { }
}
