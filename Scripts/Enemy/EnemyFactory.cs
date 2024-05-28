using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
 * @brief Fabryka przeciwników korzystaj¹ca ze wzorca kreacyjnego fabryka
 * @desc
 * 
 * @author Artur Leszczak
 */

public interface EnemyFactory
{
    public GameObject createEnemy();

    //jest wiêkszy i ma o 50% wiêcej ¿ycia
    public GameObject createEnemyBoss();

    public void setStartPoint(Vector3 startPoint);

}



