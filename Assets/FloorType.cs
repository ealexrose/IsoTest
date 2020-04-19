using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FloorType : MonoBehaviour
{
    public Material[] floorVarients;
    public int floorType;
    public MeshRenderer meshRenderer;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log( floorVarients.Length);
        if (floorType < floorVarients.Length)
        {

            meshRenderer.material = floorVarients[floorType];
            //Debug.Log(floorVarients[floorType]);
        }

    }
}
