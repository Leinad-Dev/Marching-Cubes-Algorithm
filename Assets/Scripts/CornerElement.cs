using UnityEngine;

public class CornerElement : MonoBehaviour
{
    private Coordinates coordinatesCornerElement;

    public GridElement[] nearbyGridCornerNode = new GridElement[8];

    public void InitializeCornerElement(int setX, int setY, int setZ)
    {
        coordinatesCornerElement = new Coordinates(setX, setY, setZ);
        this.name = "CE_" + coordinatesCornerElement.x + "_" + coordinatesCornerElement.y + "_" + coordinatesCornerElement.z;
    }

    public void SetCornerPosition(float setX, float setY, float setZ)
    {
        this.transform.position = new Vector3(setX, setY, setZ);
    }

    public void SetNearGridElements()
    {
        int width = LevelGenerator.instance.width;
        int height = LevelGenerator.instance.height;
        int depth = LevelGenerator.instance.depth;
        Coordinates coord = coordinatesCornerElement; //shortening the long name for clarity below

        if(coord.x < width && coord.y < height && coord.z < depth)
        {
            //UpperNorthEast
            //Pass GridElementCube script to the connected corner node
            nearbyGridCornerNode[0] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + (coord.x * depth) + coord.z];
        }

        if (coord.x > 0 && coord.y < height && coord.z < depth)
        {
            //UpperNorthWest
            //Pass GridElementCube script to the connected corner node
            nearbyGridCornerNode[1] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + ((coord.x-1) * depth) + coord.z];
        }

        if (coord.x > 0 && coord.y < height && coord.z > 0)
        {
            //LowerNorthWest
            //Pass GridElementCube script to the connected corner node
            nearbyGridCornerNode[2] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + ((coord.x - 1) * depth) + (coord.z - 1)];

        }

        if (coord.x < width && coord.y < height && coord.z > 0)
        {
            //LowerNorthEast
            //Pass GridElementCube script to the connected corner node
            nearbyGridCornerNode[3] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + (coord.x * depth) + (coord.z - 1)];
        }

        if (coord.x < width && coord.y > 0 && coord.z < depth)
        {
            //UpperSouthEast
            nearbyGridCornerNode[4] = LevelGenerator.instance.gridElements[((coord.y - 1) * (width * depth)) + ((coord.x) * depth) + coord.z];
        }

        if (coord.x > 0 && coord.y > 0 && coord.z < depth)
        {
            //UpperSouthWest
            nearbyGridCornerNode[5] = LevelGenerator.instance.gridElements[(((coord.y - 1)) * (width * depth)) + (((coord.x - 1)) * depth) + coord.z];

        }

        if (coord.x > 0 && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthWest
            nearbyGridCornerNode[6] = LevelGenerator.instance.gridElements[(((coord.y - 1)) * (width * depth)) + (((coord.x - 1)) * depth) + (coord.z - 1)];

        }

        if (coord.x < width && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthEast
            nearbyGridCornerNode[7] = LevelGenerator.instance.gridElements[((coord.y - 1) * (width * depth)) + ((coord.x) * depth) + (coord.z - 1)];
        }






    }
}
