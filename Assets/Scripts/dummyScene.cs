using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class dummyScene : MonoBehaviour
{
    [SerializeField] private string firstSceneName;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
