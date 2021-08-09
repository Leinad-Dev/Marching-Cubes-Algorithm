using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    GridElement lastHitGridElement;
    private float cubeOffset = .4f; //padding added to avoid clipping with building walls
    private Vector3 CubeSize = new Vector3(1f, 1f, 1f);



    private void Start()
    {
        //disable cube cursor on start, enable when we find a collider
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit) && hit.collider.tag == "gridElement")
        {

            //re-enable our cube visuals if they had previously been turned off
            if(gameObject.GetComponent<Canvas>().enabled == false)
            {
                gameObject.GetComponent<Canvas>().enabled = true;
            }
            


            transform.position = hit.collider.transform.position;
            lastHitGridElement = hit.collider.gameObject.GetComponent<GridElement>();

            //check if right click mouse button has been clicked (delete)
            if (Input.GetMouseButtonDown(1))
            {
                SetCurserButton(0);

            }

            UpdateCursorCubeSize();
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
                if (lastHitCoord.y < height - 1)
                {
                    LevelGenerator.instance.gridElements[((lastHitCoord.y+1) * ((width * depth))) + ((lastHitCoord.x) * depth) + lastHitCoord.z].EnableELement();
                }
                break;
        }
    }

    private void UpdateCursorCubeSize()
    {
        //we want to maintain our .1 offset of cube. 
        //cube scale 1.1, so we mult that by our element size on y axis to retain our offset of .1 
        GetComponent<RectTransform>().localScale = new Vector3((CubeSize.x + cubeOffset),
                                                       ((CubeSize.y + cubeOffset) * lastHitGridElement.gameObject.transform.localScale.y),
                                                       (CubeSize.z + cubeOffset));






    }
}
