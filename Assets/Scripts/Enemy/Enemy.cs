using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float attackSpeed = 1.1f;
    private float attackDamage = 10f;
    private float attackRange = 5f;

    public float health;
    public float maxHealth = 100f;

    public enum EnemyType
    {
        Melee,
        Range,
        King
    }

    public EnemyType enemyType;


    void Start()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        Attack();
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);

        Unit unit = hitColliders[0].GetComponent<Unit>();
        float dps = attackSpeed * attackDamage * Time.deltaTime;

        if (unit)
        {
            transform.LookAt(unit.transform);
            unit.ReceiveDamage(dps);
            Debug.Log("Enemy is attacking");
        }

    }

    public void ReceiveDamage(float damage)
    {
        if (health >= 0)
            health -= damage;
        else
            Destroy(gameObject, 1f);
        //Debug.Log("DIED");

        Debug.Log(health);

    }
}
