using UnityEngine;


public class Coordinates //level generator coordinates class 
{
    public int x, y, z;

    //initialize class with contstructor below
    public Coordinates(int setX, int setY, int setZ)
    {
        x = setX;
        y = setY;
        z = setZ;
    }
}


public class GridElement : MonoBehaviour
{

    private Coordinates coord;
    private Collider col;
    private Renderer rend;
    private bool isEnable;
    private float elementHeight;
    public CornerElement[] corners = new CornerElement[8];

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Random.Range(1, 100) < 50)
        {

        }
    }
    public void InitializeElement(int setX, int setY, int setZ, float setElementHeight)
    {

        int width = LevelGenerator.instance.width;
        int height = LevelGenerator.instance.height;
        int depth = LevelGenerator.instance.depth;

        coord = new Coordinates(setX, setY, setZ);
        name = "GE_" + coord.x + "_" + coord.y + "_" + coord.z;

        this.elementHeight = setElementHeight;
        this.transform.localScale = new Vector3(1f, elementHeight, 1f);

        col = GetComponent<Collider>();
        rend = GetComponent<MeshRenderer>();

        //arranging corner elements into array
        corners[0] = LevelGenerator.instance.cornerElements[ (coord.y*((width + 1)*(depth + 1)))  +  (coord.x*(depth + 1))  +  coord.z ]; //0
        corners[1] = LevelGenerator.instance.cornerElements[ (coord.y*((width + 1)*(depth + 1)))  +  ((coord.x+1)*(depth + 1))  +  coord.z ]; //x +1
        corners[2] = LevelGenerator.instance.cornerElements[ (coord.y * ((width + 1) * (depth + 1))) + (coord.x * (depth + 1)) + (coord.z+1)]; //z +1
        corners[3] = LevelGenerator.instance.cornerElements[ (coord.y * ((width + 1) * (depth + 1))) + ((coord.x+1)* (depth + 1)) + (coord.z+1)]; //z +1 && x +1
        corners[4] = LevelGenerator.instance.cornerElements[((coord.y+1) * ((width + 1) * (depth + 1))) + (coord.x * (depth + 1)) + coord.z]; //0
        corners[5] = LevelGenerator.instance.cornerElements[((coord.y + 1) * ((width + 1) * (depth + 1))) + ((coord.x + 1) * (depth + 1)) + coord.z]; //x +1
        corners[6] = LevelGenerator.instance.cornerElements[((coord.y + 1) * ((width + 1) * (depth + 1))) + (coord.x * (depth + 1)) + (coord.z + 1)]; //z +1
        corners[7] = LevelGenerator.instance.cornerElements[((coord.y + 1) * ((width + 1) * (depth + 1))) + ((coord.x + 1) * (depth + 1)) + (coord.z + 1)]; //z +1 && x +1

        //need to manually refresh the collider bounds by turning col off and on, otherwise our bound values don't update before we use them to position corners
        col.enabled = false;
        col.enabled = true;

        //positioning of corner elements
        corners[0].SetCornerPosition(col.bounds.min.x, col.bounds.min.y, col.bounds.min.z);
        corners[1].SetCornerPosition(col.bounds.max.x, col.bounds.min.y, col.bounds.min.z);
        corners[2].SetCornerPosition(col.bounds.min.x, col.bounds.min.y, col.bounds.max.z);
        corners[3].SetCornerPosition(col.bounds.max.x, col.bounds.min.y, col.bounds.max.z);
        corners[4].SetCornerPosition(col.bounds.min.x, col.bounds.max.y, col.bounds.min.z);
        corners[5].SetCornerPosition(col.bounds.max.x, col.bounds.max.y, col.bounds.min.z);
        corners[6].SetCornerPosition(col.bounds.min.x, col.bounds.max.y, col.bounds.max.z);
        corners[7].SetCornerPosition(col.bounds.max.x, col.bounds.max.y, col.bounds.max.z);
    }

    public Coordinates GetCoord()
    {
        return coord;
    }

    public void EnableELement()
    {
        this.isEnable = true;
        this.rend.enabled = true;
        this.col.enabled = true;

        foreach(CornerElement ce in corners)
        {
            ce.SetCornerElement();
        }

    }

    public void DisableElement()
    {
        this.isEnable = false;
        this.rend.enabled = false;
        this.col.enabled = false;

        foreach (CornerElement ce in corners)
        {
            ce.SetCornerElement();
        }
    }

    public bool GetEnabledOrDisabledStatus()
    {
        return isEnable;
    }

}
