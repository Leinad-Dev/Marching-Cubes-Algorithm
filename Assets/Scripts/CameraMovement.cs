using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    private Vector3 previousPos;
    private bool camSet = false;
    //===Zoom===
    private float speed = 5f;
    private float min = 35f;
    private float max = 100f;
    private float sensitivity = 17f;
    private float fov;

    //===Panning===
    float mouseX;
    float mouseY;
    Vector3 cameraPos;


    void Update()
    {
/*        if(camSet == false)
        {
            CenterCameraPositionOnStart();
        }*/
        
        RotateCamera();
        Zoom();
        Panning();

    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            cam.transform.RotateAround(/*target.transform.position*/GetAveragePivots(), transform.up, Input.GetAxis("Mouse X") * speed);
            //cam.transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
        }


    }
    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            fov = cam.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
            fov = Mathf.Clamp(fov, min, max);
            cam.fieldOfView = fov;
        }

    }

    public void Panning()
    {
        if (Input.GetMouseButtonDown(2))
        {
            previousPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - previousPos;
            transform.Translate(delta.x * sensitivity, delta.y * sensitivity, 0);
            previousPos = Input.mousePosition;
        }
    }

    public Vector3 GetAveragePivots()
    {
        Vector3 sumVector = new Vector3(0f, 0f, 0f);

        foreach (Transform child in target.transform)
        {
            sumVector += child.position;
        }

        Vector3 groupCenter = sumVector / target.transform.childCount;

        return groupCenter;
    }

/*    public void CenterCameraPositionOnStart()
    {
        Vector3 tarCenter = GetAveragePivots();
        Debug.Log(GetAveragePivots());
        cam.transform.position = tarCenter *//*+ new Vector3(0f,6f,-8.5f)*//*;
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        //cam.transform.LookAt(tarCenter);
   
        camSet = true;

    }*/


}
