using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GeneralStateBase
{
    public abstract void EnterState(Player pl);

    public abstract void Update(Player pl);

    public abstract void FixedUpdate(Player pl);

    public abstract void LeaveState(Player pl);
}
