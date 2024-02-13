using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path Cell Objects", menuName = "TowerDefense/Grid Cell")]
public class GridCellObject : ScriptableObject
{
    public enum CellType { Path, Ground }
    public CellType cellType;
    public GameObject cellPrefab;
    public int yRotation;

}
