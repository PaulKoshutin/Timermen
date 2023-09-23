using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    public LayerMask water;
    private float speed = 10.0f;
    public bool isMoving = false;
    private Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition); 

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, water)) 
            {
                targetPosition = hit.point;
                isMoving = true;
                Debug.Log(isMoving); 
            }
        }

        if (isMoving) 
        {
            Move();
            RotateToTarget();
        }
    }

    void RotateToTarget() 
    {
        Vector3 dir = targetPosition - UnitSelections.Instance.unitsSelected[0].transform.position;
        //dir = transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        StartCoroutine(Rotate(angle, UnitSelections.Instance.unitsSelected[0].transform));
    }

    IEnumerator Rotate(float targetAngle, Transform obj)
    {
        while (obj.rotation.z != targetAngle)
        {
            obj.rotation = Quaternion.Slerp(obj.rotation, Quaternion.Euler(0f, 0f, targetAngle), 3f * Time.deltaTime);
            yield return null;
        }
        obj.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        yield return null;
    }

    void Move() 
    {
        UnitSelections.Instance.unitsSelected[0].transform.position = Vector3.MoveTowards(UnitSelections.Instance.unitsSelected[0].transform.position, targetPosition, speed * Time.deltaTime);
        Debug.Log(UnitSelections.Instance.unitsSelected[0].transform.position);
        if (UnitSelections.Instance.unitsSelected[0].transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    
}
