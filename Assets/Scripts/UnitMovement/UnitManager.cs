using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//UnitManagerView
public class UnitManager : MonoBehaviour
{


    private UnitMovement unitMovement;
    public enum Units
    {
        All,
        Melee,
        Range
    }

    private Units activeUnit;

    void Start()
    {
        activeUnit = Units.All;
        unitMovement = GameObject.FindObjectOfType<UnitMovement>();
    }

    void Update()
    {
        
    }

    public void PickSquad(int unit)
    {
        activeUnit = (Units)unit;
        Debug.Log(activeUnit);
        unitMovement.SwitchSquad(activeUnit);
    }


}
