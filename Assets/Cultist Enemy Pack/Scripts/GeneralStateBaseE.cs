using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GeneralStateBaseE
{
    public abstract void EnterState(Enemy em);

    public abstract void Update(Enemy em);

    public abstract void FixedUpdate(Enemy em);

    public abstract void LeaveState(Enemy em);
}
