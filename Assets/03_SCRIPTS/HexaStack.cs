using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HexaStack : MonoBehaviour
{
    public List<HexaJelly> Jellies {get; private set;}
    
    public Material GetTopHexaMaterial() => Jellies[Jellies.Count - 1].Material;
    
    
    public void Add(HexaJelly jelly)
    {
        if(Jellies == null)
            Jellies = new List<HexaJelly>();
        
        Jellies.Add(jelly);
        jelly.SetParent(transform);
        jelly.transform.localRotation = Quaternion.identity;
    }
    public void Place()
    {
        foreach (var jelly in Jellies)
        {
            jelly.DisableCollider();
        }
    }
    
    public bool Contains(HexaJelly jelly) => Jellies.Contains(jelly);

    public void Remove(HexaJelly jelly)
    {
        Jellies.Remove(jelly);
        
        if (Jellies.Count <= 0)
        {
            DestroyImmediate(gameObject);
        }
    }
}
