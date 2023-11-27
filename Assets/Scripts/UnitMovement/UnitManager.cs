using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



//UnitManagerView
public class UnitManager : MonoBehaviour
{


    private UnitMovement unitMovement;
    private Enemy[] potentialArmy;

    public Transform army;

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

    private void OnEnable()
    {
        UnitMovement.TargetAction += TargetHandler;
        Enemy.OnKingsDead += ScaleTheArmy;
    }

    private void TargetHandler(Enemy enemy)
    {
        potentialArmy = enemy.transform.GetComponentInParent<EnemyManager>().gameObject.transform.GetComponentsInChildren<Enemy>();
        
    }

    public void PickSquad(int unit)
    {
        activeUnit = (Units)unit;
        Debug.Log(activeUnit);
        unitMovement.SwitchSquad(activeUnit);
    }

    public void ScaleTheArmy()
    {
        foreach(Enemy enemy in potentialArmy)
        {
            if (enemy.enemyType != Enemy.EnemyType.King)
            {
                enemy.DeleteEnemy();
                //enemy.transform.SetParent(army);
                enemy.gameObject.AddComponent<MeleeUnits>();
                NavMeshAgent newAgent = enemy.gameObject.AddComponent<NavMeshAgent>();
                enemy.GetComponent<MeleeUnits>().agent = newAgent;
            }
            potentialArmy = null;
        }
    }


}
