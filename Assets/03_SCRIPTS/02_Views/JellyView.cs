using HexaSort._01_Models;
using UnityEngine;

namespace HexaSort._02_Views
{
    public class JellyView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        public JellyModel Model { get; private set; }

        public void Setup(JellyModel model, Material material)
        {
            Model = model;
            _renderer.material = material;
        }

        public void Clear()
        {
            Model = null;
        }
    }
}