using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using static ClickPoint;
using UnityEngine.EventSystems;
using TMPro;
using Unity.AI.Navigation;


/**
* Klasa ma za zadanie zarządzać trybem budoawnia/rozbiórki użytkownika 
* 
* śledzi promień kursora użytkownika i zaznacza pdpowieni element siatki na któym w przypadku 
* trybu budowania stawia przeszkodę lub w przypadku trybu rozbiórki przeszkodę usuwa.
* 
* @author Artur Leszczak
* @version 1.0.0
*/

public class TrapPrice
{
    private int Id;
    private float Price;

    /**
 * Konstruktor klasy TrapPrice, który inicjalizuje identyfikator i cenę pułapki.
 *
 * @param id Identyfikator pułapki
 * @param price Cena pułapki
 * @author Artur Leszczak
 * @version 1.0.0
 */
    public TrapPrice(int id, float price) {
    this.Id = id;
    this.Price = price;
}
    public float getPrice()
    {
        return Price;
    }
}

/**
 * Klasa reprezentująca punkt kliknięcia w grze, decydująca o ruchu kamery
 * oraz o wybranym miejscu do postawienia obiektu.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class ClickPoint : MonoBehaviour
{
    /**
     * Enum reprezentujące obiekty, które mogą być budowane w grze.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     */
    public enum BuldableObjects {
        Sciany,
        Sciezka,
        Pułapki
    }

    /**
     * Enum reprezentujące pułapki, które mogą być budowane w grze.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     */
    public enum BuldableTraps
    {
        Kolce,
        Strzelajaca,
        Lawa,
        Oblodzenie
    }

    public TextMeshProUGUI pieniadze;
    public bool inBuildMode = true;
    public bool inDestroyingMode = false;
    public BuldableObjects TrybBudowania;
    public BuldableTraps WybranaPułapka;
    private PlaceWall sciana;
    private PlacePath sciezka;
    private PlaceTrap pulapka;
    public Transform mainCamera;
    public Camera kamera;
    public GameObject cameraMother;
    private EnemyManager ManagerEnemy;
    public GameObject obiektEnemyManager;
    private ZarzadzajObiektami zarzadzanieObiektamiGlobalnymi;
    private float Mouser = 0f;

    private int mapWidth = 15;
    private int mapHeight = 10;

    private TrapPrice[] trapPrice;

    // private GlobalFunctions GlobalFunctions;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Wykonuje start");
        this.TrybBudowania = BuldableObjects.Sciany;
        this.sciana = gameObject.GetComponent<PlaceWall>();
        sciana.Zaladuj();
        this.sciezka = kamera.GetComponent<PlacePath>();
        this.pulapka = gameObject.GetComponent<PlaceTrap>();
        this.zarzadzanieObiektamiGlobalnymi = gameObject.AddComponent<ZarzadzajObiektami>();
        this.ManagerEnemy = obiektEnemyManager.GetComponent<EnemyManager>();

        TrapPrice trap1 = new TrapPrice(1, 10f);
        TrapPrice trap2 = new TrapPrice(2, 20f);
        TrapPrice trap3 = new TrapPrice(3, 10f);
        TrapPrice trap4 = new TrapPrice(4, 8f);

        this.trapPrice = new TrapPrice[] { trap1, trap2, trap3, trap4 };

        pieniadze.text = GlobalFunctions.getMoney().ToString();

    }
    void ZaktualizujPieniadze()
    {
        pieniadze.text = GlobalFunctions.getMoney().ToString();
    }
      
    void Update()
    {
        ZaktualizujPieniadze();

        if (sciezka.czySkonczonaSciezka())
        {
            ManagerEnemy.enabled = true;
        }
        if(this.WybranaPułapka == BuldableTraps.Strzelajaca && this.TrybBudowania == BuldableObjects.Pułapki)
        {
            zarzadzanieObiektamiGlobalnymi.setObjectsVisibilityByTag("TrapPlaceRender", true);
        }
        else
        {
            zarzadzanieObiektamiGlobalnymi.setObjectsVisibilityByTag("TrapPlaceRender", false);
        }

        //reaguje/niszczy na wciśnięcie lewego klawisza myszy
        if (!EventSystem.current.IsPointerOverGameObject())
            if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Deklaruj strukturę RaycastHit, aby przechwyci� informacje o kolizji
            RaycastHit hit;

            // Sprawdź, czy promień przecina obiekty z koliderem
            if (Physics.Raycast(ray, out hit))
            {
                //sprawdza czy znajdujemy się w trybie budowania / rozbiórki
                if (inBuildMode && !inDestroyingMode)
                {
                    // Utw�rz promie� od pozycji kamery przez punkt, w kt�ry klikn��e�

                    if(this.WybranaPułapka == BuldableTraps.Strzelajaca && this.TrybBudowania == BuldableObjects.Pułapki)
                    {
                        TrapPoint trapPoint = hit.collider.GetComponent<TrapPoint>();

                        if (trapPoint == null)
                            Debug.Log("Nie mozna w tym miejscu postawic pulapki strzelajacej");
                        else if (trapPoint.getIsFree() == false)
                            Debug.Log("Pułapka juz istnieje w tym punkcie");
                        else if (GlobalFunctions.removeMoney(trapPrice[1].getPrice())) // Poprawne miejsce && wolne miejsce && wystarczajaco pieniedzy
                        {
                            pulapka.getTrap(1, trapPoint);
                            Debug.Log("Pozostało pieniedzy: " + GlobalFunctions.getMoney());
                        }
                        else
                            Debug.Log("Nie masz wystarczająco dużo pieniędzy!");
                    }
                    else
                    {
                        GridTile gridTile = hit.collider.GetComponent<GridTile>();
                        //sprawdza czy wybraną opcją jest budowa ścian, pułapek czy ścieżki

                        if (this.TrybBudowania == BuldableObjects.Sciany && gridTile.movable == true && gridTile.buildedPillar == null)
                        {
                            //wywołuje metodę tworząca ścianę
                            sciana.getWall(gridTile);
                        }
                        else if (this.TrybBudowania == BuldableObjects.Pułapki && gridTile.movable == true && gridTile.buildedPillar == null)
                        {
                            //wywołuje metodę tworzącą pułapkę
                            if (gridTile.buildedTrap == null)
                            {
                                if (GlobalFunctions.removeMoney(trapPrice[(int)WybranaPułapka].getPrice()))
                                {
                                    pulapka.getTrap((int)WybranaPułapka, gridTile);
                                    Debug.Log("Pozostało pieniedzy: " + GlobalFunctions.getMoney());
                                        pieniadze.text = GlobalFunctions.getMoney().ToString();
                                }
                                else
                                {
                                    Debug.Log("Nie masz wystarczająco dużo pieniędzy! "+GlobalFunctions.getMoney());
                                }
                            }
                                

                        }
                        else if (this.TrybBudowania == BuldableObjects.Sciezka && gridTile.movable == true && gridTile.pathable == true && gridTile.buildedPillar == null)
                        {
                            sciezka.GetPath(gridTile);

                        }
                    }

                    
                }
                else if (inDestroyingMode && !inBuildMode)
                {
                    //w przypadku trybu rozbiórki niszczy obiekt na siatce i ponownie robi z niej movable

                    GridTile gridTile = hit.collider.GetComponent<GridTile>();

                    // TD-34: Poprawka bledu wynikajacego z wprowadzenia TD-24.
                    if (gridTile == null)
                    {
                        Transform tr = hit.collider.transform;

                        gridTile = GridGenerator.GetOneTile((int)tr.position.x, (int)tr.position.z);
                    }

                    if (gridTile.removable)
                    {
                        gridTile.movable = true;

                        if (TrybBudowania == BuldableObjects.Sciany)
                        {
                            Destroy(gridTile.buildedWall);
                            gridTile.buildedWall = null;
                        }
                        if (TrybBudowania == BuldableObjects.Pułapki)
                        {
                            Destroy(gridTile.buildedTrap);
                            gridTile.buildedTrap = null;
                        }
                        if (TrybBudowania == BuldableObjects.Sciezka)
                        {
                            GridGenerator.ModifyTitlePath(gridTile.x, gridTile.y, true, false);
                            GridTile obiekt = sciezka.ostatniobiekt();
                            GridGenerator.ModifyTitlePath(obiekt.x, obiekt.y, false, true);

                            Destroy(gridTile.buildedPath);
                            gridTile.buildedPath = null;
                        }
                    }
                    else
                    {
                        Debug.Log("Nie można usunąć!");
                    }
                    

                }
            }
                
        }
        

        //zmienia pozycję kamery
        if(Input.GetMouseButton(1))
        {
        
            if (Input.GetKey(KeyCode.LeftControl))
            {
                float rotationSpeed = 2.0f;

                float deltaPose = Input.GetAxis("Mouse X") * rotationSpeed;

                // Skoryguj rotację
                Mouser += deltaPose;

                Transform tr = cameraMother.GetComponent<Transform>();

                // Ustaw nową rotację kamery
                cameraMother.transform.rotation = Quaternion.Euler(tr.eulerAngles.x, Mouser, tr.eulerAngles.z);
            }else if (Input.GetKey(KeyCode.LeftAlt))
            {
                float rotationSpeed = 2.0f;

                float deltaPose = Input.GetAxis("Mouse Y") * rotationSpeed;

                // Skoryguj rotację
                Mouser += deltaPose;

                Transform tr = cameraMother.GetComponent<Transform>();

                // Ustaw nową rotację kamery
                cameraMother.transform.rotation = Quaternion.Euler(tr.eulerAngles.x, tr.eulerAngles.y, tr.eulerAngles.z);
            }
            else
            {
                float moveSpeed = 2f;
                float horizontalInput = Input.GetAxis("Horizontal"); // Pobierz wejście klawiszy A/D lub strzałki w lewo/prawo
                float verticalInput = Input.GetAxis("Vertical"); // Pobierz wejście klawiszy W/S lub strzałki w górę/dół

                Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

                Transform tr = cameraMother.GetComponent<Transform>();

                    Vector3 deltaPose = new Vector3(Input.mousePosition.x - (Screen.width / 2), 0, Input.mousePosition.y - (Screen.height / 2));
                    deltaPose /= 20000;

                    Vector3 newPosition = tr.position + tr.TransformDirection(deltaPose) * moveSpeed;

                    // Ogranicz nową pozycję do obszaru mapy
                    newPosition.x = Mathf.Clamp(newPosition.x, 0f, (float)mapWidth);
                    newPosition.z = Mathf.Clamp(newPosition.z, 0f, (float)mapHeight);

                    // Ustaw nową pozycję kamery
                    tr.position = newPosition;
                
            }
        }

        float zoomSpeed = 15.0f; // Szybkość przybliżania i oddalania
        float minZoom = 20.0f;    // Minimalne przybliżenie
        float maxZoom = 60.0f;   // Maksymalne przybliżenie
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        // Sprawdź, czy użytkownik używa scrolla
        if (Mathf.Abs(scrollDelta) > 0.0f)
        {
            // Pobierz aktualne przybliżenie kamery
            float currentZoom = kamera.fieldOfView;

            // Zmodyfikuj przybliżenie w zależności od kierunku ruchu scrolla
            currentZoom -= scrollDelta * zoomSpeed;

            // Ogranicz przybliżenie do określonych granic (min i max)
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            // Ustaw nowe przybliżenie kamery
            kamera.fieldOfView = currentZoom;
        }
    }
        
    }
