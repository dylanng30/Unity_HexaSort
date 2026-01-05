using System.Collections.Generic;
using HexaSort.ObjectPool;
using HexaSort.Utilitilies;
using UnityEngine;

namespace HexaSort.MapGenerators
{
    public class HexaMapGenerator : IMapGenerator
    {
        public Dictionary<Vector3Int, HexaCell> CreateMap(BaseObjectPool<HexaCell> pool ,int width, int height)
        {
            Dictionary<Vector3Int, HexaCell> HexaCellDic = new Dictionary<Vector3Int, HexaCell>();

            HexaCellDic.Clear();
        
            float heightCell = Constants.CellSize;
            float widthCell = Constants.CellSize * Mathf.Sqrt(3) / 2;
        
            Vector3 qDirection = Quaternion.Euler(0, 60, 0) * Vector3.right;
            Vector3 rDirection = Vector3.back;
            Vector3 sDirection = Quaternion.Euler(0, 120, 0) * Vector3.right;
        
            for (int q = -width; q <= width; q++)
            {
                int r1 = Mathf.Max(-width, -q - width);
                int r2 = Mathf.Min(width, -q + width);
            
                for (int r = r1; r <= r2; r++)
                {
                    int s = -q - r;
                
                    var cell = pool.Get();
                    cell.Setup(q, r, s);
                
                    Vector3 spawnPosition = 
                        rDirection * r * heightCell * 1.5f +
                        qDirection * q * widthCell +
                        sDirection * s * widthCell;
                
                    cell.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                    HexaCellDic.Add(new Vector3Int(q, r, s), cell);
                }
            }
            
            return HexaCellDic;
        }
    }
}