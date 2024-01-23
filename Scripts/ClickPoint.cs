using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClickPoint;
using UnityEngine.EventSystems;


/**
* @brief Klasa ma za zadanie zarządzać trybem budoawnia/rozbiórki użytkownika 
* 
* <span style="font-size: 16px; text-indent: 30px;">
*     śledzi promień kursora użytkownika i zaznacza pdpowieni element siatki na któym w przypadku trybu budowania stawia przeszkodę lub w przypadku 
*     trybu rozbiórki przeszkodę usuwa.
* </span>
* @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
*/

public class ClickPoint : MonoBehaviour
{
    //deklaruje obiekty możliwe do wyubudowania
    public enum BuldableObjects
    {
        Sciany,
        Sciezka,
        Pułapki
    }

    public enum BuldableTraps
    {
        Kolce,
        Strzelajaca,
        Lawa,
        Oblodzenie
    }

    public bool inBuildMode = true;
    public bool inDestroyingMode = false;
    public BuldableObjects TrybBudowania;
    public BuldableTraps WybranaPułapka;
    private PlaceWall sciana;
    private PlacePath sciezka;
    private PlaceTrap pulapka;
    public Transform mainCamera;
    public Camera kamera;
    private EnemyManager ManagerEnemy;
    public GameObject obiektEnemyManager;
    private ZarzadzajObiektami zarzadzanieObiektamiGlobalnymi;
    private float Mousex = 0f;
    private float Mouser = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.TrybBudowania = BuldableObjects.Sciany;
        this.sciana = new PlaceWall();
        this.sciezka = kamera.GetComponent<PlacePath>();
        this.pulapka = new PlaceTrap();
        this.zarzadzanieObiektamiGlobalnymi = new ZarzadzajObiektami();
        this.ManagerEnemy = obiektEnemyManager.GetComponent<EnemyManager>();

    }

      
    void Update()
    {
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
                        //Debug.Log("xd");
                        TrapPoint trapPoint = hit.collider.GetComponent<TrapPoint>();
                        
                        pulapka.getTrap(1, trapPoint);
                    }
                    else
                    {
                        GridTile gridTile = hit.collider.GetComponent<GridTile>();
                        //sprawdza czy wybraną opcją jest budowa ścian, pułapek czy ścieżki

                        if (this.TrybBudowania == BuldableObjects.Sciany && gridTile.movable == true) // aktualnie(13.12.) sciane mozna postawic na pulapce (gridTile.buildedObject == true)
                        {
                            //wywołuje metodę tworząca ścianę
                            sciana.getWall(gridTile);

                        }
                        else if (this.TrybBudowania == BuldableObjects.Pułapki && gridTile.movable == true) // aktualnie(13.12.) pulapke mozna postawic na scianie
                        {
                            //wywołuje metodę tworzącą pułapkę
                            if (gridTile.buildedTrap == null)
                                pulapka.getTrap((int)WybranaPułapka, gridTile);

                        }
                        else if (this.TrybBudowania == BuldableObjects.Sciezka && gridTile.movable == true && gridTile.pathable == true)
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
        else if(Input.GetKeyDown(KeyCode.Space) && sciezka.czySciezkaStworzona==true )
        {
            ManagerEnemy.enabled = true;
           
        }
        
        //zmienia pozycję kamery
        if(Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                float deltaPose = Input.mousePosition.x - (Screen.width / 2);

                deltaPose -= Mouser;

                deltaPose /= 10;

                Transform tr = this.GetComponent<Transform>();

                Mouser = tr.rotation.y + deltaPose;
                float rotY = tr.rotation.y - deltaPose;
                if (rotY > 0f)
                {
                    rotY = 0f;
                }
                else if(rotY < -180f)
                {
                    rotY = -180f;
                }

                mainCamera.rotation = Quaternion.Euler(52.487f,rotY , tr.rotation.z);
            }
            else
            {
                float deltaPose = Input.mousePosition.x - (Screen.width / 2);

                deltaPose -= Mousex;

                deltaPose /= 10000;

                Transform tr = this.GetComponent<Transform>();
                Vector3 v3 = new Vector3(tr.position.x + deltaPose, tr.position.y, tr.position.z);

                Mousex = tr.position.x + deltaPose;

                mainCamera.position = v3;
            }
        }
    }
        
    }
