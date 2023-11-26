using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitModel : MonoBehaviour
{
    public float attackSpeed = 1.1f;
    public float attackDamage = 10f;
    public float attackRange = 5f;

    //private float Dps { get; set; }

    private bool isAttacking = false;
/*    private Enemy enemy;
    public Enemy target = null;
*/

    //private Animator animator;

    private int Hp { get; set; }
    private int MaxHp { get; set; }


    public UnitModel(int maxHp)
    {
        MaxHp = maxHp;
    }

    public enum Type
    {
        MeleeHuman,
        RangeHuman,
        MeleeMech,
        RangeMech
    }

     Type unitType;

    void Start()
    {

    }

/*    private void OnEnable()
    {
        UnitMovement.TargetAction += TargetHandler;
    }

    private void TargetHandler(Enemy en)
    {
        target = en;
    }*/

    private void Awake()
    {
        //enemy = GetComponent<Enemy>();
    }

    void Update()
    {
/*        if (target!=null)
        {
            
            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                return;
            }
            else
            {
                Attack();
            }

        }*/
    }

/*    private void Attack()
    {
        if (animator) 
        {
            animator.SetBool("isAttacking", true);
        }

            dps = attackSpeed * attackDamage * Time.deltaTime;
            target.GetComponent<Enemy>().ReceiveDamage(dps);
            Debug.Log("Attacking");
        
        //return attackDamage * attackSpeed * Time.deltaTime;
    }*/
}
