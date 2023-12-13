using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
 *@brief Klasa pozwala na ustawianie pu�apek na siatce [GridTile] mapy
 * < span style = "font-size: 16px; text-indent: 50px;" >
 *Wywo�anie konstruktora powinno nast�pi� raz w funkcji start aby przydzieli� wszelkie prywatne [GameObject] do odpowiednich plik�w w folderze Prefabs/Traps
 * </span>
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Dawid Ugniewski</span></i>
 */

public class PlaceTrap : MonoBehaviour
{
    private GameObject SimpleSpikes;

    private GameObject trap;

    private GameObject[] obiektySiatki;

    public PlaceTrap()
    {
        //skanuje katalog zawieraj�cy pliki Pulapek/Kolcow
        string katalogZasobow = "Assets/Prefabs/Traps/Spikes";

        string[] sciezkiDoZasobow = AssetDatabase.FindAssets("", new[] { katalogZasobow });

        foreach (string sciezka in sciezkiDoZasobow)
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

        //pobiera wszystkie obiekty opisane tagiem grid
        obiektySiatki = GameObject.FindGameObjectsWithTag("Grid");
    }

    /**
    * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda tworzy instancj� obiektu i umieszcza go w odpowiednim miejscu na siatce
    * 
    * <span style="font-size: 16px; text-indent: 50px;">
    *   Metoda skanuje wszystkie obiekty znajduj�ce si� na siatce a nast�pnie w zale�no�ci od rodzaju figury zwraca odpowiedni gameObject na wyj�cie
    * </span>
    * 
    * @param gridTile [<span style="text-decoration: italic; font-weight: bold;">GridTile</span>] - Pobiera informacj� o obiekcie siatki na kt�rym mamy 
    * zbudowa� obiekt
    * 
    * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Dawid Ugniewski</span></i>
    */
    public void getTrap(GridTile gridTile)
    {
        if (gridTile.isPath)
        {
            return;

        }

        this.trap = null;

        GameObject ob; //nowy obiekt na kt�rym wykonuj� operacj� transformacji rotacji

        ob = Instantiate(SimpleSpikes);
        ob.transform.rotation = Quaternion.identity;
        ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);
        this.trap = ob;

        // Podaj informacj� do Tile, jaki obiekt przechowuje
        if (this.trap != null)
        {
            gridTile.buildedObject = this.trap;
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
