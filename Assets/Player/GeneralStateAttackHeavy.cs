using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackHeavy : GeneralStateBase
{
    public override void EnterState(Player pl)
    {
        pl.gStateShow = Player.generalStates.attack;
        pl.comboCount = 0;
        if (pl.isGrounded)
        {
            pl.playerAnim.SetInteger(pl.attackHeavyHash, 1);
            pl.playerAnim.SetInteger(pl.attackHeavyAirHash, 0);
        }
        else
        {
            pl.playerAnim.SetInteger(pl.attackHeavyAirHash, 1);
            pl.playerAnim.SetInteger(pl.attackHeavyHash, 0);
        }
        if (pl.isGrounded) pl.playerRB.velocity = Vector2.zero;
    }

    public override void Update(Player pl)
    {
        /*if (pl.comboCount == 1 && pl.comboed)
        {
            pl.playerAnim.SetInteger("AttackStateLight", 0);
        }*/
    }

    public override void FixedUpdate(Player pl)
    {
        if (!pl.isGrounded) pl.playerRB.velocity = new Vector2(pl.horizontalMovement * pl.hspeed, pl.playerRB.velocity.y);
    }

    public override void LeaveState(Player pl)
    {
        //pl.canInput = false;
        pl.playerAnim.SetInteger(pl.attackHeavyHash, 0);
        pl.playerAnim.SetInteger(pl.attackHeavyAirHash, 0);
        pl.comboCount = 0;
        pl.attackTimer = pl.attackCool;
        //pl.canInput = true;
    }
}
