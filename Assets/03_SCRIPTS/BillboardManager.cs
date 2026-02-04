using System.Collections.Generic;
using HexaSort.Utilities;
using UnityEngine;

namespace HexaSort
{
    public class BillboardManager : Singleton<BillboardManager>
    {
        private List<Transform> objs = new List<Transform>();
        private Transform _mainCameraTransform;

        protected override void Awake()
        {
            base.Awake();
            _mainCameraTransform = Camera.main.transform;
            
        }
        public void Register(Transform obj)
        {
            if (!objs.Contains(obj))
                objs.Add(obj);
        }

        public void Unregister(Transform obj)
        {
            if (objs.Contains(obj))
                objs.Remove(obj);
        }
        
        private void LateUpdate()
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i] != null)
                {
                    objs[i].rotation = _mainCameraTransform.rotation;
                }
            }
        }
        
    }
}