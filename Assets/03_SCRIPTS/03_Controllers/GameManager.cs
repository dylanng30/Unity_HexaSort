using System;
using HexaSort._01_Models;
using HexaSort._04_Services.BoardModelGenerateService;
using HexaSort._04_Services.BoardModelGenerateService.Interfaces;
using UnityEngine;

namespace HexaSort._03_Controllers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] BoardController _boardController;


        private void Awake()
        {
            
        }
    }
}