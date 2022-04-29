using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemCollect : MonoBehaviour
{
    //[SerializeField] private SceneTypeSetter.SceneType thisScene;
    [SerializeField] private GameObject buttonIcon;
    [SerializeField] private Item item;
    
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

    public void interact()
    {
        //SceneManager.LoadScene(sceneName);
        Debug.Log("delegated to inventory");
        GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(item);
        Destroy(gameObject);
    }    

}
