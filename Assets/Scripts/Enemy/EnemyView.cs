using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyView : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;

    private Enemy controller;

    public Slider healthBar;
    public Image filler;

    public Image KingSign;

    public float curHealth;
   // public float maxHealth;

    private Camera mainCamera;

    private NavMeshAgent agent;


    private void Awake()
    {
        //curHealth = maxHealth;
        mainCamera = Camera.main;
        
 
        controller = GetComponent<Enemy>();

        
        healthBar.value = CalculateHealthBar();

        try
        {
            animator = GetComponent<Animator>();
        }
        catch
        {
            Debug.Log("No Animator");
        }

        try
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
        }
        catch
        {
            Debug.Log("No NavMeshAgent");
        }


    }


    private void FixedUpdate()
    {
        healthBar.value = CalculateHealthBar();
        filler.color = Color.red;
        healthBar.transform.LookAt(mainCamera.transform);

        if (this.controller.enemyType == Enemy.EnemyType.King)
        {
            KingSign.transform.LookAt(mainCamera.transform);
        }

    }

    private void OnEnable()
    {
        if (controller != null)
        {
            controller.OnAgentStopped += AgentStopHandler;
            controller.OnAttack += UnitAttackHandler;
            controller.OnWalk += UnitWalkHandler;
        }
    }

    private void OnDisable()
    {
        if (controller != null)
        {
            controller.OnAgentStopped -= AgentStopHandler;
            controller.OnAttack -= UnitAttackHandler;
            controller.OnWalk -= UnitWalkHandler;
        }
    }

    public void AgentStopHandler()
    {
        animator.SetBool("isWalking", false);
    }

    public void UnitAttackHandler()
    {
        //animator.SetTrigger("isAttackingTrigger");
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);

    }

    public void UnitWalkHandler()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    float CalculateHealthBar()
    {
        //Debug.Log("Calculation");
        return controller.health / controller.maxHealth;
    }

}
