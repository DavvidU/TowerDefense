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

    string[] sciezkiDoZasobow;
    string pelnaSciezkaWariant1;
    string pelnaSciezkaWariant2;
    public static bool czyPosagZabrany;

    public TextMeshProUGUI tekst;
    public TextMeshProUGUI levelText;
    private int numerfali=0;

    private EnemyFactory enemyVillager1;
    private EnemyFactory enemyKnite;
    public Vector3 cel;
    public static bool rozpocznijPrzygotowanie = true;
    private bool widocznoscPaskuZycia = true;



    void Start()
    {
       // dlugoscfali = 10;
        sciezka = kamera.GetComponent<PlacePath>();
        string katalogZasobow = "Assets/Prefabs/Enemy/Villager";
        czyPosagZabrany = false;
        sciezkiDoZasobow = AssetDatabase.FindAssets("", new[] { katalogZasobow });
        pelnaSciezkaWariant1 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[0]);
        pelnaSciezkaWariant2 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[1]);

        

        this.enemyVillager1 = gameObject.AddComponent<DefaultEnemyFactory>();
        this.enemyKnite = gameObject.AddComponent<KniteEnemyFactory>();

    }
    private void Awake()
    {
        //Application.targetFrameRate = 60;
    }
    private void Update()
    {
       
        if(widocznoscPaskuZycia==true)
        {
            WidocznoscPaskowZycia(true);
        }
        else
        {
            WidocznoscPaskowZycia(false);
        }

    }
    GameObject postac;
    public void WidocznoscPaskowZycia(bool stan)
    {
        foreach( GameObject przeciwnik in listaPrzeciwnikow){
           enemyVillager enemy= przeciwnik.GetComponent<enemyVillager>();
            enemy.UstawWidocznoscObiektów(stan);
        }
    }

    public void GetEnemyVillager()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyVillager1.setStartPoint(newStart);
        postac = this.enemyVillager1.createEnemy();

        listaPrzeciwnikow.Add(postac);

    }
    public void GetEnemyVillagerBoss()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyVillager1.setStartPoint(newStart);
        postac = this.enemyVillager1.createEnemyBoss();

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
    int licznik = 0;
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        FloorManager siatkaPlanszy = gameObject.AddComponent<FloorManager>();

        levelText.text = "" + numerfali;

        // SprawdŸ, czy up³yn¹³ okreœlony interwa³ czasowy
        if ( elapsedTime >= interval && iloscodmierzacz!=ilosc  )
        {
            if(iloscodmierzacz==0)
            mesh.BuildNavMesh();

            siatkaPlanszy.Hide();

            tekst.text = "Walka!";
            iloscodmierzacz++;
            // Wykonaj kod, który ma byæ wywo³any po danym interwale czasowym

            if (iloscodmierzacz == 10 || iloscodmierzacz == 20)
            {
               // GetEnemyKnite();
            } 
            else if(licznik<=10)
            {
                licznik++;
                GetEnemyVillager();
            }

            // Zresetuj licznik czasu
            elapsedTime = 0f;
        }
        if(listaPrzeciwnikow.Count==0 && rozpocznijPrzygotowanie==true)
        {   tekst.text = ""+numerfali;

            siatkaPlanszy.Show();
            tekst.text = (15f - Mathf.Floor(elapsedTime)) + "s";
            tekst.GetComponent<Animator>().SetTrigger("odpalAnimacje");

            //tekst.GetComponent<Animation>().Play();
            if (elapsedTime >= 15f)
            {
                licznik = 0;
                iloscodmierzacz = 0;
                rozpocznijPrzygotowanie = false;
                numerfali += 1;
                tekst.GetComponent<Animator>().ResetTrigger("odpalAnimacje");
                tekst.GetComponent<Animator>().SetTrigger("wylaczAnimacje");
                tekst.text = "Walka!";

            }
            

        }
        if (sciezka.posag == null && czyPosagZabrany == false)
        {
            
            czyPosagZabrany = true;
            GameObject postac;
            Debug.Log("posag zabrany??: ");
            EnemyFactory ef = new DefaultEnemyFactory();
            postac = Instantiate(ef.createEnemy(), PlacePath.pozycjaPosagu, Quaternion.identity);
            postac.AddComponent<enemyVillager>();
            postac.tag = "EnemyWithStatue";
            enemyVillager pierwsza = postac.GetComponent<enemyVillager>();
            pierwsza.GetDesiredPoint();
            Debug.Log("Zaczynam tworzyc postac z pos¹giem");
            

            pierwsza.NavMeshAgent.SetDestination(enemyVillager.cel);
            listaPrzeciwnikow.Add(postac);

            // Znisz posag po podniesieniu
            zmianaKierunkuWszystkich();
            pierwsza.TakeDamage(60);
            Debug.Log("Tworze postac z posagiem");

            Destroy(sciezka.posag);
          
        }

    }
    public void zmianaKierunkuWszystkich()
    {
        foreach(GameObject przeciwnik in listaPrzeciwnikow)
        {
            Debug.Log("Zawracam"+ listaPrzeciwnikow.Count);
            enemyVillager enemy=przeciwnik.GetComponent<enemyVillager>();
            //enemy.powrot = true;
            enemy.NavMeshAgent.SetDestination(enemyVillager.cel);

        }
    }

    public static void SetCzyPosagZabrany(bool czyPosagZabrany) { EnemyManager.czyPosagZabrany = czyPosagZabrany; }
}
