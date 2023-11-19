using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//UnitController
public class Unit : MonoBehaviour
{
    public NavMeshAgent agent;

    private Animator animator;

/*    private UnitModel Model { get; set; }
    private UnitView View { get; set; }

    public Unit(UnitModel model, UnitView view)
    {
        Model = model;
        View = view;
    }*/
   

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

       // View.ViewTest("HOH");
    }

    void Update()
    {
        if (agent.destination == this.transform.position)
        {
            animator.SetBool("isWalking", false);
        }
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
