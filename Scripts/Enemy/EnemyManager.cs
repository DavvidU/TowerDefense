using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    private List<enemy> listaPrzeciwnikow = new List<enemy>();
    public Camera kamera;
    public PlacePath sciezka;
    private float elapsedTime = 0f;
    public float interval = 0f;
    int ilosc = 20;
    int iloscodmierzacz = 0;
   public bool startFali = false;
    string[] sciezkiDoZasobow;
    string pelnaSciezkaWariant1;
    string pelnaSciezkaWariant2;
    static bool czyPosagZabrany;

    private EnemyFactory enemyVillager;
    private EnemyFactory enemyKnite;


    void Start()
    {
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
       
    }
    public void GetEnemyVillagerBoss()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyVillager.setStartPoint(newStart);
        postac = this.enemyVillager.createEnemyBoss();

    }
    public void GetEnemyKnite()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyKnite.setStartPoint(newStart);
        postac = this.enemyKnite.createEnemy();

    }
    public void GetEnemyKniteBoss()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyKnite.setStartPoint(newStart);
        postac = this.enemyKnite.createEnemyBoss();

    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        // SprawdŸ, czy up³yn¹³ okreœlony interwa³ czasowy
        if (elapsedTime >= interval && iloscodmierzacz!=ilosc  )
        {
            iloscodmierzacz++;
            // Wykonaj kod, który ma byæ wywo³any po danym interwale czasowym
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
        if (sciezka.posag == null && czyPosagZabrany == false)
        {
            czyPosagZabrany = true;
            GameObject postac;

            postac = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant2),PlacePath.pozycjaPosagu, Quaternion.identity);
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
