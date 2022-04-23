using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTypeSetter : MonoBehaviour
{
    public enum SceneType
    {
        Action,
        Street,
        Explore,
    }

    [SerializeField] private SceneType mySceneType;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().currentSceneType = mySceneType;
    }

    
}
