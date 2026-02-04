using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaSort._01_Models
{
    public class StackModel
    {
        public List<JellyModel> Jellies { get; private set; } = new List<JellyModel>();

        public Action OnJelliesAdded;
        
        public bool Contains(JellyModel jelly) => Jellies.Contains(jelly);
        
        public JellyFruit GetTopJellyFruit 
            => Jellies.Count > 0 ? Jellies.Last().JellyFruit :  JellyFruit.None;

        public void Add(JellyModel jelly)
        {
            Jellies.Add(jelly);
            OnJelliesAdded?.Invoke();
        }

        public void Remove(JellyModel jelly)
        {
            Jellies.Remove(jelly);
            OnJelliesAdded?.Invoke();
        }
        
        public int GetSimilarJelliesOnTop()
        {
            int topCount = 0;
            for (int i = Jellies.Count - 1; i >= 0; i--)
            {
                if(Jellies[i].JellyFruit != GetTopJellyFruit)
                    break;
                topCount++;
            }
        
            return topCount;
        }
    }
}