using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float attack;
    [SerializeField]
    private float defense;
    [SerializeField]
    private float healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
