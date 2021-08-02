using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit) && hit.collider.tag == "gridElement")
        {
            Debug.Log("Just hit object named: " + hit.collider.name);
            transform.position = hit.collider.transform.position;
        }
    }
}
