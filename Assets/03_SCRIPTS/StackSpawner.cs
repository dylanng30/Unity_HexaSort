using System.Collections.Generic;
using HexaSort.ObjectPool;
using HexaSort.Utilities;
using UnityEngine;

public class StackSpawner : MonoBehaviour
{
    [Header("---SET UP---")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private HexaJelly hexaPrefab;
    [SerializeField] private HexaStack hexaStackPrefab;
    
    private int _miniumHexaAmount;
    private int _maxiumHexaAmount;
    private Material[] _hexaMaterials;

    private int stackCounter;

    private BaseObjectPool<HexaJelly> jellyPool;
    private BaseObjectPool<HexaStack> stackPool;

    private void Awake()
    {
        jellyPool = new BaseObjectPool<HexaJelly>(hexaPrefab, transform, 50);
        stackPool = new BaseObjectPool<HexaStack>(hexaStackPrefab, transform, 20);
    }

    public void Setup(int miniumHexaAmount, int maxiumHexaAmount, Material[] hexaMaterials)
    {
        _miniumHexaAmount = miniumHexaAmount;
        _maxiumHexaAmount = maxiumHexaAmount;
        _hexaMaterials = hexaMaterials;
        
        CreateStacks();
    }
    
    public void NotifyStackPlaced()
    {
        stackCounter++;
    }
    
    public void CheckAndSpawnNewStacks()
    {
        if(stackCounter >= spawnPoints.Length)
        {
            stackCounter = 0;
            CreateStacks();
        }
    }

    public void Clear()
    {
        stackCounter = 0;
        
        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = spawnPoint.childCount - 1; i >= 0; i--)
            {
                Transform child = spawnPoint.GetChild(i);
                var stack = child.GetComponent<HexaStack>();

                if (stack != null)
                {
                    stack.ReturnToPool();
                }
                else
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private void CreateStacks()
    {

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            CreateStack(spawnPoints[i]);
        }
    }

    private void CreateStack(Transform spawnPoint)
    {
        var hexaStack = stackPool.Get();

        hexaStack.transform.SetParent(spawnPoint);
        hexaStack.transform.localPosition = Vector3.zero;
        hexaStack.transform.localRotation = Quaternion.identity;
        
        hexaStack.name = $"Stack { spawnPoint.GetSiblingIndex()}" ;

        hexaStack.RegisterPool(stackPool);

        int amount = Random.Range(_miniumHexaAmount, _maxiumHexaAmount);

        if (_hexaMaterials == null || _hexaMaterials.Length == 0) return;

        int firstMaterialCount = Random.Range(0, amount);

        Material[] materialArray = GetRandomMaterials();

        for (int i = 0; i < amount; i++)
        {
            Vector3 hexaPosition = spawnPoint.position + Vector3.up * i * Constants.HeightHexaModel;

            var hexaJelly = jellyPool.Get();

            hexaJelly.transform.position = hexaPosition;
            hexaJelly.transform.rotation = Quaternion.identity;

            hexaJelly.RegisterPool(jellyPool);
            hexaJelly.Material = i < firstMaterialCount ? materialArray[0] : materialArray[1];

            hexaStack.Add(hexaJelly);
            hexaJelly.RegisterStack(hexaStack);
        }
    }

    private Material[] GetRandomMaterials()
    {
        if (_hexaMaterials.Length < 2) 
            return new Material[] { _hexaMaterials[0], _hexaMaterials[0] };

        List<Material> materialList = new List<Material>(_hexaMaterials);

        Material firstMaterial = materialList[Random.Range(0, materialList.Count)];
        materialList.Remove(firstMaterial);
        Material secondMaterial = materialList[Random.Range(0, materialList.Count)];

        return new Material[] { firstMaterial, secondMaterial };
    }
}
