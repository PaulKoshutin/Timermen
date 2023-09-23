using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float maxZoom;
    public float minZoom;
    public float sensityvity;

    private Vector3 Origin;
    private Vector3 Difference;
    private bool drag;

    private void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                Origin = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            
        } else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void ZoomCamera(float increment)
    {
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize - increment * sensityvity, minZoom, maxZoom);
    }
}
