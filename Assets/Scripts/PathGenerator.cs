using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private int height, width;
    private List<Vector2Int> pathCells;

    public PathGenerator(int height, int width)
    {
        this.width = width;
        this.height = height;
    }

    public List<Vector2Int> GeneratePath()
    {
        // Generate path
        pathCells = new List<Vector2Int>();
        int y = (int)(height / 2);
        int x = 0;

        while (x < width)
        {
            pathCells.Add(new Vector2Int(x, y));
            bool validMove = false;

            while (!validMove)
            {
                int moveDir = Random.Range(0, 3);

                if (moveDir == 0 || x % 2 == 0 || (x > width - 2))
                {
                    x++;
                    validMove = true;
                }
                else if (moveDir == 1 && CellIsEmpty(x, y + 1) && y < (height - 2))
                {
                    y++;
                    validMove = true;
                }
                else if (moveDir == 2 && CellIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }
        return pathCells;
    }

    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }


    // down = 1, left = 2, right = 4, up = 8
    //        8
    //    2 CELL 4
    //       1
    //
    // so if there is a cell to the left and top, the value is 10 so
    // you would use the scriptable oject with the curve set to turn up etc...
    public int getCellNeighborVal(int x, int y)
    {
        int neighbor = 0;

        if (CellIsTaken(x, y - 1))
        {
            neighbor += 1;
        }
        if (CellIsTaken(x - 1, y))
        {
            neighbor += 2;
        }
        if (CellIsTaken(x + 1, y))
        {
            neighbor += 4;
        }
        if (CellIsTaken(x, y + 1))
        {
            neighbor += 8;
        }
        return neighbor;
    }
}
