using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    private Vector3 pointerDisplace = Vector3.zero;
    private Vector3 displ;
    private Vector3 pointerDisplacement;
    private Vector3 transformScreen;
    private Vector3 screenSpace;
    private Vector3 offset;

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
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

    }

     void OnMouseDrag()
    {

        //keep track of the mouse position
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        //update the position of the object in the world
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        //logic 
    }

}
