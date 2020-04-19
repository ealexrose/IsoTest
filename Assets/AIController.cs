using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GridInfo gridInfo;
    public LayerMask floorMask;
    int[] target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SearchForTarget();
        }
    }

    void SearchForTarget()
    {
        target = FindValidTarget();
        BreadthSearchInitiate((int)transform.position.x, (int)transform.position.z);
    }

    int[] FindValidTarget()
    {
        int z = 0;
        do {
            int x = Random.Range(0, gridInfo.xSize);
            int y = Random.Range(0, gridInfo.ySize);
            if (gridInfo.grid[x, y] == 0)
            {
                return (new int[] { x, y });
            }
            z++;
            
        } while (z < 100);
        return (null);
    }
    List<int> BreadthSearchInitiate(int _x, int _y)
    {
        int xSize = gridInfo.xSize;
        int ySize = gridInfo.ySize;
        int[,]  visitedGrid = new int[xSize,ySize];
        int[,] obstacles = gridInfo.grid;

        for (int i = 0; i < gridInfo.xSize; i++)
        {
            for (int j = 0; j < gridInfo.ySize; j++)
            {
                visitedGrid[i, j] = -1;
            }
        }

        visitedGrid[_x,_y-1] = 0;
        visitedGrid[target[0], target[1]] = -2;       
        int z = 0;
        bool complete = false;

        do
        {
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (visitedGrid[i, j] == z)
                    {
                        if (i + 1 < xSize)
                        { if (obstacles[i + 1, j] == 0 && visitedGrid[i + 1, j] == -1)
                            {
                                visitedGrid[i + 1, j] = z + 1; ;
                            }
                            else if (visitedGrid[i + 1, j] == -2)
                            {
                                complete = true;
                            }
                        };

                        if (i - 1 >= 0)
                        {
                            if (obstacles[i - 1, j] == 0 && visitedGrid[i - 1, j] == -1)
                            {
                                visitedGrid[i - 1, j] = z + 1; ;
                            }
                            else if (visitedGrid[i - 1, j] == -2)
                            {
                                complete = true;
                            }
                        };

                        if (j + 1 < ySize)
                        {
                            if (obstacles[i, j + 1] == 0 && visitedGrid[i, j + 1] == -1)
                            {
                                visitedGrid[i, j + 1] = z + 1; ;
                            }
                            else if (visitedGrid[i, j + 1] == -2)
                            {
                                complete = true;
                            }
                        };

                        if (j - 1 >= 0)
                        {
                            if (obstacles[i, j - 1] == 0 && visitedGrid[i, j - 1] == -1)
                            {
                                visitedGrid[i, j - 1] = z + 1; ;
                            }
                            else if (visitedGrid[i, j - 1] == -2)
                            {
                                complete = true;
                            }
                        };
                    };

                };
            }
            
            z++;
        }
        while (z < gridInfo.xSize * gridInfo.ySize && !complete);

        for (int i = 0; i < ySize; i++)
        {
            string rowString = "(";

            for (int j = 0; j < xSize; j++)
            {
                rowString += (visitedGrid[j, i]).ToString() + ", ";
            }
           Debug.Log("Row:" + i + " contains " + rowString.Substring(0, rowString.Length - 2) + ")");
        }
        //Debug.Log("Got Here");

        int safety = 0;
        z++;
        int dir = 0;
        complete = false;
        List<int> path = new List<int> ();

        do
        {
            Debug.Log(target[0] + 1 + " " +  target[1]);
            if (target[0] + 1 < xSize)
            {

                if (visitedGrid[target[0] + 1, target[1]] < z && visitedGrid[target[0] + 1, target[1]] > -1)
                {
                    z = visitedGrid[target[0] + 1, target[1]];
                    dir = 4;
                }
            };

            if (target[1] + 1 < ySize)
            {

                if (visitedGrid[target[0], target[1] + 1] < z && visitedGrid[target[0], target[1] + 1] > -1)
                {

                    z = visitedGrid[target[0], target[1] + 1];
                    dir = 3;
                };
            };

            if (target[0] - 1 >= 0)
            {

                if (visitedGrid[target[0] - 1, target[1]] < z && visitedGrid[target[0] - 1, target[1]] > -1)
                {

                    z = visitedGrid[target[0] - 1, target[1]];
                    dir = 2;
                }
            };

            if (target[1] - 1 >= 0)
            {

                if (visitedGrid[target[0], target[1] - 1] < z && visitedGrid[target[0], target[1] - 1] > -1)
                {

                    z = visitedGrid[target[0], target[1] - 1];
                    dir = 1;
                };
            };

            if (dir == 1) { target[1]--; };
            if (dir == 2) { target[0]--; };
            if (dir == 3) { target[1]++; };
            if (dir == 4) { target[0]++; };

            path.Add(dir);
            if (z == 0)
            {
                //Debug.Log("Got Here");
                //foreach (int number in path){Debug.Log(number);}
                complete = true;
                PaintPath(path, (int)transform.position.x, (int)transform.position.y);
                return path;
            }
            safety++;
        } while (complete != true && safety < 100);

        return null;
    }

    void PaintPath(List<int> _path, int _x, int _y)
    {
        //Debug.Log("got here");
        int[] coordinates = new int[2] { _x, _y };
        for (int i = 1; i <= _path.Count; i++)
        {
            int dir = _path[_path.Count - i];

            if (dir == 1) { coordinates[1]++; };
            if (dir == 2) { coordinates[0]++; };
            if (dir == 3) { coordinates[1]--; };
            if (dir == 4) { coordinates[0]--; };
            //Debug.Log(dir + " + (" + coordinates[0] +"," + coordinates[1] + ")");
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(coordinates[0], -0.5f, coordinates[1]), Vector3.up, out hit, 1f, floorMask, QueryTriggerInteraction.UseGlobal))
            {

                hit.transform.GetComponent<FloorType>().floorType = 3;
            }
            Debug.DrawRay(new Vector3(coordinates[0], -0.5f, coordinates[1]), transform.up, Color.yellow, 100f);
        }
    }
}
