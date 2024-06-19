using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Domyœlna fabryka przeciwników.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class DefaultEnemyFactory : MonoBehaviour, EnemyFactory
{
    [SerializeField]
    GameObject EnemyPrefab;
    [SerializeField]
    GameObject EnemyPrefabStatua;
    private Vector3 startPoint;

    void Start()
    {
    }

     /**
     * Ustawia punkt startowy dla obiektu.
     *
     * @param startPoint Wektor 3D reprezentuj¹cy punkt startowy.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     */
    public void setStartPoint(Vector3 startPoint)
    {
        this.startPoint = startPoint;
    }

    GameObject villager;

    public GameObject createEnemy()
    {

        villager = Instantiate(EnemyPrefab, startPoint, Quaternion.identity);
        villager.AddComponent<enemyVillager>();

        return villager;
    }

    public GameObject createEnemyBoss()
    {

        villager = Instantiate(EnemyPrefab, startPoint, Quaternion.identity);
        villager.AddComponent<enemyVillagerBoss>();

        return villager;
    }

    public GameObject createEnemyWithStatue()
    {


        // villager = Instantiate(EnemyPrefabStatua, startPoint, Quaternion.identity);
        // villager.AddComponent<enemyVillagerBos>();

       // villager = EnemyPrefabStatua;

        return EnemyPrefabStatua;
    }


    public GameObject createEnemyBossWithStatue()
    {


        villager = Instantiate(EnemyPrefabStatua, startPoint, Quaternion.identity);
        villager.AddComponent<enemyVillagerBoss>();

        return villager;
    }

}
