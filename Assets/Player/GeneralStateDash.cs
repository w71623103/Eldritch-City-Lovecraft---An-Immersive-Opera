using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateDash : GeneralStateBase
{
    public override void EnterState(Player pl)
    {
        pl.gStateShow = Player.generalStates.dash;
        //pl.ChangeGeneralState(pl.movementState);
        pl.playerRB.velocity = Vector2.zero;
    }

    public override void Update(Player pl)
    { }

    public override void FixedUpdate(Player pl)
    {
        //pl.playerRB.velocity = new Vector2(pl.horizontalMovement * pl.hspeed, pl.playerRB.velocity.y);
    }

    public override void LeaveState(Player pl)
    { }
}
