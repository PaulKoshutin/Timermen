using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float damage;
    public float initialDistance;
    public float effectiveDistance;
    public Vector3 target;
    public string tagger;

    private void Start()
    {
        effectiveDistance = initialDistance;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 20 * Time.deltaTime);
        if (transform.position == target)
            Destroy(gameObject);
    }
}
