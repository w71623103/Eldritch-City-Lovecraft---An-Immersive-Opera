using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
