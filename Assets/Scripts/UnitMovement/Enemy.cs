using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float health;

    float maxHealth = 100;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float damage)
    {
        if(health >= 0)
            health -= damage;
        else
            Debug.Log("DIED");

        Debug.Log(health);

    }

    public void Die()
    {

    }
}
