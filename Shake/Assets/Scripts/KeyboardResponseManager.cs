using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardResponseManager : MonoBehaviour 
{
    List<KeyboardResponseButton> buttons = new List<KeyboardResponseButton>();

    public int AddButton(KeyboardResponseButton button)
    {
        buttons.Add(button);

        return buttons.Count;
    }

    public void RemoveButton(KeyboardResponseButton button)
    {
        buttons.Remove(button);
    }

    void Update()
    {
        for(int i = 0; i < buttons.Count; ++i)
        {
            if(Input.GetKey(KeyCode.Alpha0 + i + 1))
                buttons[i].Press();
        }
    }
}
