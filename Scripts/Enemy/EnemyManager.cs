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
    bool czyPosagZabrany;

   

    void Start()
    {
        sciezka = kamera.GetComponent<PlacePath>();
        string katalogZasobow = "Assets/Prefabs/Enemy";
        czyPosagZabrany = false;
        sciezkiDoZasobow = AssetDatabase.FindAssets("", new[] { katalogZasobow });
        pelnaSciezkaWariant1 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[0]);
        pelnaSciezkaWariant2 = AssetDatabase.GUIDToAssetPath(sciezkiDoZasobow[1]);
    }
  
    public void GetEnemy()
    {
        GameObject postac;
        postac = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant1), sciezka.getStartPosition(), Quaternion.identity);
        postac.AddComponent<enemy>();
       
    }
   
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        // SprawdŸ, czy up³yn¹³ okreœlony interwa³ czasowy
        if (elapsedTime >= interval && iloscodmierzacz!=ilosc  )
        {
            iloscodmierzacz++;
            // Wykonaj kod, który ma byæ wywo³any po danym interwale czasowym
            GetEnemy();

            // Zresetuj licznik czasu
            elapsedTime = 0f;
        }
        if (sciezka.posag == null && czyPosagZabrany == false)
        {
            czyPosagZabrany = true;
            GameObject postac;
            postac = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezkaWariant2), sciezka.getStopPosition(), Quaternion.identity);
            postac.AddComponent<enemy>();
            postac.tag = "EnemyWithStatue";
            enemy pierwsza = postac.GetComponent<enemy>();
            pierwsza.aktualnyKafelek = sciezka.getSciezka().Count-1;
            pierwsza.powrot = true;
           
        }

    }

}
