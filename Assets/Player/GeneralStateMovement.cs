using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateMovement : GeneralStateBase
{
    public override void EnterState(Player pl)
    {
        //pl.comboCount = 0;
        pl.gStateShow = Player.generalStates.move;
    }

    public override void Update(Player pl)
    {
        if (pl.attackTimer > 0f) pl.attackTimer -= Time.deltaTime;
        pl.playerAnim.SetBool(pl.movingHash, pl.horizontalMovement != 0.0f);
        pl.playerAnim.SetFloat(pl.jumpHash, pl.playerRB.velocity.y);
        pl.playerAnim.SetBool(pl.groundHash,pl.isGrounded);

        //Flip Control=========================================================================================================
        if (pl.horizontalMovement < 0f)
            pl.isLeft = true;
        else if (pl.horizontalMovement >= 0f)
            pl.isLeft = false;

        //Scale Version
        var scale = pl.transform.localScale;
        if (pl.isLeft)
            pl.transform.localScale = new Vector3(scale.x < 0 || pl.horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);
        else
            pl.transform.localScale = new Vector3(scale.x > 0 || pl.horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);
        //======================================================================================================================
    }

    public override void FixedUpdate(Player pl)
    {
        //if (pl.isGrounded) pl.playerRB.velocity = new Vector2(pl.playerRB.velocity.x, 0f);
        pl.playerRB.velocity = new Vector2(pl.horizontalMovement * pl.hspeed, pl.playerRB.velocity.y);
    }

    public override void LeaveState(Player pl)
    { }
}
