using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//UnitController
public class Unit : MonoBehaviour
{
    public NavMeshAgent agent;

    private Animator animator;




    protected void Start()
    {
        try
        {
            animator = GetComponent<Animator>();
        }
        catch
        {
            Debug.Log("There's no animator");
        }
    }

    void Update()
    {

    }

    public void MoveToPoint(Vector3 dest)
    {
        if (animator)
        {
            animator.SetBool("isWalking", true);
        }
        agent.SetDestination(dest);
    }
}
