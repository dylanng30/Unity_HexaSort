using System.Collections.Generic;
using HexaSort.GameStateMachine.GameStates;
using HexaSort.Level;
using HexaSort.Utilitilies;
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
            foreach (Transform child in spawnPoint)
            {
                Destroy(child.gameObject);
            }
        }
        //ObjectPool
    }
    /*private void StackPlacedCallBack()
    {
        stackCounter++;

        if(stackCounter >= spawnPoints.Length)
        {
            stackCounter = 0;
            CreateStacks();
        }
    }*/

    private void CreateStacks()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            CreateStack(spawnPoints[i]);
        }
    }

    private void CreateStack(Transform spawnPoint)
    {
        var hexaStack = Instantiate(hexaStackPrefab, spawnPoint.position, Quaternion.identity,spawnPoint);
        hexaStack.name = $"Stack { spawnPoint.GetSiblingIndex()}" ;
        
        int amount = Random.Range(_miniumHexaAmount, _maxiumHexaAmount);
        
        int firstMaterialCount = Random.Range(0, amount);

        Material[] materialArray = GetRandomMaterials();

        for (int i = 0; i < amount; i++)
        {
            Vector3 hexaPosition = spawnPoint.position + Vector3.up * i * Constants.HeightHexaModel;
            
            var hexaJelly = Instantiate(hexaPrefab, hexaPosition, Quaternion.identity, hexaStack.transform);
            hexaStack.Add(hexaJelly);
            hexaJelly.RegisterStack(hexaStack);
            hexaJelly.Material = i < firstMaterialCount ? materialArray[0] : materialArray[1];
        }
    }

    private Material[] GetRandomMaterials()
    {
        List<Material> materialList = new List<Material>();
        materialList.AddRange(_hexaMaterials);

        if (materialList.Count <= 0)
        {
            Debug.LogError("There are no materials in stack");
            return null;
        }
        
        Material firstMaterial = materialList[Random.Range(0, materialList.Count)];
        materialList.Remove(firstMaterial);
        Material secondMaterial = materialList[Random.Range(0, materialList.Count)];
        
        return new Material[]{firstMaterial, secondMaterial};
    }
}
