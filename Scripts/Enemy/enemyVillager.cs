using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class enemyVillager : MonoBehaviour
{


    private PlacePath sciezka;
    public GameObject postac;
    List<GridTile> listaSciezki = new List<GridTile>();
    private Vector3 aktualnaPozycja;
    public int aktualnyKafelek = 1;
    public float speed = 3f;
    public bool powrot = false;
    // public int HP=100;
    private bool czasSpowolnienia = false;
    private bool czasPodpalenia = false;
    private float timer = 0.0f;
    private GameObject gameControllerObj;
    private int currentLife = 100; // Aktualna ilo�� �ycia
    private TextMeshPro lifeText;
    public NavMeshAgent NavMeshAgent;
    public static Vector3 cel;
    private int licznik;


    public Transform target; // Cel, np. gracz
    public float moveSpeed = 3f;

     /* Pasek zdrowia */

    private Image lifeBar;
    private GameObject lifeBarBackground;

    private GameObject fireEffect;
    private GameObject iceEffect;

    void Awake()
    {
        gameControllerObj = GameObject.Find("Main Camera");
        sciezka = gameControllerObj.GetComponent<PlacePath>();
        listaSciezki = sciezka.getSciezka();
        NavMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        CreateLifeBar();

    }
    void Start()
    {
        UpdateLifeBar();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = moveSpeed;
        cel = sciezka.StopPosition;
        
        NavMeshAgent.SetDestination(PlacePath.pozycjaPosagu);
        Debug.Log("Moj poczatkowycel to :" + enemyVillager.cel);
    }
    void CreateLifeBar()
    {
        // Tworzenie t�a paska �ycia
        lifeBarBackground = new GameObject("LifeBarBackground");
        lifeBarBackground.transform.SetParent(transform);
        lifeBarBackground.transform.localPosition = Vector3.up * 2f;
        lifeBarBackground.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        lifeBarBackground.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 10;
        lifeBarBackground.AddComponent<GraphicRaycaster>();

        // Tworzenie paska �ycia
        GameObject lifeBarObject = new GameObject("LifeBar");
        lifeBarObject.transform.SetParent(lifeBarBackground.transform);
        lifeBarObject.AddComponent<CanvasRenderer>();
        lifeBar = lifeBarObject.AddComponent<Image>();
        lifeBar.color = Color.green;

        RectTransform rectTransform = lifeBar.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 20);
        rectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        rectTransform.localPosition = Vector3.zero;

        // Tworzenie efektu ognia
        fireEffect = new GameObject("FireEffect");
        fireEffect.transform.SetParent(lifeBarObject.transform);
        ParticleSystem fireParticleSystem = fireEffect.AddComponent<ParticleSystem>();
        var main = fireParticleSystem.main;
        main.startColor = Color.red;
        main.loop = true;
        fireParticleSystem.Stop();

        // Tworzenie efektu lodu
        iceEffect = new GameObject("IceEffect");
        iceEffect.transform.SetParent(lifeBarObject.transform);
        ParticleSystem iceParticleSystem = iceEffect.AddComponent<ParticleSystem>();
        var iceMain = iceParticleSystem.main;
        iceMain.startColor = Color.cyan;
        iceMain.loop = true;
        iceParticleSystem.Stop();
    }

    void UpdateLifeBar()
    {
        if (lifeBar != null)
        {
            lifeBar.fillAmount = (float)currentLife / 100f;
            if (currentLife > 50)
                lifeBar.color = Color.green;
            else if (currentLife > 20)
                lifeBar.color = Color.yellow;
            else
                lifeBar.color = Color.red;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Dosta�em damage");
        
        currentLife -= damageAmount;


        if (currentLife > 0)
        {
            Debug.Log("updatuje zycie " + currentLife);
            UpdateLifeBar();
        }
        else
        {
            // Przeciwnik ginie
            currentLife = 0;
            UpdateLifeBar();
            Destroy(gameObject);
        }
    }
    public PlacePath getPath()
    {
        return sciezka;
    }
    public void sprawdzCzyMinalCzas(float czas)
    {
        if (czas > 0.5f)
        {
            czasSpowolnienia = false;
            czasPodpalenia = false;
            timer = 0.0f;
            speed = 3f;
            fireEffect.GetComponent<ParticleSystem>().Stop();
            iceEffect.GetComponent<ParticleSystem>().Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Kolce")
        {
            //currentLife = currentLife - 20;
            //Debug.Log("Wlazlem w kolce-" + currentLife);
            TakeDamage(20);
        }
        else if (other.gameObject.tag == "Icing")
        {
            if (PatronType.PatronSlowActive)
            {
                timer = 0.0f;
                speed = 1.5f;
                czasSpowolnienia = true;
            }
            else
            {
                timer = 0.0f;
                speed = 2f;
                czasSpowolnienia = true;
            }

            iceEffect.GetComponent<ParticleSystem>().Play();
        }
        else if (other.gameObject.tag == "lawa")
        {
            timer = 0.0f;
            licznik = 1;
            czasPodpalenia = true;
            fireEffect.GetComponent<ParticleSystem>().Play();
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        //currentLife = currentLife - 40;
        Debug.Log("Trafiony przez strza�e-" + currentLife);
        Destroy(collision.gameObject);
        TakeDamage(40);
    }


    void Update()
    {
        lifeBarBackground.transform.rotation = Quaternion.LookRotation(transform.position - gameControllerObj.transform.position);


        if (czasSpowolnienia == true)
        {
            timer += Time.deltaTime;
            sprawdzCzyMinalCzas(timer);
        }
        if (czasPodpalenia == true)
        {
            if (timer > 0.15f * licznik)
            {
                if (PatronType.PatronDmgActive)
                {
                    TakeDamage(15);
                    licznik++;
                }
                else
                {
                    TakeDamage(10);
                    licznik++;
                }

            }

            timer += Time.deltaTime;
            sprawdzCzyMinalCzas(timer);
        }

        /*
        aktualnaPozycja = transform.position;
        if (aktualnyKafelek < sciezka.naKtorymKafelkuSciezkiLezyPosag && aktualnyKafelek >= 0)
        {
            Vector3 targetPosition = new Vector3();
            if (listaSciezki[aktualnyKafelek] != null)
            {
                targetPosition.x = listaSciezki[aktualnyKafelek].x;
                targetPosition.z = listaSciezki[aktualnyKafelek].y;
                targetPosition.y = 0.5f;
                float krok = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, krok);

                // obliczamy wektor kierunku i tworzymy obr�t na podstawie kierunku
                Vector3 direction = (targetPosition - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    // rotacja w stron� kierunkow�
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
                }
            }
            if (aktualnaPozycja.x == listaSciezki[aktualnyKafelek].x && aktualnaPozycja.z == listaSciezki[aktualnyKafelek].y)
            {
                if (powrot == false)
                    aktualnyKafelek++;
                else
                {
                    if (aktualnyKafelek == 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                        aktualnyKafelek--;
                }
            }

        }
        else
        {
            if (sciezka.posag != null)
            {
                Destroy(sciezka.posag);
                Destroy(gameObject);

            }
            powrot = true;
            aktualnyKafelek--;
        }
        */
        if (NavMeshAgent.isActiveAndEnabled && NavMeshAgent.isOnNavMesh && NavMeshAgent != null)
        {
            if (sciezka.posag != null && NavMeshAgent.remainingDistance < 0.5f)
            {
                EnemyManager.listaPrzeciwnikow.Remove(gameObject);
                if (EnemyManager.listaPrzeciwnikow.Count == 0)
                {
                    EnemyManager.rozpocznijPrzygotowanie = true;
                }
                Destroy(NavMeshAgent);
                Destroy(sciezka.posag);
                //EnemyManager.SetCzyPosagZabrany(true);


                Destroy(gameObject);
               // Debug.Log("Zniszczy�em sie");
            }
            else
            if (powrot==true && NavMeshAgent.remainingDistance < 0.5f)
            {
                EnemyManager.listaPrzeciwnikow.Remove(gameObject);
                if (EnemyManager.listaPrzeciwnikow.Count == 0)
                {
                    EnemyManager.rozpocznijPrzygotowanie = true;
                }
                Destroy(NavMeshAgent);
                
                Destroy(gameObject);
                Debug.Log("Zniszczy�em sie");
            }else 
            
            if (NavMeshAgent!= null &&!NavMeshAgent.pathPending && NavMeshAgent.remainingDistance < 0.5f )
            {
                GetDesiredPoint();
                // Zmie� cel na ��dany punkt
                NavMeshAgent.SetDestination(cel);
                powrot = true;
            }
        }
        if (currentLife < 0)
        {
           // Vector3 pozycjaDocelowa = NavMeshAgent.destination;
            float pozycjaX = transform.position.x;
            float pozycjaZ = transform.position.z;
           // float pozycjaX = (float)Math.Truncate(this.aktualnaPozycja.x);
            //float pozycjaZ = (float)Math.Truncate(this.aktualnaPozycja.z);
            Debug.Log(pozycjaZ + ". " + pozycjaZ);

            Vector3 deathpostion = new Vector3(pozycjaX, 0.5f, pozycjaZ);


            if (this.gameObject.tag == "EnemyWithStatue") // Czy umierajacy niosl posag
            {
                Debug.Log("Umar�emze statuetka"+ deathpostion);
                PlacePath.pozycjaPosagu = deathpostion;
                sciezka.posag = Instantiate(PlacePath.pobierzObiektPosagu(), deathpostion, Quaternion.identity);
                sciezka.naKtorymKafelkuSciezkiLezyPosag = aktualnyKafelek;
                ustawCelDlaPrzeciwnikow(deathpostion);
                //sciezka.SetStopPosition(deathpostion); //TD-39: To chyba nie potrzebne do podnoszenia posagu
            }
            EnemyManager.listaPrzeciwnikow.Remove(gameObject);
            if (EnemyManager.listaPrzeciwnikow.Count == 0)
            {
                EnemyManager.rozpocznijPrzygotowanie = true;
            }
            Destroy(gameObject);

            EnemyManager.SetCzyPosagZabrany(false);
        }

    }
    public void GetDesiredPoint()
    {
        cel= sciezka.getStartPosition();
       
    }
       
    
    public void ustawCelDlaPrzeciwnikow(Vector3 cel2)
    {
            enemyVillager.cel = cel2;
        Debug.Log("cel" + cel);
        foreach(GameObject przeciwnik in EnemyManager.listaPrzeciwnikow )
        {
            Debug.Log("lecimy znowu na statuetke");
            enemyVillager enemy = przeciwnik.GetComponent<enemyVillager>();
            enemy.powrot = false;
            enemy.NavMeshAgent.SetDestination(cel2);
        }
    }


}

public class enemyVillagerBoss : MonoBehaviour
{

    private PlacePath sciezka;
    public GameObject postac;
    List<GridTile> listaSciezki = new List<GridTile>();
    private Vector3 aktualnaPozycja;
    public int aktualnyKafelek = 1;
    public float speed = 2f;
    public bool powrot = false;
    // public int HP=100;
    private bool czasSpowolnienia = false;
    private bool czasPodpalenia = false;
    private float timer = 0.0f;
    private GameObject gameControllerObj;
    private int currentLife = 200; // Aktualna ilo�� �ycia
    private TextMeshPro lifeText;

    private int licznik;

    void Awake()
    {
        gameControllerObj = GameObject.Find("Main Camera");
        sciezka = gameControllerObj.GetComponent<PlacePath>();
        listaSciezki = sciezka.getSciezka();

    }
    void Start()
    {
        //currentLife = HP;
        CreateLifeText();
        UpdateLifeText();
        Vector3 skala = new Vector3(postac.transform.localScale.x + 0.2f, postac.transform.localScale.y + 0.2f, postac.transform.localScale.z + 0.2f);
        postac.transform.localScale = skala;
    }
    void CreateLifeText()
    {

        lifeText = new GameObject("LifeText").AddComponent<TextMeshPro>();
        lifeText.transform.SetParent(transform);
        lifeText.transform.localPosition = Vector3.up * 2f;
        lifeText.alignment = TextAlignmentOptions.Center;
        lifeText.fontSize = 600;
        lifeText.color = Color.red;
        lifeText.rectTransform.sizeDelta = new Vector2(200, 50); // Ustawienie rozmiaru
        lifeText.rectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f); // Ustawienie skali
        lifeText.rectTransform.rotation = Quaternion.LookRotation(transform.position - gameControllerObj.transform.position);
        //image = new GameObject("Tlo").AddComponent<UnityEngine.UI.Image>();
        // image.transform.SetParent(lifeText.transform);
        // image.rectTransform.sizeDelta = new Vector2(100, 50);


        //lifeText.rectTransform.rotation = Quaternion.LookRotation(transform.position - GameController.instance.transform.position);
    }

    void UpdateLifeText()
    {
        lifeText.text = "Life: " + currentLife.ToString();
    }

    public void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount;
        UpdateLifeText();

        // Dodaj dodatkow� logik�, np. sprawdzenie czy posta� umar�a, itp.
    }
    public PlacePath getPath()
    {
        return sciezka;
    }
    public void sprawdzCzyMinalCzas(float czas)
    {
        if (czas > 0.5f)
        {

            czasSpowolnienia = false;
            czasPodpalenia = false;
            timer = 0.0f;
            speed = 3f;
        }
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Kolce")
        {
            //currentLife = currentLife - 20;
            Debug.Log("Wlazlem w kolce-" + currentLife);
            TakeDamage(20);

            // Kod obs�uguj�cy kolizj� z obiektem o tagu "Przeszkoda"
        }
        else if (other.gameObject.tag == "Icing")
        {
            timer = 0.0f;
            speed = 2f;
            Debug.Log("Wlazlem lod-" + currentLife);
            czasSpowolnienia = true;

            // Kod obs�uguj�cy kolizj� z obiektem o tagu "Przeszkoda"
        }
        else if (other.gameObject.tag == "lawa")
        {
            timer = 0.0f;
            licznik = 1;
            Debug.Log("Wlazlem lawa-" + currentLife);
            czasPodpalenia = true;

        }

    }
    void OnCollisionEnter(Collision collision)
    {

        //currentLife = currentLife - 40;
        Debug.Log("Trafiony przez strza�e-" + currentLife);
        Destroy(collision.gameObject);
        TakeDamage(40);
    }


    void Update()
    {
        lifeText.rectTransform.rotation = Quaternion.LookRotation(transform.position - gameControllerObj.transform.position);


        if (czasSpowolnienia == true)
        {
            timer += Time.deltaTime;
            sprawdzCzyMinalCzas(timer);
        }
        if (czasPodpalenia == true)
        {
            if (timer > 0.15f * licznik)
            {
                TakeDamage(10);
                licznik++;
            }

            timer += Time.deltaTime;
            sprawdzCzyMinalCzas(timer);
        }

        aktualnaPozycja = transform.position;
        if (aktualnyKafelek < sciezka.naKtorymKafelkuSciezkiLezyPosag && aktualnyKafelek >= 0)
        {
            Vector3 targetPosition = new Vector3();
            if (listaSciezki[aktualnyKafelek] != null)
            {
                targetPosition.x = listaSciezki[aktualnyKafelek].x;
                targetPosition.z = listaSciezki[aktualnyKafelek].y;
                targetPosition.y = 0.5f;
                float krok = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, krok);

                // obliczamy wektor kierunku i tworzymy obr�t na podstawie kierunku
                Vector3 direction = (targetPosition - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    // rotacja w stron� kierunkow�
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
                }
            }
            if (aktualnaPozycja.x == listaSciezki[aktualnyKafelek].x && aktualnaPozycja.z == listaSciezki[aktualnyKafelek].y)
            {
                if (powrot == false)
                    aktualnyKafelek++;
                else
                {
                    if (aktualnyKafelek == 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                        aktualnyKafelek--;
                }
            }

        }
        else
        {
            if (sciezka.posag != null)
            {
                Destroy(sciezka.posag);
                Destroy(gameObject);

            }
            powrot = true;
            aktualnyKafelek--;
        }
        if (currentLife < 0)
        {
            float pozycjaX = (float)Math.Truncate(this.aktualnaPozycja.x);
            float pozycjaZ = (float)Math.Truncate(this.aktualnaPozycja.z);

            Vector3 deathpostion = new Vector3(pozycjaX, this.aktualnaPozycja.y, pozycjaZ);


            if (this.gameObject.tag == "EnemyWithStatue") // Czy umierajacy niosl posag
            {
                PlacePath.pozycjaPosagu = deathpostion;
                sciezka.posag = Instantiate(PlacePath.pobierzObiektPosagu(), deathpostion, Quaternion.identity);
                sciezka.naKtorymKafelkuSciezkiLezyPosag = aktualnyKafelek;
                //sciezka.SetStopPosition(deathpostion); //TD-39: To chyba nie potrzebne do podnoszenia posagu
            }
            Destroy(gameObject);

            EnemyManager.SetCzyPosagZabrany(false);
        }

    }


}