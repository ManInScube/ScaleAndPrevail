using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitView : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;

    private Unit controller;

    public Slider healthBar;
    public Image filler;

    public float curHealth;
   // public float maxHealth;

    private Camera mainCamera;


    private void Awake()
    {
        //curHealth = maxHealth;
        mainCamera = Camera.main;
        
 
        controller = GetComponent<Unit>();

        
        healthBar.value = CalculateHealthBar();

        try
        {
            animator = GetComponent<Animator>();
        }
        catch
        {
            Debug.Log("No Animator");
        }
    }


    private void FixedUpdate()
    {
        healthBar.value = CalculateHealthBar();
        filler.color = healthBar.value > 0.3 ? Color.green : Color.red;
        healthBar.transform.LookAt(mainCamera.transform);
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

/*    public void SetMaxHealth(float health)
    {
        controller.maxHealth = health;
        curHealth = health;
        healthBar.value = CalculateHealthBar();
    }*/
}
