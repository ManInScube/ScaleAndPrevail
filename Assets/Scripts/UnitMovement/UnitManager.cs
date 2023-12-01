using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



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
                //enemy.transform.SetParent(army);
                enemy.DeleteEnemy();

                // TODO: -move to separate function
                enemy.gameObject.AddComponent<MeleeUnits>();
                NavMeshAgent newAgent = enemy.gameObject.AddComponent<NavMeshAgent>();
                enemy.GetComponent<MeleeUnits>().agent = newAgent;
                enemy.GetComponent<UnitView>().enabled = true;
                enemy.GetComponent<EnemyView>().enabled = false;
                enemy.tag = "Untagged";
                enemy.GetComponent<UnitView>().UpdateComponents();

                unitMovement.UpdateSquad();
                //
                //enemy.gameObject.AddComponent<UnitView>();
                //enemy.GetComponent<UnitView>().filler = enemy.gameObject.transform.Find("/Health/HealthBar/Fill Area/Fill").GetComponent<Image>();
                //Destroy(enemy.gameObject.GetComponent<EnemyView>());



                //view.filler = enemy.transform.Find("/Health/HealthBar/Fill Area/Fill").gameObject.GetComponent<Image>();
                //


                /*                enemy.DeleteEnemy();
                                enemy.gameObject.AddComponent<MeleeUnits>();
                                NavMeshAgent newAgent = enemy.gameObject.AddComponent<NavMeshAgent>();
                                enemy.GetComponent<MeleeUnits>().agent = newAgent;
                                enemy.GetComponent<Unit>().enabled = true;
                                enemy.GetComponent<UnitView>().enabled = true;
                                enemy.GetComponent<EnemyView>().enabled = false;*/


            }
            potentialArmy = null;
        }
    }


}
