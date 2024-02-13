using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridManager : MonoBehaviour
{

    public int gridWidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 30;
    public GridCellObject[] pathCellObjects;
    public GridCellObject[] sceneryCellObjects;

    private PathGenerator pathGenerator;
    void Start()
    {
        pathGenerator = new PathGenerator(gridHeight, gridWidth);
        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;

        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSize = pathCells.Count;
        }

        StartCoroutine(CreateGrid(pathCells));
    }

    IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return StartCoroutine(LayPathCells(pathCells));
        yield return StartCoroutine(LaySceneryCells());
    }


    IEnumerator LaySceneryCells()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    int rnadomSceneryCellIndex = UnityEngine.Random.Range(0, sceneryCellObjects.Length);
                    Instantiate(sceneryCellObjects[rnadomSceneryCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    yield return new WaitForSeconds(0.01f);
                }

            }
        }
        yield return null;
    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighborVal = pathGenerator.getCellNeighborVal(pathCell.x, pathCell.y);
            GameObject pathTile = pathCellObjects[neighborVal].cellPrefab;
            GameObject pathTileCell = Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, pathCellObjects[neighborVal].yRotation, 0f, Space.Self);
            yield return new WaitForSeconds(0.005f);
        }
        yield return null;
    }

    void Update()
    {

    }
}
