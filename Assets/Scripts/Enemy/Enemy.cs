using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Enemy : MonoBehaviour
{

    [Header("Attack")]
    [SerializeField] private float attackSpeed = 1.1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 5f;

    public float health;
    public float maxHealth = 100f;

    public bool isDead;

    public static event Action OnKingsDead;
    public static event Action OnEnemyDead;

    public event Action OnAgentStopped;
    public event Action OnAttack;
    public event Action OnWalk;

    private Unit currentTarget = null;
    Collider[] units;


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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        float dps = attackSpeed * attackDamage * Time.deltaTime;
        
       for (int i = 0; i <= hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Unit>())
            {
                currentTarget = hitColliders[i].GetComponent<Unit>();
                break;
            }
        }


        if (currentTarget)
        {
            OnAttack?.Invoke();
            transform.LookAt(currentTarget.transform);
            currentTarget.ReceiveDamage(dps);
            Debug.Log("Enemy is attacking");
        }

        else
        {
            return;
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
                //OnEnemyDead?.Invoke();
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

    private void MoveToUnit()
    {

    }

/*    private void OnTriggerEnter(Collider other)
    {
        Unit kek = other.GetComponent<Unit>();
        if (kek)
        {

        }
    }*/


    //скрипт которые подбирает и настраивает скрипыт при усыновлении в отряд
}
