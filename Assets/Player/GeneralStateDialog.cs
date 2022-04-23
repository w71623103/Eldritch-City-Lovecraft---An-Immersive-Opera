using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStateDialog : GeneralStateBase
{
    public override void EnterState(Player pl)
    {
        pl.gStateShow = Player.generalStates.dialog;
    }

    public override void Update(Player pl)
    { }

    public override void FixedUpdate(Player pl)
    { }

    public override void LeaveState(Player pl)
    { }
}
