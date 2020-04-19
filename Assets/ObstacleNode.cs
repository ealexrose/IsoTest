using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNode 
{
    readonly int tileEffectSize = 1;
    readonly int effectValuesSize = 0;

    public bool open;
    //Spreads Sickness
    public bool[] tileEffects;
    public int[] effectValues;
    public int weight;
    public int visited;
    int tileType;

    public ObstacleNode()
    {

        open = true;
        tileEffects = new bool[tileEffectSize];
        effectValues = new int[effectValuesSize];
        weight = 10;
        visited = -1;
        tileType = 0;
    }
    public ObstacleNode(int _tileType)
    {
        open = true;
        tileEffects = new bool[tileEffectSize];
        effectValues = new int[effectValuesSize];
        weight = 10;
        visited = -1;
        SetTileType(_tileType);
    }
    public void SetTileType(int _tileType)
    {
        switch(_tileType)
        {
            case 1:
                open = false;
                tileEffects = new bool[tileEffectSize];
                effectValues = new int[effectValuesSize];
                weight = 10;
                visited = -1;
                tileType = 1;
                break;
            case 0:
            default:
                open = true;
                tileEffects = new bool[tileEffectSize];
                effectValues = new int[effectValuesSize];
                weight = 10;
                visited = -1;
                tileType = 0;
                break;
        }
    }
}
