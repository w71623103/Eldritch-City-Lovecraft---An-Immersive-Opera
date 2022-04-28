using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovLogicHolder : MonoBehaviour
{
    public enum EnemyMoveCommand
    {
        turnBack,
        jump,
    }

    public EnemyMoveCommand moveCommand;
}
