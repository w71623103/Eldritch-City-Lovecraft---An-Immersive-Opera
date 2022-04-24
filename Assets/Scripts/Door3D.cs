using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door3D : MonoBehaviour
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

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("ExploreParentCube"))
        {
            buttonIcon.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("ExploreParentCube"))
        {
            buttonIcon.SetActive(false);
            
        }
    }

    public void interact(GameObject pl)
    {
        
        pl.transform.SetParent(null);
        DontDestroyOnLoad(pl);
        SceneManager.LoadScene(sceneName);
    }    

}
