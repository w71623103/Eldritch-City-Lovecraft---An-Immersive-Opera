using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GeneralStateBaseB
{
    public abstract void EnterState(DeathBringer db);

    public abstract void Update(DeathBringer db);

    public abstract void FixedUpdate(DeathBringer db);

    public abstract void LeaveState(DeathBringer db);
}
