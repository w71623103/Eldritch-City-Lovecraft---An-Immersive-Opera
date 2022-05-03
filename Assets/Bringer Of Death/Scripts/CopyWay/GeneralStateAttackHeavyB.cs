using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateAttackHeavyB : GeneralStateBaseB
{
    private float armorPercent;
    public override void EnterState(DeathBringer db)
    {
        db.gStateShow = DeathBringer.generalStates.attack;
        db.transform.position = db.phase2Loc.transform.position;
        //db.comboCount = 0;
        db.enemyAnim.SetInteger("AttackStateHeavy", 1);
        db.enemyRB.velocity = Vector2.zero;
        db.armor = db.maxArmor;
        db.phase2Entered = true;
        db.armorBar.SetActive(true);
    }

    public override void Update(DeathBringer db)
    {
        /*if (pl.comboCount == 1 && pl.comboed)
        {
            pl.playerAnim.SetInteger("AttackStateLight", 0);
        }*/
        armorPercent = (float) db.armor / (float) db.maxArmor;
        Debug.Log(armorPercent);
        db.armorBar.GetComponent<UIBar>().percent = (armorPercent);
        if (db.armor <= 0) db.ChangeGeneralState(db.movementState);
    }

    public override void FixedUpdate(DeathBringer db)
    {
        
    }

    public override void LeaveState(DeathBringer db)
    {
        //pl.canInput = false;
        db.enemyAnim.SetInteger("AttackStateHeavy", 0);
        db.comboCount = 0;
        db.attackTimer = db.attackCool;
        //pl.canInput = true;
        db.armorBar.SetActive(false);
    }
}
