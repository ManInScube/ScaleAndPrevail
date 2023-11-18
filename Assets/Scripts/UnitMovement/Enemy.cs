using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float health;

    float maxHealth = 100;

    [SerializeField] uint enemiesCount;
    [SerializeField] uint mindStrength;
    private uint Complexity { get { return (uint)Mathf.Floor(uintsCount / enemiesCount * 10); } } //TODO: доработать сделать в string значение

    private uint uintsCount;

    void Start()
    {
        health = maxHealth;
    }

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

    public void WinCondition()
    {
        if (enemiesCount < enemiesCount / 2)
        {

        }
    }


}
