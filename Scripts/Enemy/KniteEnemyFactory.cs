using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Klasa tworz¹ca rycerzy - wzorzec fabryki
 * @desc 
 * @author Artur Leszczak
 */

public class KniteEnemyFactory : MonoBehaviour, EnemyFactory
{
    [SerializeField]
    GameObject PreafabKnite;
    private Vector3 startPoint;

    void Start()
    {
        // loadAssets();
    }

    private void loadAssets()
    {
        //this.katalogZasobow = "Assets/Prefabs/Enemy/Knite";


    }

    public void setStartPoint(Vector3 startPoint)
    {
        this.startPoint = startPoint;
    }

    public GameObject createEnemy()
    {
        loadAssets();
        GameObject knite;
        knite = Instantiate(PreafabKnite, startPoint, Quaternion.identity);
        knite.AddComponent<enemyKnite>();

        return knite;
    }

    public GameObject createEnemyBoss()
    {
        loadAssets();
        GameObject knite;
        knite = Instantiate(PreafabKnite, startPoint, Quaternion.identity);
        knite.AddComponent<enemyKniteBoss>();

        return knite;
    }

}

