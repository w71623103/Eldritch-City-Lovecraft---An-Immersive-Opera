using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomEffects : MonoBehaviour
{
    [SerializeField] private string prefax;
    [SerializeField] private int maxInd;

    public void OnPlayRandom()
    {
        transform.Find(prefax + Random.Range(1, maxInd+1).ToString()).GetComponent<ParticleSystem>().Play();
    }
}
