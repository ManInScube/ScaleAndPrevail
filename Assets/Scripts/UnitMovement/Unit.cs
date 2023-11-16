using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public NavMeshAgent agent;


    public enum Type{
        MeleeHuman,
        RangeHuman,
        MeleeMech,
        RangeMech
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void MoveToPoint(Vector3 dest)
    {
        agent.SetDestination(dest);
    }
}
