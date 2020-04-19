using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    public GameObject searcher;
    public LayerMask floorMask;
    public int xSize;
    public int ySize;
    public int unit;
    public int[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new int[xSize, ySize];
        BuildGrid();
        IterateGrid();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BuildGrid()
    {
        Vector3 home = transform.position;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                searcher.transform.localPosition = new Vector3(i * unit, -0.5f, j * unit);
                //Debug.Log("got here");
                RaycastHit hit;
                if (Physics.Raycast(searcher.transform.position, transform.up, out hit, 1f, floorMask, QueryTriggerInteraction.UseGlobal))
                {
                    Debug.DrawRay(searcher.transform.position, transform.up, Color.green, 10f);
                    grid[i, j] = hit.transform.GetComponent<FloorType>().floorType;
                    Debug.Log(hit.transform + " " + hit.transform.GetComponent<FloorType>().floorType);
                }
                else { Debug.DrawRay(searcher.transform.position, transform.up, Color.red, 10f); }



            }
        }

        //searcher.transform.localPosition = new Vector3(0, 0, 0);
    }
    void IterateGrid()
    {
        
        for (int i = 0; i < ySize; i++)
        {
            string rowString = "(";

            for (int j = 0; j < xSize; j++)
            {
                rowString += (grid[j, i]).ToString() + ", ";
            }
            Debug.Log("Row:" + i + " contains " + rowString.Substring(0, rowString.Length - 2) + ")");
        }
    }
}
