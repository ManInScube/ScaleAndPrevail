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

    private float attackSpeed = 1.1f;
    private float attackDamage = 10f;
    public float attackRange = 2f;

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

            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                return;
            }
            else
            {
                Attack();
                transform.position = transform.position;
            }

        }

        //if ((agent.destination - this.transform.position).sqrMagnitude <= 1f)
        if (agent.destination == this.transform.position)
        {
            OnAgentStopped?.Invoke();
           // Debug.Log(gameObject.name + " stopped");
        }
    }


    protected void OnEnable()
    {
        UnitMovement.TargetAction += TargetHandler;
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
        //transform.LookAt(target.transform.position);
        //agent.ResetPath();
        agent.isStopped = true;
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
