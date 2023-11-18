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

        if (isMoving)
        {
            MoveSquad(destination);
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
                isMoving = true;
            }
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Enemy found");
                foreach (Unit unit in units)
                {
                    //unitModel.GetComponent<UnitModel>().target = hit.collider.transform.gameObject.GetComponent<Enemy>();
                    TargetAction?.Invoke(hit.collider.transform.gameObject.GetComponent<Enemy>());
                }
                destination = hit.point;
                isMoving = true;
                //Debug.Log(unitModel.target);
            }
        }
    }

    private void MoveSquad(Vector3 dest)
    {
            foreach (Unit unit in units)
            {
                unit.MoveToPoint(dest);

                if (unit.gameObject.transform.position == dest)
                {
                    isMoving = false;
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
