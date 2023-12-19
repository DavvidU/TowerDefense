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

/**
 * @brief Klasa pozwala na ustawianie scie¿ki na siatce [GridTile] mapy
 * <span style="font-size: 16px; text-indent: 50px;">
 *   Ustawiamy statyczny start i mete.
 *   Kafelki musz¹ byæ ze sob¹ po³¹czone aby tworzyæ spójn¹ œcie¿ke.(Obiekt scie¿ki mo¿na postawiæ tylko obok wczeœniej stworzonego kafelka b¹dŸ startu)
 * </span>
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Nikola Osiñska</span></i>
 */

public class PlacePath : MonoBehaviour
{

   // private List<Vector3> listasciezki = new List<Vector3> ();
   private List<GridTile> listasciezki = new List<GridTile> ();
    public GameObject obiektsciezki;
    Vector3 StartPosition;
    Vector3 StopPosition;
        Vector3 PozycjaSciezki;
    GameObject Sciezka;
   public bool czySciezkaStworzona;
    GridTile gridTileStop;
    public GameObject posag;
    public GameObject postawionyPosag;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector3(5, 0, 0);
         StopPosition = new Vector3(10, 0, 9);
         PozycjaSciezki = new Vector3();
        czySciezkaStworzona = false;
        GameObject start = Instantiate(obiektsciezki, StartPosition, Quaternion.identity);
        GridTile gridTile = start.AddComponent<GridTile>();
        gridTile.Initialize(5, 0, false, false, false);
        gridTile.SetisPath(true);


        GameObject Stop = Instantiate(obiektsciezki, StopPosition, Quaternion.identity);
        gridTileStop = Stop.AddComponent<GridTile>();
        gridTileStop.Initialize(10, 9, false, false, false);
        gridTileStop.SetisPath(true);
        Vector3 pozycjaPosag = new Vector3(StopPosition.x, 0.5f, StopPosition.z);
        posag = Instantiate(posag, pozycjaPosag, Quaternion.identity);


        GridGenerator.ModifyTitlePath(5,0, false,true);
        GridGenerator.ModifyTitlePath(10,9, false,false);
       // listasciezki.Add(StartPosition);
       listasciezki.Add(gridTile);
    }
    /**
 * @brief Metoda usuwa ostatni obiekt oraz zwraca przedostatni
 * <span style="font-size: 16px; text-indent: 50px;">
 * </span>
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Nikola Osiñska</span></i>
 */
    public GridTile ostatniobiekt()
    {
        listasciezki.Remove(listasciezki[listasciezki.Count-1]);
        return listasciezki[listasciezki.Count-1];
    }
    public Vector3 getStartPosition()
    {
        Vector3 pozycja;
        pozycja = StartPosition;
        pozycja.y += 2;
        return pozycja;
    }
    public Vector3 getStopPosition()
    {
        Vector3 pozycja;
        pozycja = StopPosition;
        pozycja.y += 0.5f;
        return pozycja;
    }

    public List<GridTile> getSciezka()
    {
        return listasciezki;
    }

    /**
 * @brief Metoda tworzy instancje scie¿ki
 * <span style="font-size: 16px; text-indent: 50px;">
 * </span>
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Nikola Osiñska</span></i>
 */
    public void GetPath(GridTile gridTile)
    {
            //przed dodaniem nowego obiektu do listy usuwamy mo¿liwoœæ stawiania obiektów obok ostatniego na liœcie
            GridGenerator.ModifyTitlePath((int)listasciezki[listasciezki.Count - 1].x, (int)listasciezki[listasciezki.Count - 1].y, false, false);

 
           //sprawdzamy czy dotarlismy do mety
        if (gridTile.x == StopPosition.x - 1 && gridTile.y == StopPosition.z || gridTile.x == StopPosition.x + 1 && gridTile.y == StopPosition.z || gridTile.y == StopPosition.z - 1 && gridTile.x == StopPosition.x)
        {
            //jesli tak to usuwamy mozliwosc tworzenia sciezki
            GridGenerator.ModifyTitlePath(gridTile.x, gridTile.y, false, false);
            czySciezkaStworzona = true;
            listasciezki.Add(gridTileStop);

        }else
            GridGenerator.ModifyTitlePath(gridTile.x, gridTile.y, false, true);
        //jesli nie to umo¿liwiamy stawianie sciezki obok nowego obiektu
        
        gridTile.SetisPath(true);

        listasciezki.Add(gridTile);
       // Debug.Log(listasciezki.Count);
        //inicjujemy nowy obiekt i przypisujemy go do zmiennej buildobject w gridtile
            Sciezka = Instantiate(obiektsciezki);
            Sciezka.transform.rotation = Quaternion.identity;
            Sciezka.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);
        gridTile.BuildPath(Sciezka);


    }
}


