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
using TMPro;
using System;

public class enemy : MonoBehaviour
{
   
    private PlacePath sciezka;
    public GameObject postac;
    List<GridTile> listaSciezki=new List<GridTile>();
    private Vector3 aktualnaPozycja;
    public int aktualnyKafelek=1;
    public float speed = 6f;
    public bool powrot = false;
   // public int HP=100;
    private bool czasSpowolnienia = false;
    private bool czasPodpalenia = false;
        private float timer = 0.0f;
    private GameObject gameControllerObj;
    private int currentLife = 100; // Aktualna ilo�� �ycia
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
       
    }
    void CreateLifeText()
    {
        lifeText = new GameObject("LifeText").AddComponent<TextMeshPro>();
        lifeText.transform.SetParent(transform);
        lifeText.transform.localPosition = Vector3.up * 2f;
        lifeText.alignment = TextAlignmentOptions.Center;
        lifeText.fontSize = 600;
        lifeText.color = Color.red;
        lifeText.rectTransform.sizeDelta = new Vector2(100, 50); // Ustawienie rozmiaru
        lifeText.rectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f); // Ustawienie skali
        lifeText.rectTransform.rotation = Quaternion.LookRotation(transform.position - gameControllerObj.transform.position);

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
       if(czas>1.0f )
        {

            czasSpowolnienia = false;
            czasPodpalenia = false;
            timer = 0.0f;
            speed = 6f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
       
    
        if (other.gameObject.tag == "Kolce")
        {
           //currentLife = currentLife - 20;
            Debug.Log("Wlazlem w kolce-"+currentLife);
            TakeDamage(20);

            // Kod obs�uguj�cy kolizj� z obiektem o tagu "Przeszkoda"
        }
       else if (other.gameObject.tag == "Icing")
        {
            speed = 2f;
            Debug.Log("Wlazlem lod-" + currentLife);
            czasSpowolnienia = true;
         
            // Kod obs�uguj�cy kolizj� z obiektem o tagu "Przeszkoda"
        }
        else if (other.gameObject.tag == "lawa")
        {
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
        if (czasSpowolnienia == true )
        {
            timer += Time.deltaTime;
            sprawdzCzyMinalCzas(timer);
        }
        if(czasPodpalenia == true)
        {
            if (timer > 0.3f*licznik)
            {
                TakeDamage(10);
                licznik++;
            }
           
            timer += Time.deltaTime;
            sprawdzCzyMinalCzas(timer);
        }

        aktualnaPozycja = transform.position;
        if (aktualnyKafelek < listaSciezki.Count && aktualnyKafelek >= 0)
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
        if(currentLife<0)
        {
            float pozycjaX = (float)Math.Truncate(this.aktualnaPozycja.x);
            float pozycjaZ = (float)Math.Truncate(this.aktualnaPozycja.z);

            Vector3 deathpostion = new Vector3(pozycjaX, this.aktualnaPozycja.y, pozycjaZ);

            if (this.gameObject.tag == "EnemyWithStatue")

            Instantiate(PlacePath.pobierzObiektPosagu(), deathpostion, Quaternion.identity);
            Destroy(gameObject);
                        
        }

    }


}

