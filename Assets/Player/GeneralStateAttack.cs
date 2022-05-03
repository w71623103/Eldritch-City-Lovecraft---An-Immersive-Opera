using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When start attack, the general state will become GeneralStateAttack and stays until the player no longer input light attack.
public class GeneralStateAttack : GeneralStateBase
{
    public override void EnterState(Player pl)
    {
        pl.gStateShow = Player.generalStates.attack;
        // start attack
        pl.comboCount = 1;
        if (pl.isGrounded)
        {
            pl.playerAnim.SetInteger(pl.attackLightHash, 1);
            pl.playerAnim.SetInteger(pl.attackLightAirHash, 0);
        }
        else 
        { 
            pl.playerAnim.SetInteger(pl.attackLightAirHash, 1);
            pl.playerAnim.SetInteger(pl.attackLightHash, 0);
        }
        if (pl.isGrounded) pl.playerRB.velocity = Vector2.zero;
    }

    public override void Update(Player pl)
    { }

    public override void FixedUpdate(Player pl)
    {
        if(!pl.isGrounded) pl.playerRB.velocity = new Vector2(pl.horizontalMovement * pl.hspeed, pl.playerRB.velocity.y);
    }

    public override void LeaveState(Player pl)
    {
        //pl.canInput = false;
        pl.playerAnim.SetInteger(pl.attackLightHash, 0);
        pl.playerAnim.SetInteger(pl.attackLightAirHash, 0);
        pl.comboCount = 0;
        pl.attackTimer = pl.attackCool;
    }
}
