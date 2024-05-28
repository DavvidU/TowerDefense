using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
 * @brief Fabryka przeciwnik�w korzystaj�ca ze wzorca kreacyjnego fabryka
 * @desc
 * 
 * @author Artur Leszczak
 */

public interface EnemyFactory
{
    public GameObject createEnemy();

    //jest wi�kszy i ma o 50% wi�cej �ycia
    public GameObject createEnemyBoss();

    public void setStartPoint(Vector3 startPoint);

}



