using UnityEngine;

public class CornerElement : MonoBehaviour
{
    public Material buildingMat;
    public Material floorMat;
    private Coordinates coordinatesCornerElement;

    public GridElement[] nearbyGridElements = new GridElement[8];
    public int bitMaskValue;


    private MeshFilter meshFilter;





    public void InitializeCornerElement(int setX, int setY, int setZ)
    {
        coordinatesCornerElement = new Coordinates(setX, setY, setZ);
        this.name = "CE_" + coordinatesCornerElement.x + "_" + coordinatesCornerElement.y + "_" + coordinatesCornerElement.z;
        meshFilter = this.GetComponent<MeshFilter>();

/*        //swap material and scale cornerCircle to wall
        this.gameObject.transform.localScale = new Vector3(1,1,1);//originally we scaled our corner circle to 0.1, now that we are swapping it with a mesh lets set the scale back to 1
  */
    }

    public void SetCornerPosition(float setX, float setY, float setZ)
    {
        this.transform.position = new Vector3(setX, setY, setZ);
    }

    public void SetCornerElement()
    {
        bitMaskValue = BitMask.GetBitMask(nearbyGridElements);
        meshFilter.mesh = CornerMeshes.instance.GetCornerMesh(bitMaskValue, coordinatesCornerElement.y);
        SetCornerMeshMaterial(coordinatesCornerElement.y);
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
            nearbyGridElements[0] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + (coord.x * depth) + coord.z];
        }

        if (coord.x > 0 && coord.y < height && coord.z < depth)
        {
            //UpperNorthWest
            //Pass GridElementCube script to the connected corner node
            nearbyGridElements[1] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + ((coord.x-1) * depth) + coord.z];
        }

        if (coord.x > 0 && coord.y < height && coord.z > 0)
        {
            //LowerNorthWest
            //Pass GridElementCube script to the connected corner node
            nearbyGridElements[2] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + ((coord.x - 1) * depth) + (coord.z - 1)];

        }

        if (coord.x < width && coord.y < height && coord.z > 0)
        {
            //LowerNorthEast
            //Pass GridElementCube script to the connected corner node
            nearbyGridElements[3] = LevelGenerator.instance.gridElements[(coord.y * (width * depth)) + (coord.x * depth) + (coord.z - 1)];
        }

        if (coord.x < width && coord.y > 0 && coord.z < depth)
        {
            //UpperSouthEast
            nearbyGridElements[4] = LevelGenerator.instance.gridElements[((coord.y - 1) * (width * depth)) + ((coord.x) * depth) + coord.z];
        }

        if (coord.x > 0 && coord.y > 0 && coord.z < depth)
        {
            //UpperSouthWest
            nearbyGridElements[5] = LevelGenerator.instance.gridElements[(((coord.y - 1)) * (width * depth)) + (((coord.x - 1)) * depth) + coord.z];

        }

        if (coord.x > 0 && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthWest
            nearbyGridElements[6] = LevelGenerator.instance.gridElements[(((coord.y - 1)) * (width * depth)) + (((coord.x - 1)) * depth) + (coord.z - 1)];

        }

        if (coord.x < width && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthEast
            nearbyGridElements[7] = LevelGenerator.instance.gridElements[((coord.y - 1) * (width * depth)) + ((coord.x) * depth) + (coord.z - 1)];
        }






    }

    private void SetCornerMeshMaterial(int floorLevel)
    {
        if(this.gameObject.transform.position.y < .1f)//floor
        {
            this.gameObject.GetComponent<MeshRenderer>().material = floorMat;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = buildingMat;
        }

    }
}
