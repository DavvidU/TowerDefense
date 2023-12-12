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

public class enemy : MonoBehaviour
{
   
    private PlacePath sciezka;
    public GameObject postac;
    List<GridTile> listaSciezki=new List<GridTile>();
    private Vector3 aktualnaPozycja;
    public int aktualnyKafelek=1;
    public float speed = 8f;
    public bool powrot = false;
    
   
    void Awake()
    {
        GameObject gameControllerObj = GameObject.Find("Main Camera");
        sciezka = gameControllerObj.GetComponent<PlacePath>();
        listaSciezki = sciezka.getSciezka();
        
    }
    public PlacePath getPath()
    {
        return sciezka;
    }
    void Update()
    {
        aktualnaPozycja = transform.position;
        if (aktualnyKafelek < listaSciezki.Count - 1  &&  aktualnyKafelek >= 0)
        {
            Vector3 pozycja = new Vector3();
             if (aktualnaPozycja.x == listaSciezki[aktualnyKafelek].x && aktualnaPozycja.z== listaSciezki[aktualnyKafelek].y )
            {

                if(powrot==false)
                  aktualnyKafelek++;
                else
                {
                    aktualnyKafelek--;
                }

            }

            if (listaSciezki[aktualnyKafelek] != null)
            {
                pozycja.x = listaSciezki[aktualnyKafelek].x;
                pozycja.z = listaSciezki[aktualnyKafelek].y;
                pozycja.y = 0.5f;
                float krok = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, pozycja, krok);
                
            }
            
        }
        else if (aktualnyKafelek<0 && powrot==true)
        {
            Destroy(gameObject);
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
    }

}

