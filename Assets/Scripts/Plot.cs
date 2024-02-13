using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Plot Properties")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color selectedColor;
    private bool isSelected = false;
    private GameObject tower;
    private Color[] startingColors;
    private GameObject[] allPlots;
    private List<Vector2Int> pathCells;
    public TowerObject towerObject;
    private void Start()
    {
        // get layer name of the plot

        startingColors = new Color[meshRenderer.materials.Length];

        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            startingColors[i] = meshRenderer.materials[i].color;
        }
    }

    private void OnMouseEnter()
    {
        string layerName = LayerMask.LayerToName(gameObject.layer);
        if (layerName != "Path" && !isSelected)
        {
            HighlightPlot();
        }

    }



    private void OnMouseExit()
    {
        string layerName = LayerMask.LayerToName(gameObject.layer);
        if (layerName != "Path" && !isSelected)
        {
            UnhighlightPlot();
        }

    }

    private void OnMouseDown()
    {

        if (isSelected)
        {
            Debug.Log("going to build");
            buildTower();
        }


        allPlots = GameObject.FindGameObjectsWithTag("Plot");
        foreach (GameObject plot in allPlots)
        {


            if (plot.gameObject != gameObject)
            {
                plot.GetComponent<Plot>().isSelected = false;
                plot.GetComponent<Plot>().UnSelectPlot();
            }
            else
            {
                if (LayerMask.LayerToName(gameObject.layer) != "Scenery")
                {
                    isSelected = true;
                    Debug.Log(LayerMask.LayerToName(gameObject.layer) + " is the layer name");
                    SelectPlot();
                }
            }
        }

    }

    private void HighlightPlot()
    {
        for (int i = 0; i < startingColors.Length; i++)
        {
            meshRenderer.materials[i].color = highlightColor;
        }
    }

    private void UnhighlightPlot()
    {
        for (int i = 0; i < startingColors.Length; i++)
        {
            meshRenderer.materials[i].color = startingColors[i];
        }
    }

    private void SelectPlot()
    {

        for (int i = 0; i < startingColors.Length; i++)
        {
            meshRenderer.materials[i].color = selectedColor;
        }
    }
    private void UnSelectPlot()
    {
        for (int i = 0; i < startingColors.Length; i++)
        {
            meshRenderer.materials[i].color = startingColors[i];
        }
    }

    private bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }

    private void buildTower()
    {
        GameObject towerPrefab = Instantiate(towerObject.cellPrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        isSelected = false;
        UnSelectPlot();
    }
}
