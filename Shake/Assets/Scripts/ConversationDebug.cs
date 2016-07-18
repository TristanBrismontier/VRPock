using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationDebug : MonoBehaviour 
{
    void Start()
    {
        //var trig = gameObject.AddComponent<PixelCrushers.DialogueSystem.ConversationTrigger>();
        //trig.OnUse();


    }

    void OnGUI()
    {
        PixelCrushers.DialogueSystem.DialogueLua.GetVariable("Awkwardness");

        var response =PixelCrushers.DialogueSystem.DialogueManager.CurrentConversationState.pcResponses ;
        if(response != null)
            GUI.Label(new Rect(10, 10, 100, 100), response.Length.ToString());
    }

    void Update()
    {
        var response = PixelCrushers.DialogueSystem.DialogueManager.CurrentConversationState.pcResponses;
        if(response != null)
        {
           // PixelCrushers.DialogueSystem.
           // GUI.Label(new Rect(10, 10, 100, 100), response.Length.ToString());
        }
    }
}
