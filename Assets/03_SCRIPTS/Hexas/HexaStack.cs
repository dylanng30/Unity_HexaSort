using System;
using System.Collections;
using System.Collections.Generic;
using HexaSort;
using HexaSort.Utilities;
using TMPro;
using UnityEngine;

public class HexaStack : MonoBehaviour
{
    public List<HexaJelly> Jellies {get; private set;}
    
    public Material GetTopHexaMaterial() => Jellies[Jellies.Count - 1].Material;
    
    [SerializeField] private Transform _canvas;
    [SerializeField] private TextMeshProUGUI _topCountText;

    private void OnEnable()
    {
        if(BillboardManager.Instance != null)
            BillboardManager.Instance.Register(_canvas);
    }
    private void OnDisable()
    {
        if(BillboardManager.Instance != null)
            BillboardManager.Instance.Unregister(_canvas);
    }

    private void LateUpdate()
    {
        UpdateCanvas();
        UpdateTopCountText();
    }

    public void Add(HexaJelly jelly)
    {
        if(Jellies == null)
            Jellies = new List<HexaJelly>();
        
        Jellies.Add(jelly);
        jelly.SetParent(transform);
        jelly.transform.localRotation = Quaternion.identity;

        UpdateCanvas();
        UpdateTopCountText();
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
        
        UpdateCanvas();
        UpdateTopCountText();
        
        if (Jellies.Count <= 0)
        {
            DestroyImmediate(gameObject);
        }
    }
    
    private void UpdateCanvas()
    {
        if(_canvas == null) return; 
        
        _canvas.localPosition = Vector3.up * Jellies.Count * Constants.HeightHexaModel;
    }

    private void UpdateTopCountText()
    {
        if (_topCountText)
            _topCountText.text = GetTopCount().ToString();
    }
    
    private int GetTopCount()
    {
        int topCount = 0;
        for (int i = Jellies.Count - 1; i >= 0; i--)
        {
            if(Jellies[i].Material.color != GetTopHexaMaterial().color)
                break;
            topCount++;
        }
        
        return topCount;
    }
}
