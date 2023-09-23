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
        }
    }

    void Move() {
        UnitSelections.Instance.unitsSelected[0].transform.position = Vector3.MoveTowards(UnitSelections.Instance.unitsSelected[0].transform.position, targetPosition, speed * Time.deltaTime);
        Debug.Log(UnitSelections.Instance.unitsSelected[0].transform.position);
        if (UnitSelections.Instance.unitsSelected[0].transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    
}
