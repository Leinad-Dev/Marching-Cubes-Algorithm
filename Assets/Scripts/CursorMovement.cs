using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    GridElement lastHitGridElement;



    private void Start()
    {
        
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit) && hit.collider.tag == "gridElement")
        {
/*            Debug.Log("Just hit object named: " + hit.collider.name);*/
            transform.position = hit.collider.transform.position;
            lastHitGridElement = hit.collider.gameObject.GetComponent<GridElement>();

            //check if right click mouse button has been clicked (delete)
            if (Input.GetMouseButton(1))
            {
                SetCurserButton(0);

            }
        }
    }

    public void SetCurserButton(int input)
    {
        Coordinates lastHitCoord = lastHitGridElement.GetCoord();
        int width = LevelGenerator.instance.width;
        int height = LevelGenerator.instance.height;
        int depth = LevelGenerator.instance.depth;

        switch (input)
        {
            case 0:
                //remove GridElement;
                if(lastHitCoord.y > 0)
                {
                    lastHitGridElement.DisableElement();
                }

                break;
            case 1:
                //add X+
                if(lastHitCoord.x < width - 1)
                {
                    LevelGenerator.instance.gridElements[(lastHitCoord.y * ((width * depth))) + ((lastHitCoord.x +1) * depth) + lastHitCoord.z].EnableELement();
                }
                
                break;
            case 2:
                //add X-
                if (lastHitCoord.x > 0)
                {
                    LevelGenerator.instance.gridElements[(lastHitCoord.y * ((width * depth))) + ((lastHitCoord.x - 1) * depth) + lastHitCoord.z].EnableELement();
                }
                break;
            case 3:
                //add z+
                if (lastHitCoord.z < depth - 1)
                {
                    LevelGenerator.instance.gridElements[(lastHitCoord.y * ((width * depth))) + ((lastHitCoord.x) * depth) + lastHitCoord.z+1].EnableELement();
                }
                break;
            case 4:
                //add z-
                if (lastHitCoord.z > 0)
                {
                    LevelGenerator.instance.gridElements[(lastHitCoord.y * ((width * depth))) + ((lastHitCoord.x) * depth) + (lastHitCoord.z - 1)].EnableELement();
                }
                break;
            case 5:
                //add y+
                if (lastHitCoord.z < height - 1)
                {
                    LevelGenerator.instance.gridElements[((lastHitCoord.y+1) * ((width * depth))) + ((lastHitCoord.x) * depth) + lastHitCoord.z].EnableELement();
                }
                break;
        }
    }
}
