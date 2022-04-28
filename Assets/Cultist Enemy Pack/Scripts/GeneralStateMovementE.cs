using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateMovementE : GeneralStateBaseE
{
    private int enemyMoveCLayerMask;
    private Vector2 movDir;
    public override void EnterState(Enemy em)
    {
        //em.comboCount = 0;
        enemyMoveCLayerMask = LayerMask.GetMask("EnemyMoveCommand");
        em.gStateShow = Enemy.generalStates.move;
    }

    public override void Update(Enemy em)
    {
        
        if (em.attackTimer > 0f) em.attackTimer -= Time.deltaTime;
        em.enemyAnim.SetBool(em.movingHash, em.horizontalMovement != 0.0f);
        em.enemyAnim.SetFloat(em.jumpHash, em.enemyRB.velocity.y);
        em.enemyAnim.SetBool(em.groundHash, em.isGrounded);

        //Flip Control=========================================================================================================
        if (em.horizontalMovement < 0f)
            em.isRight = false;
        else if (em.horizontalMovement >= 0f)
            em.isRight = true;

        //Scale Version
        var scale = em.transform.localScale;
        if (em.isRight)
            em.transform.localScale = new Vector3(scale.x < 0 || em.horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);
        else
            em.transform.localScale = new Vector3(scale.x > 0 || em.horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);

        //=====================================================================================================================

        //Movement AI==========================================================================================================
        if (em.transform.localScale.x > 0) movDir = Vector2.left;
        else movDir = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(em.transform.position, movDir, 1f, enemyMoveCLayerMask);
        Debug.DrawRay(em.transform.position, movDir, Color.blue);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("EnemyMovementCommand"))
            {
                em.horizontalMovement *= -1;
            }
        }
    }

    public override void FixedUpdate(Enemy em)
    {
        //if (pl.isGrounded) pl.playerRB.velocity = new Vector2(pl.playerRB.velocity.x, 0f);
        em.enemyRB.velocity = new Vector2(em.horizontalMovement * em.hspeed, em.enemyRB.velocity.y);
    }

    public override void LeaveState(Enemy em)
    { }
}
