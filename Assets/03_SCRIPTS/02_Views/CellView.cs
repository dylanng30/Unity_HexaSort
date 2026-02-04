using HexaSort._01_Models;
using TMPro;
using UnityEngine;

namespace HexaSort._02_Views
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        public CellModel Model { get; private set; }

        public void Setup(CellModel model)
        {
            Model = model;
            //Gan texture
        }
    }
}