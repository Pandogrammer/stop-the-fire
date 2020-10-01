using System.Collections.Generic;
using UnityEngine;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] cellTypes;
        [SerializeField] private int cellSize;

        public int CellSize => cellSize;
        
        private List<GameObject> spawnedCells;
        public virtual void DestroyLevel()
        {
            foreach (var cell in spawnedCells)
            {
                Destroy(cell);
            }
            spawnedCells.Clear();
        }
        
        public void SpawnLevel(Level level)
        {
            spawnedCells = new List<GameObject>();
            for (int i = 0; i < level.width; i++)
            {
                for (int j = 0; j < level.height; j++)
                {
                    var cellType = level.GetCellAt(i, j);
                    if (cellType == Cell.Floor)
                        continue;
                    var position = new UnityEngine.Vector3(i, transform.position.y, j) * cellSize + transform.position;
                    var cellPrefab = cellTypes[(int) cellType];
                    var spawnedCell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                    spawnedCell.transform.localScale *= cellSize;
                    spawnedCells.Add(spawnedCell);
                }
            }
        }
        
    }
}