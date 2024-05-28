using UnityEngine;


/**
 * @brief Klasa pozwala na ustawianie przeszkód na siatce [GridTile] mapy
 * <span style="font-size: 16px; text-indent: 50px;">
 *   Wywołanie konstruktora powinno nastąpić raz w funkcji start aby przydzielił wszelkie prywatne [GameObject] do odpowiednich plików w folderze Prefabs/Walls
 *   Ustawione przy pomocy tej klasy obiekty powinny automacztynie wpływac na siebie nawzajem tak aby zachować spójną ścieżkę ścian. 
 * </span>
 * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
 */

public class PlaceWall : MonoBehaviour
{
    //Prywatne enum opisujące pozycję (patrząc od obiektu który ją otrzyma) na obiekt który jest "parent'em"
    private enum Pos
    {
        Left, Right, Top, Bottom
    }

    

    [SerializeField]
    private GameObject SingleWallObject;
    [SerializeField]
    private GameObject StraightWallObject;
    [SerializeField]
    private GameObject TWallObject;
    [SerializeField]
    private GameObject EndWallObject;
    [SerializeField]
    private GameObject CrossWallObject;
    [SerializeField]
    private GameObject CornerWallObject;

    private GameObject wall;
    private string nazwa;

    private GetNeaghbours sasiad;

    private GameObject[] obiektySiatki;
    /**
   * @brief [<span style="color: lightblue; font-weight: bold;">Publiczny</span>] Konstruktor pobierający do klasy dane o obiektach składających się na ściany w katalogu gry
   * 
   * <span style="font-size: 16px; text-indent: 50px;">
   *   Konstruktor z zadanego katalogu skanuje wszystkie assety następnie exportuje zasoby które są Prefabami , przy użyciu funkcji foreach , na koniec porównuję nazwę
   *   pobranego pliku z nazwą zadeklarowaną w instrukcji warunkowej konstruktora i na tej podstawie przypisuje do odpowiedniej stałej zawartej w ramach klasy.
   *   
   *   Wystarczy podmienić prefab na nowy , ważne aby miał tę samą nazwę co prefab właściwy, a gra klasa zacznie z niego korzystać.
   * </span>
   * 
   * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
   */
    
    public void Zaladuj()
    {
        
        
        sasiad = new GetNeaghbours();
        obiektySiatki = GameObject.FindGameObjectsWithTag("Grid");

    }
    
