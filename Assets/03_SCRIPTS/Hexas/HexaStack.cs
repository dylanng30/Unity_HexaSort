using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexaSort;
using HexaSort.ObjectPool;
using HexaSort.Utilities;
using TMPro;
using UnityEngine;

public class HexaStack : MonoBehaviour
{
    public List<HexaJelly> Jellies {get; private set;}

    public Material GetTopHexaMaterial() => (Jellies != null && Jellies.Count > 0) ? Jellies[Jellies.Count - 1].Material : null;

    [SerializeField] private Transform _canvas;
    [SerializeField] private TextMeshProUGUI _topCountText;

    private BaseObjectPool<HexaStack> _pool;

    private void OnEnable()
    {
        if(BillboardManager.Instance != null)
            BillboardManager.Instance.Register(_canvas);

        if (Jellies == null)
            Jellies = new List<HexaJelly>();
        else
            Jellies.Clear();

        UpdateCanvas();
        UpdateTopCountText();
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

    public void RegisterPool(BaseObjectPool<HexaStack> pool)
    {
        _pool = pool;
    }

    public void ReturnToPool()
    {
        if (Jellies != null && Jellies.Count > 0)
        {
            foreach (var jelly in Jellies.ToList())
            {
                jelly.Clear();
            }
            Jellies.Clear();
        }
        if (transform.parent != null)
        {
            var cell = transform.parent.GetComponent<HexaCell>();
            if (cell != null && cell.HexaStack == this)
            {
                cell.RegisterStack(null);
            }
        }

        transform.SetParent(null);

        if (_pool != null)
            _pool.Return(this);
        else
            Destroy(gameObject);
    }

    public void Reverse()
    {
        for (int i = 0; i < Jellies.Count; i++)
        {
            var jelly = Jellies[i];
            jelly.transform.localPosition = Vector3.up * (Jellies.Count - 1 - i) * Constants.HeightHexaModel;
        }
        Jellies.Reverse();
        UpdateTopCountText();
        UpdateCanvas();
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
            ReturnToPool();
        }
    }
    
    private void UpdateCanvas()
    {
        if(_canvas == null) return;
        int count = Jellies != null ? Jellies.Count : 0;
        _canvas.localPosition = Vector3.up * Jellies.Count * Constants.HeightHexaModel;
    }

    private void UpdateTopCountText()
    {
        if (_topCountText)
            _topCountText.text = GetTopCount().ToString();
    }
    
    private int GetTopCount()
    {
        if (Jellies == null || Jellies.Count == 0) return 0;

        int topCount = 0;
        var topMat = GetTopHexaMaterial();

        if (topMat == null) return 0;

        for (int i = Jellies.Count - 1; i >= 0; i--)
        {
            if (Jellies[i] == null) continue;

            if (Jellies[i].Material.color != topMat.color)
                break;
            topCount++;
        }
        
        return topCount;
    }
}
