using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    public NavMeshSurface mesh;
    public static List<GameObject> listaPrzeciwnikow = new List<GameObject>();
    public Camera kamera;
    public static PlacePath sciezka;
    private float elapsedTime = 0f;
    public float interval = 0f;
    int ilosc = 20;
    int iloscodmierzacz = 20;
  // public bool startFali = false;

    string[] sciezkiDoZasobow;
    string pelnaSciezkaWariant1;
    string pelnaSciezkaWariant2;
    public static bool czyPosagZabrany;

    public TextMeshProUGUI tekst;
    private int numerfali=0;

    private EnemyFactory enemyVillager;
    private EnemyFactory enemyKnite;


    void Start()
    {
       // dlugoscfali = 10;
        sciezka = kamera.GetComponent<PlacePath>();
        string katalogZasobow = "Assets/Prefabs/Enemy/Villager";
        czyPosagZabrany = false;
        sciezkiDoZasobow = AssetDatabase.FindAssets("", new[] { katalogZasobow });
        pelnaSciezkaWariant1 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[0]);
        pelnaSciezkaWariant2 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[1]);

        this.enemyVillager = new DefaultEnemyFactory();
        this.enemyKnite = new KniteEnemyFactory();

    }
    GameObject postac;

    public void GetEnemyVillager()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyVillager.setStartPoint(newStart);
        postac = this.enemyVillager.createEnemy();

        listaPrzeciwnikow.Add(postac);

    }
    public void GetEnemyVillagerBoss()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyVillager.setStartPoint(newStart);
        postac = this.enemyVillager.createEnemyBoss();

        listaPrzeciwnikow.Add(postac);

    }
    public void GetEnemyKnite()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyKnite.setStartPoint(newStart);
        postac = this.enemyKnite.createEnemy();

        listaPrzeciwnikow.Add(postac);

    }
    public void GetEnemyKniteBoss()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyKnite.setStartPoint(newStart);
        postac = this.enemyKnite.createEnemyBoss();

        listaPrzeciwnikow.Add(postac);

    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        // Sprawd�, czy up�yn�� okre�lony interwa� czasowy
        if ( elapsedTime >= interval && iloscodmierzacz!=ilosc  )
        {
            if(iloscodmierzacz==0)
            mesh.BuildNavMesh();

            tekst.text = "Fala " + numerfali;
            iloscodmierzacz++;
            // Wykonaj kod, kt�ry ma by� wywo�any po danym interwale czasowym

            if (iloscodmierzacz == 10 || iloscodmierzacz == 20)
            {
                GetEnemyKnite();
            } 
            else
            {
                GetEnemyVillager();
            }

            // Zresetuj licznik czasu
            elapsedTime = 0f;
        }
        if(iloscodmierzacz==ilosc)
        {
            tekst.text = "Do fali: " + (15f - Mathf.Floor(elapsedTime)) + "s";
            if (elapsedTime >= 15f)
            {
                iloscodmierzacz = 0;
                numerfali += 1;
            }
            

        }
        if (sciezka.posag == null && czyPosagZabrany == false)
        {
            Debug.Log("Tworze postac z posagiem"+ PlacePath.pozycjaPosagu);
            czyPosagZabrany = true;
            GameObject postac;
            postac = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant2), PlacePath.pozycjaPosagu, Quaternion.identity);
            postac.AddComponent<enemy>();
            postac.tag = "EnemyWithStatue";
            enemy pierwsza = postac.GetComponent<enemy>();
            pierwsza.aktualnyKafelek = sciezka.getSciezka().Count-1;
            pierwsza.powrot = true;

            // Znisz posag po podniesieniu
            Destroy(sciezka.posag);
        }

    }

    public static void SetCzyPosagZabrany(bool czyPosagZabrany) { EnemyManager.czyPosagZabrany = czyPosagZabrany; }
}
