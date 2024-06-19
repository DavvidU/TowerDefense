using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
 * @brief Interfejs fabryki przeciwników korzystaj¹cy ze wzorca kreacyjnego fabryka 
 * 
 * @author Artur Leszczak
 * @version 1.0.0
 */

public interface EnemyFactory
{
    public GameObject createEnemy();

    //jest wiêkszy i ma o 50% wiêcej ¿ycia
    public GameObject createEnemyBoss();
    public GameObject createEnemyWithStatue();

    public void setStartPoint(Vector3 startPoint);

}



