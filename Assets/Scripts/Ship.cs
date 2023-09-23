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
    private float attack;
    private float defense;
    private float healthPoints;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private float speed = 3.0f;

    public GameObject indicatorPrefab;
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
        CursorTrail();
    }

    void Move()
    {
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
            if (tagToFind == "AI")
            {
                if (result >= 1)
                {
                    if (scannedFoes.Contains(foe))
                    {
                        Transform indicator = transform.Find("Indicator" + foe.name);
                        float width = 2 / result;
                        if (width < 0.05)
                            width = 0.05f;
                        indicator.GetComponentInChildren<SpriteRenderer>().size = new Vector2(width, 1f);
                        Vector3 dir = foe.transform.position - transform.position;
                        dir = foe.transform.InverseTransformDirection(dir);
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                        float randomError = Random.Range(-width * 5, width * 5);
                        angle += randomError;
                        indicator.eulerAngles = new Vector3(0, 0, angle);
                        StartCoroutine(Rotate(angle, indicator.transform));
                    }
                    else
                    {
                        scannedFoes.Add(foe);
                        GameObject indicator = Instantiate(indicatorPrefab, transform) as GameObject;
                        float width = 2 / result;
                        if (width < 0.05)
                            width = 0.05f;
                        indicator.GetComponentInChildren<SpriteRenderer>().size = new Vector2(width, 1f);
                        indicator.transform.parent = transform;
                        indicator.name = "Indicator" + foe.name;
                        Vector3 dir = foe.transform.position - transform.position;
                        dir = foe.transform.InverseTransformDirection(dir);
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                        float randomError = Random.Range(-width * 5, width * 5);
                        angle += randomError;
                        //indicator.transform.eulerAngles = new Vector3(0, 0, angle);
                        StartCoroutine(Rotate(angle, indicator.transform));
                    }
                }
                else
                {
                    if (scannedFoes.Contains(foe))
                    {
                        scannedFoes.Remove(foe);
                        Destroy(transform.Find("Indicator" + foe.name).gameObject);
                    }
                }
            }
        }
    }
    protected void CursorTrail()
    {
        if (CompareTag("Player"))
        {
            StopAllCoroutines();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Transform gun = transform.Find("GunSlot");
            Vector3 dir = mousePosition - transform.position;
            dir = transform.InverseTransformDirection(dir);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            //gun.eulerAngles = new Vector3(0, 0, angle);
            StartCoroutine(Rotate(angle, gun));
        }
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
    void attackShip(Ship ship)
    {

    }

    void getDamage(float damage)
    {
        if ((damage - defense) <= 0)
        {
            damage = 0;
        }
        else
        {
            damage -= defense;
        }

        this.healthPoints -= damage;

    }
}