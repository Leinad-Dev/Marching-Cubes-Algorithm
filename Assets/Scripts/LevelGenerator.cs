using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Range(1, 100)]
    public int width;

    [Range(1, 100)]
    public int height;

    [Range(1, 100)]
    public int depth;

    public GridElement gridElement;
    public GridElement[] gridElements;

    private void Start()
    {
        gridElements = new GridElement[width * depth * height];
   


        for(int y =0; y<height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    GridElement gridElementInstance = Instantiate(gridElement, new Vector3(x,y,z),Quaternion.identity,this.transform);
                    gridElementInstance.name = "gridElement" + "_(" + x + "," + y + "," + z + ")";

                    gridElements[x+width*(z+width*y)]=  gridElementInstance;
                    //[x+width*(z+width*y)] this transforms our 3D array into a 1D array.
                    //1D array allows us to easily access neighbor elements
                }
            }
        }
    }

}
