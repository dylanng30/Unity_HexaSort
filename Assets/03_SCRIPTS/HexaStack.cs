using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaStack : MonoBehaviour
{
    public List<HexaJelly> Jellies {get; private set;}
    

    public void Add(HexaJelly jelly)
    {
        if(Jellies == null)
            Jellies = new List<HexaJelly>();
        
        Jellies.Add(jelly);
    }

    public void Place()
    {
        foreach (var jelly in Jellies)
        {
            jelly.DisableCollider();
        }
    }
}
