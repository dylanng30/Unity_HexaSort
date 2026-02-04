using System.Collections.Generic;
using HexaSort._01_Models;
using HexaSort._05_Utilities;
using TMPro;
using UnityEngine;

namespace HexaSort._02_Views
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private JellyView _jellyView;
        [SerializeField] private CellView _cellView;
        
        private Dictionary<CellModel, CellView> _cellMapper = new ();
        private Dictionary<JellyModel, JellyView> _jellyMapper = new ();

        public void GenerateBoard(BoardModel boardModel)
        {
            Clear();
            
            float heightCell = Constants.CellSize;
            float widthCell = Constants.CellSize * Mathf.Sqrt(3) / 2;
                
            Vector3 qDirection = Quaternion.Euler(0, 60, 0) * Vector3.right;
            Vector3 rDirection = Vector3.back;
            Vector3 sDirection = Quaternion.Euler(0, 120, 0) * Vector3.right;
            
            foreach (Vector3Int coordinate in boardModel.Cells.Keys)
            {
                int q = coordinate.x;
                int r = coordinate.y;
                int s = coordinate.z;
                Vector3 spawnPosition = 
                    rDirection * r * heightCell * 1.5f +
                    qDirection * q * widthCell +
                    sDirection * s * widthCell;
                
                CellView view = Instantiate(_cellView, spawnPosition, Quaternion.identity);
                view.transform.SetParent(transform);
                CellModel cellModel = boardModel.Cells[coordinate];
                view.Setup(cellModel);
                _cellMapper.Add(cellModel, view);
            }
        }

        public void Merge(List<JellyModel> mergedModels)
        {
            foreach (JellyModel model in mergedModels)
            {
                if (_jellyMapper.TryGetValue(model, out JellyView view))
                {
                    //Cho view vào Pool
                }
            }
        }
        
        public void Clear()
        {
            if (_cellMapper.Count > 0)
            {
                foreach (var cellView in _cellMapper.Values)
                {
                    //Cho view vào Pool
                }
                _cellMapper.Clear();
            }
            
            if (_jellyMapper.Count > 0)
            {
                foreach (var jellyView in _jellyMapper.Values)
                {
                    //Cho view vào Pool
                }
                _jellyMapper.Clear();
            }
        }
    }
}