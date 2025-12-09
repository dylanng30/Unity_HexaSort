using System.Collections.Generic;
using UnityEngine;

public class StackSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private HexaJelly hexaPrefab;
    [SerializeField] private HexaStack hexaStackPrefab;
    
    [Header("---SETTINGS---")]
    [SerializeField] private int miniumHexaAmount;
    [SerializeField] private int maxiumHexaAmount;
    [SerializeField] private Material[] hexaMaterials;

    private void Start()
    {
        CreateStacks();
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
        var hexaStack = Instantiate(hexaStackPrefab, spawnPoint.position, Quaternion.identity,spawnPoint);
        hexaStack.name = $"Stack { spawnPoint.GetSiblingIndex()}" ;
        
        int amount = Random.Range(miniumHexaAmount, maxiumHexaAmount);
        
        int firstMaterialCount = Random.Range(0, amount);

        Material[] materialArray = GetRandomMaterials();

        for (int i = 0; i < amount; i++)
        {
            Vector3 hexaPosition = spawnPoint.position + Vector3.up * i * Constants.HeightHexaModel;
            
            var hexaJelly = Instantiate(hexaPrefab, hexaPosition, Quaternion.identity, hexaStack.transform);
            hexaJelly.RegisterStack(hexaStack);
            hexaJelly.Material = i < firstMaterialCount ? materialArray[0] : materialArray[1];
        }
    }

    private Material[] GetRandomMaterials()
    {
        List<Material> materialList = new List<Material>();
        materialList.AddRange(hexaMaterials);

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
