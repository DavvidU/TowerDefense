using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class enemy : MonoBehaviour
{
   
    private PlacePath sciezka;
    public GameObject postac;
    List<GridTile> listaSciezki=new List<GridTile>();
    private Vector3 aktualnaPozycja;
    public int aktualnyKafelek=1;
    public float speed = 3f;
    public bool powrot = false;
    private bool czasSpowolnienia = false;
    private bool czasPodpalenia = false;
        private float timer = 0.0f;
    private GameObject gameControllerObj;
    private int currentLife = 100; // Aktualna ilo�� �ycia
    private TextMeshPro lifeText;

    private int licznik;

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
        
    }
    void Start()
    {
        CreateLifeBar();
        UpdateLifeBar();
       
    }
    /**
     * Metoda tworząca pasek zdrowia przeciwnika 
     *
     * @author Dawid Ugniewski
     * @version 1.0.0
     */
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
        rectTransform.sizeDelta = new Vector2(100, 20);
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
    /**
     * Metoda aktualizująca pasek zdrowia przeciwnika 
     *
     * @author Dawid Ugniewski
     * @version 1.0.0
     */
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
       if(czas>0.5f)
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
            Debug.Log("Wlazlem w kolce-"+currentLife);
            if (PatronType.PatronDmgActive)
            {
                TakeDamage(30);
            }
            else { TakeDamage(20); }
        }
       else if (other.gameObject.tag == "Icing")
       {
            if(PatronType.PatronSlowActive) 
            {
                timer = 0.0f;
                speed = 1.5f;
                Debug.Log("Wlazlem lod-" + currentLife);
                czasSpowolnienia = true;
            }
            else 
            {
                timer = 0.0f;
                speed = 2f;
                Debug.Log("Wlazlem lod-" + currentLife);
                czasSpowolnienia = true;
            }
            
            iceEffect.GetComponent<ParticleSystem>().Play();
        }
        else if (other.gameObject.tag == "lawa")
        {
            timer=0.0f;
            licznik = 1;
            Debug.Log("Wlazlem lawa-" + currentLife);
            czasPodpalenia = true;
            fireEffect.GetComponent<ParticleSystem>().Play();
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        //currentLife = currentLife - 40;
        Debug.Log("Trafiony przez strza�e-" + currentLife);
        Destroy(collision.gameObject);
        if(PatronType.PatronDmgActive)
        { TakeDamage(50); }
        else
        TakeDamage(40);
    }

    /**
     * Metoda m.in. aktualizuje stan przeciwnika, obsługuje zdarzenie upuszczenia posągu
     *
     * @author Dawid Ugniewski, Nikola Osińska, Konrad, Kondracki
     * @version 1.0.0
     */
    void Update()
    {
        lifeBarBackground.transform.rotation = Quaternion.LookRotation(transform.position - gameControllerObj.transform.position);


        if (czasSpowolnienia == true )
        {
            timer += Time.deltaTime;
            sprawdzCzyMinalCzas(timer);
        }
        if(czasPodpalenia == true)
        {
            if (timer > 0.15f*licznik)
            {
                if(PatronType.PatronDmgActive) 
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

        aktualnaPozycja = transform.position;
        if (aktualnyKafelek < EnemyManager.sciezka.naKtorymKafelkuSciezkiLezyPosag && aktualnyKafelek >= 0)
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
                if(EnemyManager.czyPosagZabrany==true)
                {
                    powrot = true;
                }
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
            if(EnemyManager.sciezka.licznikZyciaPosag>0)
            {
                EnemyManager.sciezka.licznikZyciaPosag--;
                Destroy(gameObject);

            }else
            { 
            if (EnemyManager.sciezka.posag != null && EnemyManager.sciezka.licznikZyciaPosag==0)
            {
                Destroy(EnemyManager.sciezka.posag);
                Destroy(gameObject);

            }
            
            powrot = true;
            aktualnyKafelek--;
            }
        }
        if(currentLife<0)
        {
            float pozycjaX = (float)Math.Truncate(this.aktualnaPozycja.x);
            float pozycjaZ = (float)Math.Truncate(this.aktualnaPozycja.z);


            GlobalFunctions.addMoney(10);

            Vector3 deathpostion = new Vector3(pozycjaX, this.aktualnaPozycja.y, pozycjaZ);

            if (this.gameObject.tag == "EnemyWithStatue") // Czy umierajacy niosl posag
            {
               
                Debug.Log("Umieram z posagiem");
                Debug.Log("bla" + deathpostion.x + deathpostion.z);
                PlacePath.pozycjaPosagu = deathpostion;
                EnemyManager.sciezka.posag = Instantiate(PlacePath.pobierzObiektPosagu(), deathpostion, Quaternion.identity);
                EnemyManager.sciezka.naKtorymKafelkuSciezkiLezyPosag = aktualnyKafelek;
                EnemyManager.sciezka.StopPosition=(deathpostion); //TD-39: To chyba nie potrzebne do podnoszenia posagu
                EnemyManager.SetCzyPosagZabrany(false);
            }
            Debug.Log("usuwam sie");
            
            Destroy(gameObject);

            
        }

    }


}

