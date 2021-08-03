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

    public void InitializeElement(int setX, int setY, int setZ)
    {
        coord = new Coordinates(setX, setY, setZ);
        name = "GE_" + coord.x + "_" + coord.y + "_" + coord.z;
        col = GetComponent<Collider>();
        rend = GetComponent<MeshRenderer>();
    }

    public Coordinates GetCoord()
    {
        return coord;
    }

    public void EnableELement()
    {
        rend.enabled = true;
        col.enabled = true;

    }

    public void DisableElement()
    {
        rend.enabled = false;
        col.enabled = false;
    }

}
