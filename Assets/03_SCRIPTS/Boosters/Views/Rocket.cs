using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using HexaSort.ObjectPool;
using UnityEngine;

namespace HexaSort.Boosters.Views
{
    public class Rocket : MonoBehaviour
    {
        private BaseObjectPool<Rocket> _pool;

        public void RegisterPool(BaseObjectPool<Rocket> pool)
        {
            _pool = pool;
        }

        private void OnEnable()
        {
            transform.DOKill();
            transform.rotation = Quaternion.identity;
        }

        public void ReturnToPool()
        {
            transform.DOKill();

            if (_pool != null)
            {
                _pool.Return(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
