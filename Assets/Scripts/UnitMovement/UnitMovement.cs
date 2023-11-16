using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//UnitManagerController
public class UnitMovement : MonoBehaviour
{

    private Unit[] units;
    private bool isMooving = false;

    private Vector3 destination;

    private UnitModel unitModel;
    private UnitManager unitManager;


    public static event Action<Enemy> EnemyAction;
    void Start()
    {
        units = GameObject.FindObjectsOfType<Unit>();
        unitManager = GameObject.FindObjectOfType<UnitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetPosition();
        }

        if (isMooving)
        {
            Moove(destination);
        }
    }

    void SetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // return;
            if (hit.transform.CompareTag("Ground"))
            {
                destination = hit.point;
                isMooving = true;
            }
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Enemy found");
                foreach (Unit unit in units)
                {
                    //unitModel.GetComponent<UnitModel>().target = hit.collider.transform.gameObject.GetComponent<Enemy>();
                    EnemyAction?.Invoke(hit.collider.transform.gameObject.GetComponent<Enemy>());
                }
                destination = hit.point;
                isMooving = true;
                Debug.Log(unitModel.target);
            }
        }
    }

    private void Moove(Vector3 dest)
    {
            foreach (Unit unit in units)
            {
                unit.MoveToPoint(dest);

                if (unit.gameObject.transform.position == dest)
                {
                    isMooving = false;
                }
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

}
