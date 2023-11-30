using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

//UnitController
public class Unit : MonoBehaviour
{
    public NavMeshAgent agent;

    private Enemy enemy;
    public Enemy target = null;

    protected UnitModel model;

    public event Action OnAgentStopped;
    public event Action OnAttack;
    public event Action OnWalk;
    public event Action OnIdle;


    [Header("Attack")]
    [SerializeField] private float attackSpeed = 1.1f;
    [SerializeField]private float attackDamage = 10f;
    public float attackRange = 5f;

    UnitView view;

    public float health;
    public float maxHealth = 90f;

    public bool isAttacking;
   

    protected void Start()
    {
        health = maxHealth;
        enemy = GetComponent<Enemy>();
        isAttacking = false;

        try
        {
            view = GetComponent<UnitView>();
        }
        catch
        {
            Debug.Log("No view");
        }

    }

    protected void Update()
    {

        if (target != null)
        {

            if (Vector3.Distance(transform.position, target.transform.position) > attackRange || target.isDead)
            {
                return;
            }
            else
            {
                Attack();
            }

        }

        //if ((agent.destination - this.transform.position).sqrMagnitude <= 1f)
        if ((agent.destination - this.transform.position).sqrMagnitude <= 1f)
        {
            OnAgentStopped?.Invoke();
        }
    }


    protected void OnEnable()
    {
        UnitMovement.TargetAction += TargetHandler;
        Enemy.OnEnemyDead += EnemyDeadHandler;
    }

    private void EnemyDeadHandler()
    {
        target = null;
    }

    protected void TargetHandler(Enemy en)
    {
        target = en;
    }

    public void MoveToPoint(Vector3 dest)
    {
        OnWalk?.Invoke();
        agent.SetDestination(dest);
    }

    public void Attack()
    {
        OnAttack?.Invoke();
        float dps = attackSpeed * attackDamage * Time.deltaTime;
        target.GetComponent<Enemy>().ReceiveDamage(dps);
        transform.LookAt(target.transform.position.normalized); //new

        if (target.isDead)
        {
            OnIdle?.Invoke();
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
