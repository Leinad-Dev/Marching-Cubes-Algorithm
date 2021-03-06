using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;
    [Range(1, 100)]
    public int width;
    [Range(1, 100)]
    public int height;
    [Range(1, 100)]
    public int depth;
    public GridElement gridElement;
    public CornerElement cornerElement;
    public GridElement[] gridElements;
    public CornerElement[] cornerElements;

    private float floorHeight = .125f;
    private float basementHeight = 1.5f;
    private float elementHeight;

    private void Awake()
    {
        if (instance == null) //no instance of level generator in game.
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject); //already exists in scene, destroy this copy.
        }
    }

    private void Start()
    {

        /*basementHeight = 1.5f - (floorHeight/2);*/
 
        
        gridElements = new GridElement[width * depth * height]; //set length of array
        cornerElements = new CornerElement[(width + 1) * (depth + 1) * (height + 1)]; //set length of array



        for (int y = 0; y < height+1; y++)
        {
            for (int x = 0; x < width+1; x++)
            {
                for (int z = 0; z < depth+1; z++)
                {
                    CornerElement cornerElementInstance = Instantiate(cornerElement, Vector3.zero, Quaternion.identity, this.transform);
                    cornerElementInstance.InitializeCornerElement(x, y, z);                       
                    cornerElements[(y * ((width+1) * (depth+1))) + (x * (depth+1)) + z] = cornerElementInstance;
                }
            }
        }


        for (int y =0; y < height; y++)
        {
            float yPos = y; //using this to get center pivot value of basement since it's pivot y value is different than other floors
            if (y == 0) //floor plane
            {
                elementHeight = floorHeight*2;
            }
            else if (y == 1)
            {
                elementHeight = basementHeight;
                yPos = ((floorHeight/2) +   //y pivot/center location of floor
                       (basementHeight/2))-(floorHeight/2); //y pivot/center location of basement
            }
            else
            {
                elementHeight = 1;
            }

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    GridElement gridElementInstance = Instantiate(gridElement, new Vector3(x,yPos,z),Quaternion.identity,this.transform);
                    gridElementInstance.InitializeElement(x, y, z, elementHeight);
                    gridElements[(y*(width*depth)) + (x*depth) + z] = gridElementInstance; //transforms our 3D array into a 1D array.  

                }
            }
        }

        foreach (CornerElement corner in cornerElements)
        {
            corner.SetNearGridElements();
        }

        foreach (GridElement ge in gridElements)
        {
            ge.EnableELement();
        }
    }

}
