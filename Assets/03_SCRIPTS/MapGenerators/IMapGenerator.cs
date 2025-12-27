using System.Collections.Generic;
using HexaSort.ObjectPool;
using UnityEngine;

namespace HexaSort.MapGenerators
{
    public interface IMapGenerator
    {
        Dictionary<Vector3, HexaCell> CreateMap(BaseObjectPool<HexaCell> pool, int width, int height);
    }
}