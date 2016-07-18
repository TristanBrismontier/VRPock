using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour 
{
    List<ShakeHandStuff> npcs = new List<ShakeHandStuff>();

    ShakeHandStuff realGuy = null;
    public ShakeHandStuff RealGuy 
    {
        get { return realGuy; }
    }
    
    #region Singleton

    // Bad implementation don't call until after Awake
    private static NPCManager s_instance;
    public static NPCManager Instance
    {
        get { return s_instance; }
    }

    #endregion // Singleton

    void Awake()
    {
        s_instance = this;
    
        int count = transform.childCount;

        // Get all children
        for(int i = 0; i < count; ++i)
        {
            Transform child = transform.GetChild(i);
            var shake = child.GetComponentInChildren<ShakeHandStuff>();
            if(shake != null)
                npcs.Add(shake);
        }

        // Set one as the real one
        realGuy = npcs[Random.Range(0, npcs.Count)];
    }

    public bool IsRealPerson(ShakeHandStuff hand)
    {
        return hand.isClone;
    }
}
