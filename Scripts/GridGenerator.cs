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
 * 
 * @author Dawid Ugniewski
 */

public class GridGenerator : MonoBehaviour
{
    private static GridGenerator instance;

    [SerializeField] public int mapWidth;
    [SerializeField] public int mapHeight;
    [SerializeField] public float tileSize;
   
    //public GameObject obiektsciezki ;
    public GameObject gridTileObject;

    private static GridTile[,] gridTiles;
    
    // wywolywane przed pierwszym updatem klatki
    void Awake()
    {
        mapHeight = 10;
        mapWidth = 15;
        GenerateGrid();
       
    }

    void GenerateGrid()
 {
     gridTiles = new GridTile[mapWidth, mapHeight];

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
             
             gridTiles[x, y] = gridTile;
         }
     } 
     instance = this;
 }

    // Wywolywane raz na klatke
    void Update()
    {
        
    }
    public static GridTile[,] GetMapTiles()
    {
        return gridTiles;
    }
    public static void ModifyTitlePath(int x,int y,bool movable,bool pathable)
    {

      // gridTiles[x, y].SetMovable(movable);
        if(x< 14)
        gridTiles[x+1,y].pathable = pathable;
        if(x>0)
            gridTiles[x -1, y].pathable = pathable;
        if(y< 9)
            gridTiles[x, y+1].pathable = pathable;

        
            gridTiles[x, y].removable = pathable;
        
    }

}

/**
 * @brief 
 * 
 * @author Dawid Ugniewski
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
	this.isBorder = isBorder;
    }
    
    public void BuildObject(GameObject objectToBuild)
    {
        if (objectToBuild == null) { }
        else
            this.buildedObject = objectToBuild;
    }
    
    public void SetMovable(bool movable)
    {
        this.movable = movable;
    }
    public void SetPathable(bool pathable) {  this.pathable = pathable; }
    public void SetisPath(bool path) { this.isPath = path; }
}

