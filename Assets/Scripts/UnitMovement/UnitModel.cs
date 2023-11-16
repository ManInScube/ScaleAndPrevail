using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitModel : MonoBehaviour
{
    public float attackSpeed = 1.1f;
    public float attackDamage = 10f;
    private float attackRange = 5f;

    private float dps;

    private bool isAttacking = false;
    private Enemy enemy;
    public Enemy target = null;

    private UnitMovement um;


    void Start()
    {
        
    }

    private void OnEnable()
    {
        UnitMovement.EnemyAction += TargetHandler;
    }

    private void TargetHandler(Enemy en)
    {
        target = en;
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        /*        if (isAttacking)
                {
                    Attack();
                }*/

        if (target!=null)
        {
            
            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                return;
            }
            else
            {
                Attack();
            }

        }
    }

    private void Attack()
    {
            
            dps = attackSpeed * attackDamage * Time.deltaTime;
            target.GetComponent<Enemy>().ReceiveDamage(dps);
            Debug.Log("Attacking");
        
        //return attackDamage * attackSpeed * Time.deltaTime;
    }
}
