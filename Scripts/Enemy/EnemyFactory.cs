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

public class DefaultEnemyFactory : MonoBehaviour, EnemyFactory
{
    private string katalogZasobow;
    private string[] sciezkiDoZasobow;
    private string pelnaSciezkaWariant1;
    private string pelnaSciezkaWariant2;
    private Vector3 startPoint;
    
    void Start()
    {
        loadAssets();
    }

    private void loadAssets()
    {
        this.katalogZasobow = "Assets/Prefabs/Enemy/Villager";
        sciezkiDoZasobow = AssetDatabase.FindAssets("", new[] { katalogZasobow });
        try
        {
            if (sciezkiDoZasobow.Length == 0)
            {
                throw new Exception("Nie znaleziono zasob�w w katalogu: " + katalogZasobow);
            }
            else
            {
                try
                {
                    this.pelnaSciezkaWariant1 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[0]);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError("Nie odnaleziono zasobu w " + katalogZasobow + " (Zas�b 1): " + e.Message);
                }

                try
                {
                    this.pelnaSciezkaWariant2 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[1]);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError("Nie odnaleziono zasobu w " + katalogZasobow + " (Zas�b 2): " + e.Message);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void setStartPoint(Vector3 startPoint)
    {
        this.startPoint = startPoint;
    }

    GameObject villager;

    public GameObject createEnemy()
    {
        loadAssets();
        villager = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant1), startPoint, Quaternion.identity);
        villager.AddComponent<enemyVillager>();

        return villager; 
    }

    public GameObject createEnemyBoss()
    {
        loadAssets();
        villager = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant1), startPoint, Quaternion.identity);
        villager.AddComponent<enemyVillagerBoss>();

        return villager;
    }

    public GameObject createEnemyWithStatue()
    {
        loadAssets();

        villager = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant2), startPoint, Quaternion.identity);
        villager.AddComponent<enemyVillagerBoss>();

        return villager;
    }


    public GameObject createEnemyBossWithStatue()
    {
        loadAssets();
        
        villager = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant2), startPoint, Quaternion.identity);
        villager.AddComponent<enemyVillagerBoss>();

        return villager;
    }

}

/**
 * @brief Klasa tworz�ca rycerzy - wzorzec fabryki
 * @desc 
 * @author Artur Leszczak
 */

public class KniteEnemyFactory : MonoBehaviour, EnemyFactory
{
    private string katalogZasobow;
    private string[] sciezkiDoZasobow;
    private string pelnaSciezkaWariant1;
    private string pelnaSciezkaWariant2;
    private Vector3 startPoint;
 
    void Start()
    {
        loadAssets();
    }

    private void loadAssets()
    {
        this.katalogZasobow = "Assets/Prefabs/Enemy/Knite";
 
        sciezkiDoZasobow = AssetDatabase.FindAssets("", new[] { katalogZasobow });
        try
        {
           
            if (sciezkiDoZasobow.Length == 0)
            {
                throw new Exception("Nie znaleziono zasob�w w katalogu: " + katalogZasobow);
            }
            else
            {
                try
                {
                    this.pelnaSciezkaWariant1 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[0]);    
                }
                catch (ArgumentException e)
                {
                    Debug.LogError("Nie odnaleziono zasobu w " + katalogZasobow + " (Zas�b 1): " + e.Message);
                }

                try
                {
                    this.pelnaSciezkaWariant2 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[1]);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError("Nie odnaleziono zasobu w " + katalogZasobow + " (Zas�b 2): " + e.Message);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void setStartPoint(Vector3 startPoint)
    {
        this.startPoint = startPoint;
    }

    public GameObject createEnemy()
    {
        loadAssets();
        GameObject knite;
        knite = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(this.pelnaSciezkaWariant1), startPoint, Quaternion.identity);
        knite.AddComponent<enemyKnite>();

        return knite;
    }

    public GameObject createEnemyBoss()
    {
        loadAssets();
        GameObject knite;
        knite = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(this.pelnaSciezkaWariant1), startPoint, Quaternion.identity);
        knite.AddComponent<enemyKniteBoss>();

        return knite;
    }

}
