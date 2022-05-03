using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateMovementB : GeneralStateBaseB
{
    private int enemyMoveCLayerMask;
    private Vector2 movDir;
    private GameObject pl;

    public override void EnterState(DeathBringer db)
    {
        pl = GameObject.Find("Player");
        //em.comboCount = 0;
        enemyMoveCLayerMask = LayerMask.GetMask("EnemyMoveCommand");
        db.gStateShow = DeathBringer.generalStates.move;
    }

    public override void Update(DeathBringer db)
    {
        pl = GameObject.Find("Player");
        if (db.attackTimer > 0f) db.attackTimer -= Time.deltaTime;
        db.enemyAnim.SetBool(db.movingHash, db.horizontalMovement != 0.0f);
        //db.enemyAnim.SetFloat(db.jumpHash, db.enemyRB.velocity.y);
        db.enemyAnim.SetBool(db.groundHash, db.isGrounded);

        //Flip Control=========================================================================================================
        if (db.horizontalMovement < 0f)
            db.isRight = false;
        else if (db.horizontalMovement >= 0f)
            db.isRight = true;

        //Scale Version
        var scale = db.transform.localScale;
        if (db.isRight)
            db.transform.localScale = new Vector3(scale.x < 0 || db.horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);
        else
            db.transform.localScale = new Vector3(scale.x > 0 || db.horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);

        //=====================================================================================================================

        //Movement AI==========================================================================================================
        if (pl.transform.position.x >= db.transform.position.x) db.horizontalMovement = 1;
        else db.horizontalMovement = -1;
    }

    public override void FixedUpdate(DeathBringer db)
    {
        //if (pl.isGrounded) pl.playerRB.velocity = new Vector2(pl.playerRB.velocity.x, 0f);
        db.enemyRB.velocity = new Vector2(db.horizontalMovement * db.hspeed, db.enemyRB.velocity.y);
    }

    public override void LeaveState(DeathBringer db)
    { }
}
