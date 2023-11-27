using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    private float attackSpeed = 1.1f;
    private float attackDamage = 10f;
    private float attackRange = 5f;

    public float health;
    public float maxHealth = 100f;

    public bool isDead;

    public static event Action OnKingsDead;

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

   


    //сделать по аттак рендж как в castle defense или по OnCollisionEnter
    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);

        Unit unit = hitColliders[1].GetComponent<Unit>();
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
        {
            if (this.enemyType == EnemyType.King)
            {
                OnKingsDead?.Invoke();
            }
            isDead = true;
            Destroy(gameObject, 1f);
        }
        //Debug.Log("DIED");

        //Debug.Log(health);

    }

    public void DeleteEnemy()
    {
        Destroy(this);
    }
}
