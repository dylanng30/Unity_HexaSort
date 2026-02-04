using System.Collections.Generic;
using HexaSort._01_Models;
using HexaSort._04_Services.MergeService.Interfaces;

namespace HexaSort._04_Services.MergeService
{
    public class MergeService : IMergeService
    {
        public List<MergeStep> GetMergeSteps(BoardModel board, CellModel placedCell)
        {
            List<MergeStep> mergeSteps = new();
            
            return mergeSteps;
        }
    }
}