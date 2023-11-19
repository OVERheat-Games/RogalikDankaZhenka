using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    public GameObject objectToPlace;
    public Vector2Int gridSize = new Vector2Int(5, 5);
    public float cellSize = 1f;

    void Start()
    {
        PlaceObjectsOnGrid();
    }

    void PlaceObjectsOnGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                GameObject obj = Instantiate(objectToPlace, position, Quaternion.identity);
                obj.transform.parent = transform; // Делаем объект дочерним к сетке.
            }
        }
    }
}
