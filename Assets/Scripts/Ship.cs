using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float visualRange;
    protected bool visible;
    public float scanRange;
    public float scanSize;
    public List<GameObject> scannedFoes;
    [SerializeField]
    private float attack;
    [SerializeField]
    private float defense;
    [SerializeField]
    private float healthPoints;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) 
        {
            Move();
        }
    }

    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    protected void Awake()
    {
        scannedFoes = new List<GameObject>();
        if (!CompareTag("AI"))
        {
            Transform range = transform.Find("ShipVisualRange");
            range.localScale = new Vector3(visualRange, visualRange);
        }
    }
    protected void FixedUpdate()
    {
        Scan();
    }

    void Move() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }


    protected void Scan()
    {
        string tagToFind;
        if (CompareTag("AI"))
            tagToFind = "Player";
        else
            tagToFind = "AI";

        GameObject[] foes = GameObject.FindGameObjectsWithTag(tagToFind);
        foreach (GameObject foe in foes)
        {
            float distance = Vector3.Distance(transform.position, foe.transform.position);
            float foeScanSize = foe.GetComponent<Ship>().scanSize;
            float result = scanRange * foeScanSize / distance;
            if (result >= 1)
            {
                if (tagToFind == "AI")
                {
                    if (scannedFoes.Contains(foe))
                    {
                        Transform indicator = transform.Find("Indicator");
                        //float angle = Vector3.Angle(transform.position, foe.transform.position);
                        //Debug.Log("angle = " + angle);
                        //indicator.eulerAngles = new Vector3(0,0,angle);
                        Vector3 dir = foe.transform.position - transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
                        indicator.eulerAngles = new Vector3(0, 0, angle);
                        Debug.Log("angle = " + angle);
                        Debug.Log("dir = " + dir.y+" "+dir.x);
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }
    }

    void attackShip(Ship ship) {
        
    }

    void getDamage(float damage) 
    {
        if ((damage - defense) <= 0) {
            damage = 0;
        } else {
            damage -= defense;
        }

        this.healthPoints -= damage;
        
    }




}
