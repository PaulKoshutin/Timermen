using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    private bool isMoving = false;
    private Vector3 targetPosition;
    private float speed = 3.0f;

    void OnMouseDown() 
    {
        object ship = this;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            SetTargetPosition();
        }

        if (isMoving) {
            Move();
        }
    }

    void SetTargetPosition() 
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        isMoving = true;
    }


    void Move() {
        transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
