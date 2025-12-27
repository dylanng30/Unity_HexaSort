using System.Collections.Generic;
using UnityEngine;

namespace HexaSort.Utilitilies
{
    public static class HexaAlgorithm
    {
        //QRS Grid

        //Find neighbor hexe cells in radius
        public static List<HexaCell> GetNeighborsInRadius(HexaBoard board, HexaCell startCell, int radius)
        {
            HashSet<HexaCell> result = new HashSet<HexaCell>();

            Dictionary<Vector3, int> visitedSteps = new Dictionary<Vector3, int>();

            RecursiveFind(board, startCell, 0, radius, result, visitedSteps);

            return new List<HexaCell>(result);
        }

        private static void RecursiveFind(HexaBoard board, HexaCell currentCell, int currentStep, int maxRadius,
            HashSet<HexaCell> result, Dictionary<Vector3, int> visitedSteps)
        {
            if (currentStep > maxRadius) return;

            Vector3 currentCoord = currentCell.Coordinates;

            if (visitedSteps.ContainsKey(currentCoord) && visitedSteps[currentCoord] <= currentStep)
                return;

            visitedSteps[currentCoord] = currentStep;
            result.Add(currentCell);

            foreach (Vector3 dir in Constants.HexaDirections)
            {
                Vector3 neighborCoord = currentCoord + dir;

                if (board.Map.TryGetValue(neighborCoord, out HexaCell neighborCell))
                {
                    RecursiveFind(board, neighborCell, currentStep + 1, maxRadius, result, visitedSteps);
                }
            }
        }
        //
    }
}