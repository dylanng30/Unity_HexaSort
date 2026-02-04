using System.Collections.Generic;
using UnityEngine;

namespace HexaSort.Utilities
{
    public static class HexaAlgorithm
    {
        //QRS Grid

        //Find neighbor hexe cells in radius
        public static List<HexaCell> GetNeighborsInRadius(HexaBoard board, HexaCell startCell, int radius)
        {
            HashSet<HexaCell> result = new HashSet<HexaCell>();

            Dictionary<Vector3Int, int> visitedSteps = new Dictionary<Vector3Int, int>();

            RecursiveFind(board, startCell, 0, radius, result, visitedSteps);

            return new List<HexaCell>(result);
        }

        private static void RecursiveFind(HexaBoard board, HexaCell currentCell, int currentStep, int maxRadius,
            HashSet<HexaCell> result, Dictionary<Vector3Int, int> visitedSteps)
        {
            if (board == null)
            {
                Debug.LogError("Lỗi: Biến 'board' truyền vào bị NULL.");
                return;
            }
            if (board.Map == null)
            {
                Debug.LogError("Lỗi: 'board.Map' bị NULL. Có thể LevelManager chưa gọi board.Setup()?");
                return;
            }
            if (currentStep > maxRadius) return;

            Vector3Int currentCoord = currentCell.Coordinates;

            if (visitedSteps.ContainsKey(currentCoord) && visitedSteps[currentCoord] <= currentStep)
                return;

            visitedSteps[currentCoord] = currentStep;
            result.Add(currentCell);

            foreach (Vector3Int dir in Constants.HexaDirections)
            {
                Vector3Int neighborCoord = currentCoord + dir;

                if (board.Map.TryGetValue(neighborCoord, out HexaCell neighborCell))
                {
                    RecursiveFind(board, neighborCell, currentStep + 1, maxRadius, result, visitedSteps);
                }
            }
        }
        //
    }
}