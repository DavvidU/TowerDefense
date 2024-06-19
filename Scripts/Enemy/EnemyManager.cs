using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

/**
 * Klasa odpowiedzialna za zarz�dzanie przeciwnikami
 * 
 * @author Artur Leszczak, Nikola Osi�ska, Konrad Kondracki, Dawid Ugniewski
 * @version 1.1.0
 */
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

    [SerializeField]
    GameObject PrefabVillager;
    public static bool czyPosagZabrany;

    public TextMeshProUGUI tekst;
    public TextMeshProUGUI levelText;
    private int numerfali=0;

    [SerializeField]
    private EnemyFactory enemyVillager1;
    [SerializeField]
    private EnemyFactory enemyKnite;
    public Vector3 cel;
    public static bool rozpocznijPrzygotowanie = true;
    private bool widocznoscPaskuZycia = true;
    FloorManager siatkaPlanszy;


    void Start()
    {
        sciezka = kamera.GetComponent<PlacePath>();
        czyPosagZabrany = false;
        
        /**
         * Pobranie komponent�w klas odpowiedzialnych za przeciwnik�w
         */
        this.enemyVillager1 = gameObject.GetComponent<DefaultEnemyFactory>();
        this.enemyKnite = gameObject.GetComponent<KniteEnemyFactory>();
        
        siatkaPlanszy = gameObject.GetComponent<FloorManager>();
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
            enemy.UstawWidocznoscObiekt�w(stan);
        }
    }

    /**
     * Metoda dodaj�ca nowego przeciwnika "wie�niaka" do listy przeciwnik�w
     *
     * @author Artur Leszczak
     * @version 1.0.0
     */
    public void GetEnemyVillager()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyVillager1.setStartPoint(newStart);
        postac = this.enemyVillager1.createEnemy();

        listaPrzeciwnikow.Add(postac);
    }

    /**
    * Metoda dodaj�ca nowego przeciwnika "wie�niaka bosa" do listy przeciwnik�w
    *
    * @author Artur Leszczak
    * @version 1.0.0
    */
    public void GetEnemyVillagerBoss()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyVillager1.setStartPoint(newStart);
        postac = this.enemyVillager1.createEnemyBoss();

        listaPrzeciwnikow.Add(postac);

    }

    /**
    * Metoda dodaj�ca nowego przeciwnika "rycerza" do listy przeciwnik�w
    *
    * @author Artur Leszczak
    * @version 1.0.0
    */
    public void GetEnemyKnite()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyKnite.setStartPoint(newStart);
        postac = this.enemyKnite.createEnemy();

        listaPrzeciwnikow.Add(postac);

    }

    /**
    * Metoda dodaj�ca nowego przeciwnika "rycerza bosa" do listy przeciwnik�w
    *
    * @author Artur Leszczak
    * @version 1.0.0
    */
    public void GetEnemyKniteBoss()
    {
        Vector3 newStart = sciezka.getStartPosition();
        newStart = new Vector3(newStart.x, newStart.y, newStart.z);

        this.enemyKnite.setStartPoint(newStart);
        postac = this.enemyKnite.createEnemyBoss();

        listaPrzeciwnikow.Add(postac);

    }
    int licznik = 0;
    /**
     * Metoda kt�ra pojawia na mapie przeciwnika co okre�lony interwa� czasu , pilnuje te� ilu przeciwnik�w mo�e by� w turze na mapie.
     * Podmienia przeciwnika kt�ry podnie�ie pos���k na model z pos�giem w r�ce.
     * 
     * @author Konrad Kondracki, Nikola Osi�ska
     */
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        

        levelText.text = "" + numerfali;

        // Sprawd�, czy up�yn�� okre�lony interwa� czasowy
        if ( elapsedTime >= interval && iloscodmierzacz!=ilosc  )
        {
            if(iloscodmierzacz==0)
            mesh.BuildNavMesh();

            siatkaPlanszy.Hide();

            tekst.text = "Walka!";
            iloscodmierzacz++;
            // Wykonaj kod, kt�ry ma by� wywo�any po danym interwale czasowym

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
            EnemyFactory ef = gameObject.GetComponent<DefaultEnemyFactory>();
            postac = Instantiate(ef.createEnemy(), PlacePath.pozycjaPosagu, Quaternion.identity);
            postac.AddComponent<enemyVillager>();
            postac.tag = "EnemyWithStatue";
            enemyVillager pierwsza = postac.GetComponent<enemyVillager>();
            pierwsza.GetDesiredPoint();
            Debug.Log("Zaczynam tworzyc postac z pos�giem");
            

            pierwsza.NavMeshAgent.SetDestination(enemyVillager.cel);
            listaPrzeciwnikow.Add(postac);

            // Znisz posag po podniesieniu
            zmianaKierunkuWszystkich();
            pierwsza.TakeDamage(60);
            Debug.Log("Tworze postac z posagiem");

            Destroy(sciezka.posag);
          
        }

    }

    /**
     * Metoda aktualizuj�ca cel do kt�rego d��� przeciwnicy na podstawie tego jaki cel maj� okre�lony w swojej instancji.
     * 
     */
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
