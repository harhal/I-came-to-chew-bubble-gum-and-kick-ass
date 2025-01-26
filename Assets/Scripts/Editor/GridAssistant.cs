using Core;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class GridAssistant : MonoBehaviour
    {
        static Grid GetTheGrid()
        {
            var thes = FindObjectsByType<The>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach (var the in thes)
            {
                if (the.theGrid)
                {
                    return the.GetComponent<Grid>();
                }
            }

            return null;
        }

        [MenuItem("Grid/Distribute Actors")]
        static void DistributeActors()
        {
            Grid theGrid = GetTheGrid();
            if (theGrid == null)
            {
                Debug.LogError("There is no TheGrid!");
                return;
            }

            var occupiers = FindObjectsByType<BaseGridPlacable>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach (var occupier in occupiers)
            {
                Vector3Int gridPositionV3 = new Vector3Int(occupier.gridPosition.x, occupier.gridPosition.y, 0);
                occupier.SetGridPosition(occupier.gridPosition, theGrid);
                
#if UNITY_EDITOR
                EditorUtility.SetDirty(occupier);
#endif
            }
        }

        [MenuItem("Grid/Update Actors Grid Locations")]
        public static void MatchActorsLocations()
        {
            Grid theGrid = GetTheGrid();
            if (theGrid == null)
            {
                Debug.LogError("There is no TheGrid!");
                return;
            }

            var occupiers = FindObjectsByType<BaseGridPlacable>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach (var occupier in occupiers)
            {
                occupier.gridPosition = (Vector2Int)theGrid.WorldToCell(occupier.transform.position);
#if UNITY_EDITOR
                EditorUtility.SetDirty(occupier);
#endif
            }
        }
    }
}
