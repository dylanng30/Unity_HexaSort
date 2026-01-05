using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaSort.UI
{
    public interface IMainPanel
    {
        void Setup(UIManager uiManager);
        void Show();
        void Hide();
    }
}
