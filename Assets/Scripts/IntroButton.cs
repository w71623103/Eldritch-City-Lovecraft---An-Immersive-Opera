using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class IntroButton : MonoBehaviour
{
    private void Start()
    {
        //Destroy all do not destroy on load objects when restart
        Destroy(GameObject.Find("Inventory"));
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("Player"));
    }
    public void startGame()
    {
        SceneManager.LoadScene("Initial");
    }

    void OnIntro()
    {
        startGame();
    }
}
