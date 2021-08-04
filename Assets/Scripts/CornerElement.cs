using UnityEngine;

public class CornerElement : MonoBehaviour
{
    private Coordinates coordinatesCornerElement;

    public void InitializeCornerElement(int setX, int setY, int setZ)
    {
        coordinatesCornerElement = new Coordinates(setX, setY, setZ);
        this.name = "CE_" + coordinatesCornerElement.x + "_" + coordinatesCornerElement.y + "_" + coordinatesCornerElement.z;
    }

    public void SetCornerPosition(float setX, float setY, float setZ)
    {
        this.transform.position = new Vector3(setX, setY, setZ);
    }
}
