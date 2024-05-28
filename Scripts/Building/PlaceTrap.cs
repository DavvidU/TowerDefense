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
    /* Zmienne przechowujace prefaby pulapek */
    [SerializeField]
    private GameObject SimpleSpikes;
    [SerializeField]
    private GameObject PushingTrap; //strzelajaca
    [SerializeField]
    private GameObject LavaTrap;
    [SerializeField]
    private GameObject IcingTrap;

    private GameObject trap;

    private GameObject[] obiektySiatki;

    /*
    public PlaceTrap()
    {
        //skanuje katalog zawieraj¹cy pliki Pulapek
        string katalogZasobowKolce = "Assets/Prefabs/Traps/Spikes";
        string katalogZasobowStrzelajacych = "Assets/Prefabs/Traps/PushingTraps";
        string katalogZasobowLawa = "Assets/Prefabs/Traps/Lava";
        string katalogZasobowOblodzenie = "Assets/Prefabs/Traps/Icing";

        string[] sciezkiDoZasobowKolce = AssetDatabase.FindAssets("", new[] { katalogZasobowKolce });
        string[] sciezkiDoZasobowStrzelajacych = AssetDatabase.FindAssets("", new[] { katalogZasobowStrzelajacych });
        string[] sciezkiDoZasobowLawa = AssetDatabase.FindAssets("", new[] { katalogZasobowLawa });
        string[] sciezkiDoZasobowOblodzenie = AssetDatabase.FindAssets("", new[] { katalogZasobowOblodzenie });

        // Znajdowanie prefabu Kolcow w odpowiednich zasobach
        foreach (string sciezka in sciezkiDoZasobowKolce)
        {
            string pelnaSciezka = AssetDatabase.GUIDToAssetPath(sciezka);

            GameObject zasob = AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezka);

            if (zasob != null && PrefabUtility.IsPartOfPrefabAsset(zasob))
            {
                if (zasob.name == "Spikes")
                {
                    SimpleSpikes = zasob;
                }
            }
        }

        //pobiera sciezki strzelajacych
        foreach (string sciezka in sciezkiDoZasobowStrzelajacych)
        {
            string pelnaSciezka = AssetDatabase.GUIDToAssetPath(sciezka);

            GameObject zasob = AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezka);

            if (zasob != null && PrefabUtility.IsPartOfPrefabAsset(zasob))
            {
                if (zasob.name == "PushingSimple")
                {
                    PushingTrap = zasob;
                }
            }
        }

        // Znajdowanie prefabu Lawy w odpowiednich zasobach
        foreach (string sciezka in sciezkiDoZasobowLawa)
        {
            string pelnaSciezka = AssetDatabase.GUIDToAssetPath(sciezka);

            GameObject zasob = AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezka);

            if (zasob != null && PrefabUtility.IsPartOfPrefabAsset(zasob))
            {
                if (zasob.name == "Lava 1")
                    LavaTrap = zasob;
            }
        }

        // Znajdowanie prefabu Oblodzenia w odpowiednich zasobach
        foreach (string sciezka in sciezkiDoZasobowOblodzenie)
        {
            string pelnaSciezka = AssetDatabase.GUIDToAssetPath(sciezka);

            GameObject zasob = AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezka);

            if (zasob != null && PrefabUtility.IsPartOfPrefabAsset(zasob))
            {
                if (zasob.name == "Icing 1")
                    IcingTrap = zasob;
            }
        }

        //pobiera wszystkie obiekty opisane tagiem grid
        obiektySiatki = GameObject.FindGameObjectsWithTag("Grid");
    }
    */
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
    public void getTrap(int rodzajPulapki, GridTile gridTile)
    {
        /*if (gridTile.isPath)
        {
            return;

        }*/

        this.trap = null;

        GameObject ob; //nowy obiekt na którym wykonujê operacjê transformacji rotacji

        /* 
         *  Rodzaje pulapek:
         *  0 - Kolce
         *  1 - Strzelajaca
         *  2 - Lawa
         *  3 - Oblodzenie
         *  W tym przeciazeniu getTrap() nigdy nie zachodzi: (rodzajPulapki == 1)
         */

        switch (rodzajPulapki)
        {
            case 0:
                ob = Instantiate(SimpleSpikes);
                break;
            case 1: // to nigdy nie nastapi w tym przeciazeniu metody getTrap
                ob = Instantiate(SimpleSpikes);
                break;
            case 2:
                ob = Instantiate(LavaTrap);
                break;
            case 3:
                ob = Instantiate(IcingTrap);
                break;
            default:
                ob = Instantiate(SimpleSpikes);
                break;
        }
       
        ob.transform.rotation = Quaternion.identity;
        if (rodzajPulapki == 2 || rodzajPulapki == 3)
            ob.transform.position = new Vector3(gridTile.x, 0.055f, gridTile.y);
        else
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
