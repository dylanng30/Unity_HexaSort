using System.Collections.Generic;
using HexaSort._01_Models;

namespace HexaSort._04_Services.MergeService.Interfaces
{
    public interface IMergeService
    {
        List<MergeStep> GetMergeSteps(BoardModel board, CellModel placedCell);
    }
}