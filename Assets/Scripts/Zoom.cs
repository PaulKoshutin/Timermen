using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float maxZoom;
    public float minZoom;
    public float sensityvity;

    public float limitLeft;
    public float limitRigth;
    public float limitTop;
    public float limitBotton;

    private Vector3 Origin;
    private Vector3 Difference;
    private bool drag;

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
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

        Camera.main.transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, limitLeft, limitRigth),
            Mathf.Clamp(transform.position.y, limitBotton, limitTop),
            transform.position.z);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void ZoomCamera(float increment)
    {
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize - increment * sensityvity, minZoom, maxZoom);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(limitLeft, limitTop),
            new Vector2(limitRigth, limitTop)
            );

        Gizmos.DrawLine(
            new Vector2(limitLeft, limitBotton),
            new Vector2(limitRigth, limitBotton)
            );

        Gizmos.DrawLine(
            new Vector2(limitLeft, limitTop),
            new Vector2(limitLeft, limitBotton)
            );

        Gizmos.DrawLine(
            new Vector2(limitRigth, limitTop),
            new Vector2(limitRigth, limitBotton)
            );
    }
}
