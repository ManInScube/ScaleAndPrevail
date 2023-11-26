using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//UnitManagerController
public class UnitMovement : MonoBehaviour
{

    private Unit[] units;
    private bool isMoving = false;

    private Vector3 destination;

    private UnitModel unitModel;
    private UnitManager unitManager;

    public static event Action<Enemy> TargetAction;
    void Start()
    {
        units = GameObject.FindObjectsOfType<Unit>();
        unitManager = GameObject.FindObjectOfType<UnitManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetPosition();
        }
    }

    void SetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // return;
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if (isOverUI)
            {
                return;
            }
            if (hit.transform.CompareTag("Ground"))
            {
                destination = hit.point;
                MoveSquad(destination);

            }
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Enemy found");
                destination = hit.point;



                foreach (Unit unit in units)
                {
                    TargetAction?.Invoke(hit.collider.transform.gameObject.GetComponent<Enemy>());

                    if (Vector3.Distance(unit.transform.position, hit.transform.position) > unit.attackRange)
                    {
                        unit.MoveToPoint(destination);
                    }
                    else
                    {

                        return;
 /*                       unit.isAttacking = true;
                        unit.agent.isStopped = true;
                        unit.agent.ResetPath();*/
                    }

                }

/*                foreach (Unit unit in units)
                {
                    unit.MoveToPoint(destination);
                    TargetAction?.Invoke(hit.collider.transform.gameObject.GetComponent<Enemy>());
*//*
                    if (Vector3.Distance(unit.transform.position, hit.transform.position) > unit.attackRange)
                    {
                        //unit.Attack();
                        TargetAction?.Invoke(hit.collider.transform.gameObject.GetComponent<Enemy>());

                    }*//*
                }*/
            }


        }
    }

    private void MoveSquad(Vector3 dest)
    {

        List<Vector3> positions = CalculatePositions(dest);
        for (int i= 0; i<=units.Length; i++)
        {
            units[i].MoveToPoint(positions[i]);
        }

    }

    public void SwitchSquad(UnitManager.Units unit)
    {
        switch (unit)
        {
            case UnitManager.Units.All:
                units = GameObject.FindObjectsOfType<Unit>();
                break;
            case UnitManager.Units.Melee:
                units = GameObject.FindObjectsOfType<MeleeUnits>();
                break;
            case UnitManager.Units.Range:
                units = GameObject.FindObjectsOfType<RangeUnits>();
                break;
            default:
                break;
        }
    }

    public List<Vector3> CalculatePositions(Vector3 dest)
    {
        List<Vector3> positions = new List<Vector3>();
        
        int square = (int)Mathf.Floor(Mathf.Sqrt(units.Length));
        Debug.Log(square);
        Vector3 pos;
        pos = dest;
        for (int i = 0; i<=units.Length; i++)
        {
            for (int j = 0; j <= square - 1; j++)
            {
                
                positions.Add(pos);
                pos += new Vector3(2f, 0f, 0f);
                Debug.Log(pos);
            }
            pos += new Vector3(-square*2f, 0f, 5f);
        }

        return positions;
    }

}
