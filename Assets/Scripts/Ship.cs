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
    public List<string> knownFoes;
    public float attack;
    public float healthPoints;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private float speed = 3.0f;
    public float maxRechargeTime;
    public float rechargeTime = 0;

    public GameObject indicatorPrefab;
    public GameObject ammoPrefab;
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
        OpenFire();
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
        if (rechargeTime > 0)
            rechargeTime -= 1;
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

        for (int i=0;i<scannedFoes.Count;i++)
            if (scannedFoes[i] == null)
            {
                string name = knownFoes[i];
                scannedFoes.RemoveAt(i);
                Destroy(transform.Find("Indicator" + name).gameObject);
                knownFoes.RemoveAt(i);
                break;
            }

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
                        Vector2 dir = foe.transform.position - transform.position;
                        dir = foe.transform.InverseTransformDirection(dir);
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                        angle += foe.transform.eulerAngles.z;
                        float randomError = Random.Range(-width * 5, width * 5);
                        angle += randomError;
                        indicator.eulerAngles = new Vector3(0, 0, angle);
                        indicator.transform.eulerAngles = new Vector3(0, 0, angle);
                    }
                    else
                    {
                        scannedFoes.Add(foe);
                        knownFoes.Add(foe.name);
                        GameObject indicator = Instantiate(indicatorPrefab, transform) as GameObject;
                        float width = 2 / result;
                        if (width < 0.05)
                            width = 0.05f;
                        indicator.GetComponentInChildren<SpriteRenderer>().size = new Vector2(width, 1f);
                        indicator.transform.parent = transform;
                        indicator.name = "Indicator" + foe.name;
                        Vector2 dir = foe.transform.position - transform.position;
                        dir = foe.transform.InverseTransformDirection(dir);
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                        angle += foe.transform.eulerAngles.z;
                        float randomError = Random.Range(-width * 5, width * 5);
                        angle += randomError;
                        indicator.transform.eulerAngles = new Vector3(0, 0, angle);
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
            //StopAllCoroutines();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Transform gun = transform.Find("GunSlot");
            Vector3 dir = mousePosition - transform.position;
            dir = transform.InverseTransformDirection(dir);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            gun.eulerAngles = new Vector3(0, 0, angle);
            //StartCoroutine(Rotate(angle, gun));
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
    void OpenFire()
    {
        if (CompareTag("Player"))
        {
            if (Input.GetMouseButtonDown(1) && rechargeTime == 0)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Transform gun = transform.Find("GunSlot");
                float distance = Vector3.Distance(transform.position, mousePosition);
                GameObject ammo = Instantiate(ammoPrefab) as GameObject;
                ammo.transform.eulerAngles = new Vector3(0, 0, gun.eulerAngles.z);
                ammo.transform.position = transform.position;
                ammo.GetComponent<Ammo>().damage = attack;
                ammo.GetComponent<Ammo>().initialDistance = distance;
                ammo.GetComponent<Ammo>().target = mousePosition;
                rechargeTime = maxRechargeTime;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ammo")
        {
            getDamage(collision.gameObject.GetComponent<Ammo>().damage);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "AI" || collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void getDamage(float damage)
    {
        this.healthPoints -= damage;
        if (healthPoints <= 0)
            Destroy(gameObject);
    }
}