using System;
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
using UnityEngine.AI;
using Unity.AI.Navigation;

/**
 * @brief Klasa generuje obiekty siatki i przechowuje tablicê indywidualnie do³¹czonych do nich komponentów
 * <span style="font-size: 16px; text-indent: 50px;">
 *   Klasa wymaga obiektu, do którego zostanie do³¹czona jako skrypt oraz podania prefabu kafelka siatki
 *   Generuje mapWidth * mapHeight obiektów kafelków mapy i do³¹cza do ka¿dego z nich komponent typu [GridTile]
 *   Wszystkie do³¹czone komponenty (logiczne obiekty siatki) przechowywane s¹ w tej klasie w tablicy gridTiles.
 * </span>
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Dawid Ugniewski</span></i>
 */

public class GridGenerator : MonoBehaviour
{
    private static GridGenerator instance;

    [SerializeField] public int mapWidth;
    [SerializeField] public int mapHeight;
    [SerializeField] public float tileSize;
    [SerializeField] public float Money;
   
    //public GameObject obiektsciezki ;
    public GameObject gridTileObject;
    public GameObject pillar;

    private int liczbaFilarow = 8;

    private static GridTile[,] gridTiles;
    
    // wywolywane przed pierwszym updatem klatki
    void Awake()
    {
        mapHeight = 10;
        mapWidth = 15;
        Money = 100;
        GenerateGrid();
    }
    

    void GenerateGrid()
 {
     gridTiles = new GridTile[mapWidth, mapHeight];

     var pillars = new bool[mapWidth, mapHeight];

        int minWartosc = 1;

        for(int i=0; i< mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                pillars[i, j] = false;
            }
        }

        for (int i = 0; i < liczbaFilarow; i++)
        {
            int wylosowanaLiczbaW = UnityEngine.Random.Range(minWartosc, mapWidth - 1);
            int wylosowanaLiczbaH = UnityEngine.Random.Range(minWartosc, mapHeight - 1);
            pillars[wylosowanaLiczbaW, wylosowanaLiczbaH] = true;
        }

     for (int x = 0; x < mapWidth; x++)
     {
         for (int y = 0; y < mapHeight; y++)
         {
             Vector3 tilePosition = new Vector3(x * tileSize, 0, y * tileSize);
             GameObject tileObject = Instantiate(gridTileObject, tilePosition, Quaternion.identity);
             GridTile gridTile = tileObject.AddComponent<GridTile>();
             if(x == 0 || x== mapWidth-1 || y == 0 || y == mapHeight - 1)
             {
                 //granica
                 gridTile.Initialize(x, y, true, false, true);
             }
             else
             {
                 gridTile.Initialize(x, y, true, false, false);

             }
                if (pillars[x, y])
                {
                    Instantiate(pillar, tilePosition, Quaternion.identity);
                    gridTile.buildedPillar = pillar;
                    gridTile.removable = false;
                    gridTile.isBorder = true;
                }
           

             gridTiles[x, y] = gridTile;
         }
     } 
     instance = this;
 }

    // Wywolywane raz na klatke
    void Update()
    {
        
    }
    public static GridTile[,] GetAllTiles() { return gridTiles; }
    public static GridTile GetOneTile(int x, int y) { return gridTiles[x, y]; }
    public static void ModifyTitlePath(int x,int y,bool movable,bool pathable)
    {

      // gridTiles[x, y].SetMovable(movable);
        if(x< 14)
        gridTiles[x+1,y].pathable = pathable;
        if(x>0)
            gridTiles[x -1, y].pathable = pathable;
        if(y< 9)
            gridTiles[x, y+1].pathable = pathable;
    }

    public float getMoney()
    {
        return this.Money;
    }
    public bool removeMoney(float amount)
    {
        this.Money -= amount;
        return true;
    }
    public bool addMoney(float amount)
    {
        this.Money += amount;
        return true;
    }
    public bool setMoney(float amount)
    {
        this.Money = amount;
        return true;
    }

}

/**
 * @brief Klasa do³¹czana jako komponent do obiektów kafelków siatki wygenerowanych przez GridGenerator
 * <span style="font-size: 16px; text-indent: 50px;">
 *   
 *   
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Dawid Ugniewski</span></i>
 */

public class GridTile : MonoBehaviour
{
    public int x;
    public int y;
    public bool movable;
    public bool removable;
    public bool pathable;
    public bool isPath;
    
    public GameObject buildedObject;

    public GameObject buildedWall;
    public GameObject buildedTrap;
    public GameObject buildedPath;
    public GameObject buildedPillar;

    public bool isBorder;

    public void Initialize(int x, int y, bool movable, bool isPath,bool isBorder)
    {
        this.x = x;
        this.y = y;
        this.movable = movable;
        this.removable = true;
        this.pathable = false;
        this.isPath = false;
        
        buildedObject = null;

        buildedWall = null;
        buildedTrap = null;
        buildedPath = null;
        buildedPillar = null;
	    
        this.isBorder = isBorder;
    }
    // ------------------------------------------------------------
    public void BuildObject(GameObject objectToBuild)
    {
        if (objectToBuild == null) { }
        else
            buildedObject = objectToBuild;
    }
    // ------------------------------------------------------------
    public void BuildWall(GameObject wallToBuild)
    {
        if (wallToBuild == null) { }
        else
            buildedWall = wallToBuild;
    }

    public void BuildTrap(GameObject trapToBuild)
    {
        if (trapToBuild == null) { }
        else
            buildedTrap = trapToBuild;
    }

    public void BuildPath(GameObject pathToBuild)
    {
        if (pathToBuild == null) { }
        else
            buildedPath = pathToBuild;
    }

    public void SetMovable(bool movable)
    {
        this.movable = movable;
    }
    public void SetPathable(bool pathable) {  this.pathable = pathable; }
    public void SetisPath(bool path) { this.isPath = path; }


}

