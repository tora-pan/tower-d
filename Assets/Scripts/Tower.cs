using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Objects", menuName = "TowerDefense/Tower")]
public class TowerObject : ScriptableObject
{
  public enum CellType { Tower, Other }
  public CellType cellType;
  public GameObject cellPrefab;
  public int yRotation;

}
