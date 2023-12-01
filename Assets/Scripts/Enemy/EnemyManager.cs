using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    float health;

    float maxHealth = 100;


    //сделать что-то типа EnemyView
    [SerializeField] uint enemiesCount;
    [SerializeField] uint mindStrength;
    //private uint Complexity { get { return (uint)Mathf.Floor(uintsCount / enemiesCount * 10); } } //TODO: доработать сделать в string значение

    private uint uintsCount;

    public Enemy[] enemies;

    //Enemy King => (Enemy.EnemyType.King)Enemy.EnemyType.King;

    void Start()
    {
        enemies = gameObject.GetComponentsInChildren<Enemy>();
    }

    void Update()
    {
       // WinCondition();
    }

    private void KingsDeathHandler()
    {
        Debug.Log("Castle captured");
    }

    public void WinCondition()
    {
/*        if (enemiesCount < enemiesCount / 2)
        {

        }*/

/*        foreach(Enemy enemy in enemies)
        {
            if (enemy.enemyType == Enemy.EnemyType.King && enemy.isDead)
            {
                Debug.Log("Castle captured");
            }
        }*/

    }


}
