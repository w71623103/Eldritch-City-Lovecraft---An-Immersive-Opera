using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineFindPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("playerCMdummy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
