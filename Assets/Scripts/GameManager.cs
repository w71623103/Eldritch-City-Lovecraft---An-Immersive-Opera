using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Game Manager that is not destroying between Scenes.
public class GameManager : MonoBehaviour
{
    public string controlScheme;
    public List<GameManagerKeyEventInfo> importantEventRecord = new List<GameManagerKeyEventInfo>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Two functions to record if a important event (e.g. NPC conversation) happened.
    public bool checkEventHappend(string key)
    {
        foreach (GameManagerKeyEventInfo eventRecord in importantEventRecord)
        {
            if (eventRecord.eventName == key)
                return eventRecord.triggered;
        }
        return false;
    }

    public bool setEventHappend(string key)
    {
        foreach (GameManagerKeyEventInfo eventRecord in importantEventRecord)
        {
            if (eventRecord.eventName == key)
            {
                eventRecord.triggered = true;
                return eventRecord.triggered;
            }
                
        }
        return false;
    }
}
