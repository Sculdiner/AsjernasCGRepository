using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    private Vector3 pointerDisplace = Vector3.zero;
    private Vector3 displ;
    private float dragDepth;
    private float posX;
    private float posZ;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        dragDepth = CameraPlane.CameraToPointDepth(Camera.main, transform.position);
    }

    void OnMouseDrag()
    {
        //Vector3 mousePos = MouseInWorldCoords();
        var screenMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var worldPos = CameraPlane.ScreenToWorldPlanePoint(Camera.main, dragDepth, screenMousePos);
        transform.position = worldPos;
    }

    void OnMouseUp()
    {
        //logic 
    }
}
