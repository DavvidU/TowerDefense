using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
 *@brief Klasa pozwala na ustawianie pu³apek na siatce [GridTile] mapy
 * < span style = "font-size: 16px; text-indent: 50px;" >
 *Wywo³anie konstruktora powinno nast¹piæ raz w funkcji start aby przydzieli³ wszelkie prywatne [GameObject] do odpowiednich plików w folderze Prefabs/Traps
 * </span>
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Dawid Ugniewski</span></i>
 */

public class PlaceTrap : MonoBehaviour
{
    private GameObject SimpleSpikes;

    private GameObject PushingTrap; //strzelaj¹ca

    private GameObject trap;

    private GameObject[] obiektySiatki;

    public PlaceTrap()
    {
        //skanuje katalog zawieraj¹cy pliki Pulapek/Kolcow
        string katalogZasobow = "Assets/Prefabs/Traps/Spikes";

        string katalogZasobowStrzelajacych = "Assets/Prefabs/Traps/PushingTraps";

        string[] sciezkiDoZasobowKolce = AssetDatabase.FindAssets("", new[] { katalogZasobow });
        string[] sciezkiDoZasobowStrzelajacych = AssetDatabase.FindAssets("", new[] { katalogZasobowStrzelajacych });
        

        //pobiera sciezki kolcow
        foreach (string sciezka in sciezkiDoZasobowKolce)
        {
            //narazie tylko jedna pulapka w Traps/Spikes
            string pelnaSciezka = AssetDatabase.GUIDToAssetPath(sciezka);

            GameObject zasob = AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezka);

            if (zasob != null && PrefabUtility.IsPartOfPrefabAsset(zasob))
            {
                if (zasob.name == "Spikes")
                {
                    this.SimpleSpikes = zasob;
                }
            }
        }

        //pobiera sciezki strzelajacych
        foreach (string sciezka in sciezkiDoZasobowStrzelajacych)
        {
            //narazie tylko jedna pulapka w Traps/Spikes
            string pelnaSciezka = AssetDatabase.GUIDToAssetPath(sciezka);

            GameObject zasob = AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezka);

            if (zasob != null && PrefabUtility.IsPartOfPrefabAsset(zasob))
            {
                if (zasob.name == "PushingSimple")
                {
                    this.PushingTrap = zasob;
                }
            }
        }

        //pobiera wszystkie obiekty opisane tagiem grid
        obiektySiatki = GameObject.FindGameObjectsWithTag("Grid");
    }

    /**
    * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda tworzy instancjê obiektu i umieszcza go w odpowiednim miejscu na siatce
    * 
    * <span style="font-size: 16px; text-indent: 50px;">
    *   Metoda skanuje wszystkie obiekty znajduj¹ce siê na siatce a nastêpnie w zale¿noœci od rodzaju figury zwraca odpowiedni gameObject na wyjœcie
    * </span>
    * 
    * @param gridTile [<span style="text-decoration: italic; font-weight: bold;">GridTile</span>] - Pobiera informacjê o obiekcie siatki na którym mamy 
    * zbudowaæ obiekt
    * 
    * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Dawid Ugniewski</span></i>
    */
    public void getTrap(int id, GridTile gridTile)
    {
        /*if (gridTile.isPath)
        {
            return;

        }*/

        this.trap = null;

        GameObject ob; //nowy obiekt na którym wykonujê operacjê transformacji rotacji

        switch (id)
        {
            case 1:
                ob = Instantiate(SimpleSpikes);
                break;
            default:
                ob = Instantiate(SimpleSpikes);
                break;
        }
       
        ob.transform.rotation = Quaternion.identity;
        ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);
        this.trap = ob;

        // Podaj informacjê do Tile, jaki obiekt przechowuje
        if (this.trap != null)
        {
            gridTile.BuildTrap(trap);
        }
    }

    public void getTrap(int id, TrapPoint trapPoint)
    {

        if(trapPoint.getIsFree() != false)
        {
            this.trap = null;

            GameObject ob; //nowy obiekt na którym wykonujê operacjê transformacji rotacji
            switch (id)
            {
                case 1:
                    ob = Instantiate(PushingTrap);
                    break;
                default:
                    ob = Instantiate(PushingTrap);
                    break;
            }
            ob.transform.SetParent(trapPoint.getTransform());
            ob.transform.rotation = trapPoint.getTransformRotation();
            ob.transform.localPosition = Vector3.zero;
            this.trap = ob;

            if (this.trap != null)
            {
                trapPoint.setBuildedObject(this.trap);
                trapPoint.setIsFree(false); //jest zajety
            }

        }
        else
        {
            Debug.Log("Pu³apka juz istnieje w tym punkcie");
        }

       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