    /**
    * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda tworzy instancję obiektu i umieszcza go w odpowiednim miejscu na siatce
    * 
    * <span style="font-size: 16px; text-indent: 50px;">
    *   Metoda skanuje wszystkie obiekty znajdujące się na siatce a następnie w zależności od rodzaju figury zwraca odpowiedni gameObject na wyjście
    * </span>
    * 
    * @param gridTile [<span style="text-decoration: italic; font-weight: bold;">GridTile</span>] - Pobiera informację o obiekcie siatki na którym mamy 
    * zbudować obiekt
    * 
    * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
    */
    public void getWall(GridTile gridTile)
    {
        if(gridTile.isPath)
        { 
            return; 
        
        }
        //zmienne boolowskie symbolizujące czy jakieś miejsce wokoło jest zajmowane przez jakiś obiekt
        bool Left = false;
        bool Right = false;
        bool Top = false;
        bool Down = false;

        //obiekty siatki sąsiadujące z przekazanym przez parametr gridTile
        GridTile objectLeft = null;
        GridTile objectRight = null;
        GridTile objectTop = null;
        GridTile objectDown = null;

        //wyszukuje wszystkie obiekty z tagiem Grid

        this.wall = null;
        //dla każdego obiektu sprawdza czy jest on sąsiadem oraz ustala jakiego typu obiekt jest sąsiadem
        //pobiera wartości sąsiadów konstruując nowy obiekt klasy sąsiad
        sasiad = new GetNeaghbours(gridTile, this.obiektySiatki);

        //sprawdza obiekty znajdujące się naokoło obiektu budowanego i przypisuje wartości
        Left = sasiad.getMovableLeft();
        Right = sasiad.getMovableRight();
        Top = sasiad.getMovableTop();
        Down = sasiad.getMovableDown();

        //przypisuje obiekty znajdujące się wokoło
        objectLeft = sasiad.getObjectLeft();
        objectRight = sasiad.getObjectRight();
        objectTop = sasiad.getObjectTop();
        objectDown = sasiad.getObjectDown();

        
        GameObject ob; //nowy obiekt na którym wykonuję operację transformacji rotacji

        //na podstawie otrzymanych danych o sąsiadach duduje w danym miejscu odpowiedni rodzaj ściany
        //true - oznacza puste miejsce
        if (Left == true && Right == true && Top == true && Down == true)
        {
            //tworzy klon obiektu
            ob = Instantiate(SingleWallObject);
            ob.transform.rotation = Quaternion.identity;
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);
            this.wall = ob;
        }
        else if (Left == false && Right == false && Top == true && Down == true)
        {

            ob = Instantiate(StraightWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;
            this.CheckToEdit(objectLeft, Pos.Right);
            this.CheckToEdit(objectRight, Pos.Left);
        }
        else if (Left == true && Right == true && Top == false && Down == false)
        {
            ob = Instantiate(StraightWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;
            this.CheckToEdit(objectTop, Pos.Bottom);
            this.CheckToEdit(objectDown, Pos.Top);
        }
        else if (Left == false && Right == false && Top == false && Down == false)
        {

            ob = Instantiate(CrossWallObject);
            ob.transform.rotation = Quaternion.identity;
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);
            this.wall = ob;

            this.CheckToEdit(objectRight, Pos.Left);
            this.CheckToEdit(objectDown, Pos.Top);
            this.CheckToEdit(objectLeft, Pos.Right);
            this.CheckToEdit(objectTop, Pos.Bottom);
        }
        else if (Left == false && Right == true && Top == false && Down == true)
        {
            ob = Instantiate(CornerWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectLeft, Pos.Right);
            this.CheckToEdit(objectTop, Pos.Bottom);
        }
        else if (Left == false && Right == true && Top == true && Down == false)
        {
            ob = Instantiate(CornerWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, +90f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;
            this.CheckToEdit(objectLeft, Pos.Right);
            this.CheckToEdit(objectDown, Pos.Top);
        }
        else if (Left == true && Right == false && Top == true && Down == false)
        {
            ob = Instantiate(CornerWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectRight, Pos.Left);
            this.CheckToEdit(objectDown, Pos.Top);

        }
        else if (Left == true && Right == false && Top == false && Down == true)
        {

            ob = Instantiate(CornerWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, +270f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectRight, Pos.Left);
            this.CheckToEdit(objectTop, Pos.Bottom);
        }
        else if (Left == false && Right == true && Top == true && Down == true)
        {

            ob = Instantiate(EndWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectLeft, Pos.Right);


        }
        else if (Left == true && Right == false && Top == true && Down == true)
        {
            ob = Instantiate(EndWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectRight, Pos.Left);

        }
        else if (Left == true && Right == true && Top == false && Down == true)
        {
            ob = Instantiate(EndWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectTop, Pos.Bottom);

        }
        else if (Left == true && Right == true && Top == true && Down == false)
        {
            ob = Instantiate(EndWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectDown, Pos.Top);
        }
        else if (Left == true && Right == false && Top == false && Down == false)
        {

            ob = Instantiate(TWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectTop, Pos.Bottom);
            this.CheckToEdit(objectDown, Pos.Top);
            this.CheckToEdit(objectRight, Pos.Left);

        }
        else if (Left == false && Right == true && Top == false && Down == false)
        {
            ob = Instantiate(TWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectLeft, Pos.Right);
            this.CheckToEdit(objectDown, Pos.Top);
            this.CheckToEdit(objectTop, Pos.Bottom);
        }
        else if (Left == false && Right == false && Top == true && Down == false)
        {
            ob = Instantiate(TWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, +90f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;
            this.CheckToEdit(objectLeft, Pos.Right);
            this.CheckToEdit(objectRight, Pos.Left);
            this.CheckToEdit(objectDown, Pos.Top);
        }
        else if (Left == false && Right == false && Top == false && Down == true)
        {
            ob = Instantiate(TWallObject);
            ob.transform.rotation = Quaternion.Euler(0f, +270f, 0f);
            ob.transform.position = new Vector3(gridTile.x, 0f, gridTile.y);

            this.wall = ob;

            this.CheckToEdit(objectLeft, Pos.Right);
            this.CheckToEdit(objectRight, Pos.Left);
            this.CheckToEdit(objectTop, Pos.Bottom);
        }


        if (this.wall != null)
        {
            gridTile.movable = false;
            gridTile.BuildWall(wall);
        }

    }

    /**
  * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda wykonuje aktualizację otoczenia do określonego obiektu
  * 
  * <span style="font-size: 16px; text-indent: 50px;">
  *   Metoda usuwa poprzedni obiekt zawarty w gridTile ze sceny i na jego miejscu tworzy nowy obiektprzekazany mu poprzez prametr [GameObject] Object,
  *   Następnie ustawia pozycję nowego obiektu oraz jego rotację , na końcu przypisuje obiekt do odpowiedniego gridTile oraz ponownie ustawia wartość [gridTile]
  *   movable na false.
  * </span>
  * @param gridTile [<span style="text-decoration: italic; font-weight: bold;">GridTile</span>] - Pobiera informację o obiekcie siatki na którym mamy 
  * zaktualizować obiekt 
  * @param Object [<span style="text-decoration: italic; font-weight: bold;">GameObject</span>] - Instancja (klon) obiektu który ma zostać przypisany
  * do miejsca na siatce 
  * @param eulerRotation [<span style="text-decoration: italic; font-weight: bold;">Quaternion</span>] - Przekazanie poprzez parametr rotacji obiektu
  * 
  * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
  */
    public void EditObject(GridTile gridTile, GameObject Object, Quaternion eulerRotation)
    {
        if(gridTile.isPath == false)
        {
            GameObject toRemove = gridTile.buildedWall;
            Destroy(toRemove);

            Vector3 position = new Vector3(gridTile.x, 0f, gridTile.y);
            Object.transform.rotation = Quaternion.identity;
            Object.transform.rotation = eulerRotation;
            Object.transform.position = position;

            gridTile.BuildWall(Object);
            gridTile.movable = false;
        }
    }

    /**
* @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwraca [GameObject] wygenerowany przez metodę getWall
* 
* <span style="font-size: 16px; text-indent: 50px;">
*   Metoda zwraca na wyjście obiekt typu [GameObject] zawierajacy obiekt wygenerowany przy użyciu metody [getWall()]
* </span>
* 
* @return <span style="color:green;"><B>[POWODZENIE]</B> W przypdaku powodzenia zwraca <B>[GameObject]</B> odpwoiedniej ściany</span>
* @return <span style="color:red;"><B>[NIEPOWODZENIE]</B> W przypdaku niepowodzenia zwraca <B>[NULL]</B></span> 
* 
* @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
*/
    public GameObject returnGameObject()
    {
        return this.wall;
    }

    /**
     * @brief [<span style="color: lightblue; font-weight: bold;">Prywatna</span>] Metoda wykonuje aktualizację otoczenia do określonego obiektu
     * <span style="font-size: 16px; text-indent: 50px;">
     *   
     * </span>
     * @param gridTile [<span style="text-decoration: italic; font-weight: bold;">GridTile</span>] - Pobiera informację o obiekcie siatki na którym mamy 
     * zaktualizować obiekt 
     * @param parent [<span style="text-decoration: italic; font-weight: bold;">Pos</span>] - Enum klasy deklarujacy po której stronie od aktualnego gridTile znajduje sie
     * obiekt zgłaszający konieczność sprawdzenia edycji pobliskich ścian.
     * 
     * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
     */
   private void CheckToEdit(GridTile gridTile, Pos parent)
    {
        if(gridTile == null)
        {
            Debug.Log("Gridtile to null");
        }else
        {
            Debug.Log("Gridtile to nie jest null");
        }
        if(obiektySiatki == null)
        {
            Debug.Log("Obiekty siatki to null");
        }
        GetNeaghbours gn = new GetNeaghbours(gridTile, obiektySiatki);

        bool Left = gn.getMovableLeft();
        bool Right = gn.getMovableRight();
        bool Top = gn.getMovableTop();
        bool Down = gn.getMovableDown();

        if (parent == Pos.Left) { Left = false; }
        else if (parent == Pos.Right) { Right = false; }
        else if (parent == Pos.Top) { Top = false; }
        else if (parent == Pos.Bottom) { Down = false; }

        if (Left == true && Right == true && Top == true && Down == true)
        {
            this.EditObject(gridTile, Instantiate(SingleWallObject), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (Left == false && Right == false && Top == true && Down == true)
        {
            this.EditObject(gridTile, Instantiate(StraightWallObject), Quaternion.Euler(0f, 90f, 0f));
        }
        else if (Left == true && Right == true && Top == false && Down == false)
        {
            this.EditObject(gridTile, Instantiate(StraightWallObject), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (Left == false && Right == false && Top == false && Down == false)
        {
            this.EditObject(gridTile, Instantiate(CrossWallObject), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (Left == false && Right == true && Top == false && Down == true)
        {
            this.EditObject(gridTile, Instantiate(CornerWallObject), Quaternion.Euler(0f, 180f, 0f));
        }
        else if (Left == false && Right == true && Top == true && Down == false)
        {
            this.EditObject(gridTile, Instantiate(CornerWallObject), Quaternion.Euler(0f, +90f, 0f));
        }
        else if (Left == true && Right == false && Top == true && Down == false)
        {
            this.EditObject(gridTile, Instantiate(CornerWallObject), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (Left == true && Right == false && Top == false && Down == true)
        {
            this.EditObject(gridTile, Instantiate(CornerWallObject), Quaternion.Euler(0f, 270f, 0f));
        }
        else if (Left == false && Right == true && Top == true && Down == true)
        {
            this.EditObject(gridTile, Instantiate(EndWallObject), Quaternion.Euler(0f, 90f, 0f));
        }
        else if (Left == true && Right == false && Top == true && Down == true)
        {
            this.EditObject(gridTile, Instantiate(EndWallObject), Quaternion.Euler(0f, 270f, 0f));

        }
        else if (Left == true && Right == true && Top == false && Down == true)
        {
            this.EditObject(gridTile, Instantiate(EndWallObject), Quaternion.Euler(0f, 180f, 0f));

        }
        else if (Left == true && Right == true && Top == true && Down == false)
        {
            this.EditObject(gridTile, Instantiate(EndWallObject), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (Left == true && Right == false && Top == false && Down == false)
        {
            this.EditObject(gridTile, Instantiate(TWallObject), Quaternion.Euler(0f, 0f, 0f));

        }
        else if (Left == false && Right == true && Top == false && Down == false)
        {
            this.EditObject(gridTile, Instantiate(TWallObject), Quaternion.Euler(0f, 180f, 0f));
        }
        else if (Left == false && Right == false && Top == true && Down == false)
        {
            this.EditObject(gridTile, Instantiate(TWallObject), Quaternion.Euler(0f, 90f, 0f));
        }
        else if (Left == false && Right == false && Top == false && Down == true)
        {
            this.EditObject(gridTile, Instantiate(TWallObject), Quaternion.Euler(0f, +270f, 0f));
        }

    }

    /**
      * @brief [<span style="color: lightblue; font-weight: bold;">Prywatna</span>] Klasa sprawdzajaca sąsiednie pola gridTile czy są movable
      * <span style="font-size: 16px; text-indent: 50px;">
      *  Klasa ustala inforację tylu bool o obiektach sąsiadujących z obiektem parent, znajdujących się na lewo/prawo u góry i dołu patrząc od obiektu bazowego.
      * </span>
      * 
      * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
      */
    private class GetNeaghbours
    {
        bool Left;
        bool Right;
        bool Up;
        bool Down;

        bool isBorder;

        GridTile objectLeft = null;
        GridTile objectRight = null;
        GridTile objectTop = null;
        GridTile objectDown = null;

        GridTile me; 
        /**
        * @brief [<span style="color: lightblue; font-weight: bold;">Prywatna</span>] Metoda konstruktora uzupełnia wszystkie parametry w klasie wykonywana na początku przy inicjalizacji
        * <span style="font-size: 16px; text-indent: 50px;">
        *   
        * </span>
        * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
        */
        public GetNeaghbours()
        {
            this.Left = false;
            this.Right = false;
            this.Up = false;
            this.Down = false;

            this.isBorder = true;

            objectLeft = null;
            objectRight = null;
            objectTop = null;
            objectDown = null;

        }
        /**
        * @brief [<span style="color: lightblue; font-weight: bold;">Prywatna</span>] Metoda konstruktora uzupełniajaca parametry w klasie o odpowiednie parametry
        * <span style="font-size: 16px; text-indent: 50px;">
        *   Wykonuje pentlę foreach i dla każgego obiektu reprezentującego siatkę wyłapuje komponent klasy gridTile i na jego podstawie określa sąsiadów , następnie sprawdza
        *   czy sąsiad ma pole movable ustawione na true / false i w zależności od tego modyfikuje parametry klasy typu [bool] : Left,Right,Down,Up
        * </span>
        * @param me [<span style="text-decoration: italic; font-weight: bold;">GridTile</span>] - Pobiera informację o obiekcie siatki , od którego ma sprawdzić dostępność
        * kolejnych sąsiadów
        * @param obiektySiatki [<span style="text-decoration: italic; font-weight: bold;">GameObject[]</span>] - Pobiera tablicę obiektów typu gameOject z których skłąda się mapa.
        * 
        * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
        */
        public GetNeaghbours(GridTile me, GameObject[] obiektySiatki)
        {
            this.me = me;
            foreach (GameObject obiekt in obiektySiatki)
            {
                GridTile gt = obiekt.GetComponent<GridTile>();

                if ((gt.x == me.x - 1 || gt.x == me.x + 1) && gt.y == me.y)
                {

                    //lewo 
                    if (gt.x == me.x - 1)
                    {
                        if (gt.movable)
                        {
                            Left = true;

                        }
                        else
                        {
                            objectLeft = gt;
                        }
                    }//prawo
                    else
                    {
                        if (gt.movable)
                        {
                            Right = true;
                        }
                        else
                        {
                            objectRight = gt;
                        }
                    }

                }
                else if ((gt.y == me.y - 1 || gt.y == me.y + 1) && gt.x == me.x)
                {
                    //gora
                    if (gt.y == me.y - 1)
                    {
                        if (gt.movable)
                        {
                            Down = true;
                        }
                        else
                        {
                            objectDown = gt;
                        }
                    }//dol
                    else
                    {
                        if (gt.movable)
                        {
                            Up = true;
                        }
                        else
                        {
                            objectTop = gt;
                        }
                    }
                }
            }
            checkBorder();

        }
        /**
       * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu GridTile - znajdujący się na PRAWO od [GridTile] me
       * <span style="font-size: 16px; text-indent: 50px;">
       *  Zwraca obiekt typu [gridTile] znajdujący się na LEWO od obiektu [gridTile] me, przekazanego do konstruktora.
       * </span>
       * 
       * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca obiekt [GridTile]</span>
       * @result <span style="color: red;"><B>[NIEPOWODZENIE]</B> Zwraca [NULL]</span>
       * 
       * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
       */
        public GridTile getObjectLeft()
        {
            return objectLeft;
        }
        /**
       * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu GridTile - znajdujący się na PRAWO od [GridTile] me
       * <span style="font-size: 16px; text-indent: 50px;">
       *  Zwraca obiekt typu [gridTile] znajdujący się na PRAWO od obiektu [gridTile] me, przekazanego do konstruktora.
       * </span>
       * 
       * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca obiekt [GridTile]</span>
       * @result <span style="color: red;"><B>[NIEPOWODZENIE]</B> Zwraca [NULL]</span>
       * 
       * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
       */
        public GridTile getObjectRight()
        {
            return objectRight;
        }
        /**
     * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu GridTile - znajdujący się POWYŻEJ [GridTile] me
     * <span style="font-size: 16px; text-indent: 50px;">
     *  Zwraca obiekt typu [gridTile] znajdujący się POWYŻEJ obiektu [gridTile] me, przekazanego do konstruktora.
     * </span>
     * 
     * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca obiekt [GridTile]</span>
     * @result <span style="color: red;"><B>[NIEPOWODZENIE]</B> Zwraca [NULL]</span>
     * 
     * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
     */
        public GridTile getObjectTop()
        {

            return objectTop;
        }
        /**
     * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu GridTile - znajdujący się PONIŻEJ [GridTile] me
     * <span style="font-size: 16px; text-indent: 50px;">
     *  Zwraca obiekt typu [gridTile] znajdujący się PONIZEJ obiektu [gridTile] me, przekazanego do konstruktora.
     * </span>
     * 
     * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca obiekt [GridTile]</span>
     * @result <span style="color: red;"><B>[NIEPOWODZENIE]</B> Zwraca [NULL]</span>
     * 
     * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
     */
        public GridTile getObjectDown()
        {
            return objectDown;
        }
        /**
    * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu [bool] - informuje czy na LEWO od obiektu ME znajduje się inny obiekt
    * <span style="font-size: 16px; text-indent: 50px;">
    *  Zwraca obiekt typu [bool] jeżeli na LEWO obiektu [gridTile] me znajduje się inny obiekt, na podstawie [gridTile] me - przekazanego do konstruktora.
    * </span>
    * 
    * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca TRUE [bool] - jeżeli brak obiektu</span>
    * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca FALSE [bool] - jeżeli miejsce jest zajmowane przez inny obiekt</span>
    * 
    * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
    */
        public bool getMovableLeft()
        {

            return Left;
        }
        /**
   * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu [bool] - informuje czy na PRAWO od obiektu ME znajduje się inny obiekt
   * <span style="font-size: 16px; text-indent: 50px;">
   *  Zwraca obiekt typu [bool] jeżeli na PRAWO obiektu [gridTile] me znajduje się inny obiekt, na podstawie [gridTile] me - przekazanego do konstruktora.
   * </span>
   * 
   * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca TRUE [bool] - jeżeli brak obiektu</span>
   * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca FALSE [bool] - jeżeli miejsce jest zajmowane przez inny obiekt</span>
   * 
   * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
   */
        public bool getMovableRight()
        {

            return Right;
        }
        /**
  * @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu [bool] - informuje czy POWYŻEJ obiektu ME znajduje się inny obiekt
  * <span style="font-size: 16px; text-indent: 50px;">
  *  Zwraca obiekt typu [bool] JEŻELI POWYŻEJ obiektu [gridTile] me znajduje się inny obiekt, na podstawie [gridTile] me - przekazanego do konstruktora.
  * </span>
  * 
  * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca TRUE [bool] - jeżeli brak obiektu</span>
  * @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca FALSE [bool] - jeżeli miejsce jest zajmowane przez inny obiekt</span>
  * 
  * @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
  */
        public bool getMovableTop()
        {

            return Up;
        }
        /**
* @brief [<span style="color: lightblue; font-weight: bold;">Publiczna</span>] Metoda zwracająca obiekt typu [bool] - informuje czy PONIŻEJ obiektu ME znajduje się inny obiekt
* <span style="font-size: 16px; text-indent: 50px;">
*  Zwraca obiekt typu [bool] JEŻELI PONIŻEJ obiektu [gridTile] me znajduje się inny obiekt, na podstawie [gridTile] me - przekazanego do konstruktora.
* </span>
* 
* @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca TRUE [bool] - jeżeli brak obiektu</span>
* @result <span style="color: green;"><B>[POWODZENIE]</B>Zwraca FALSE [bool] - jeżeli miejsce jest zajmowane przez inny obiekt</span>
* 
* @author <i><span style="font-size: 1rem; font-weight: bold; color: #fff;">Artur Leszczak</span></i>
*/
        public bool getMovableDown()
        {

            return Down;
        }

        private void checkBorder()
        {

            int maxMapWidth = 15;
            int maxMapHeight = 10;

            if(me == null)
            {
                Debug.Log("Jestem nullem");
            }

            if (this.me.isBorder)
            {
                if(this.me.x == 0)
                {
                    //lewo
                    this.Left = true;
                }
                if (this.me.y == 0)
                {
                    //lewo
                    this.Down = true;
                }
                if(this.me.x == maxMapWidth - 1)
                {
                    this.Right = true;
                }
                if( this.me.y == maxMapHeight - 1)
                {
                    this.Up = true;
                }
            }
        }


    }
}
    