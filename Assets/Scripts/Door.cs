using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    [SerializeField] private SceneTypeSetter.SceneType thisScene;
    [SerializeField] private GameObject buttonIcon;
    [SerializeField] private string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonIcon.SetActive(false);
            
        }
    }

    public void interact(GameObject pl)
    {
        SceneManager.LoadScene(sceneName);
    }    

}
