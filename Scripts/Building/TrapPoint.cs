using UnityEngine;

/**
 * Klasa reprezentuj�ca punkt pu�apki, niezbedne dla pu�apki strzelaj�cej.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class TrapPoint : MonoBehaviour
{
    /**
     * @brief Zmienna przechowuj�ca informacj� czy mo�na postawi� obiekt na okre�lonym miejscu.
     */
    private bool isFree;

    private GameObject buildedObject;
    private GameObject thisObject;

    // Start is called before the first frame update
    void Start()
    {
        this.isFree = true;
        this.thisObject = gameObject;
    }


    public Vector3 getTransformPosition()
    {
        return transform.position;
    }
    public Transform getTransform()
    {
        return transform;
    }

    public Quaternion getTransformRotation()
    {
        return transform.rotation;
    }
    /**
     * @brief Sprawdza czy mo�na postawi� obiekt
     * 
     * @return True - je�eli mozna postawi� 
     * @return False - je�eli nie mozna postawi�
     */
    public bool getIsFree()
    {
        return isFree;
    }
    public bool setIsFree(bool status)
    {
        this.isFree=status;
        
        return true;
    }
    /**
    * Metoda ustawiaj�ca zbudowany obiekt na danym polu.
    *
    * @param GameObject trap, objekt pu�apki kt�ry ma by� zbudowany.
    * @return Zwraca true, je�li obiekt zosta� pomy�lnie zbudowany, w przeciwnym razie zwraca false.
    * @author Artur Leszczak
    * @version 1.0.0
    */
    public bool setBuildedObject(GameObject trap)
    {
        if (this.buildedObject == null)
        {
            this.buildedObject = trap;
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject getBuildedObject() {
        
        return this.buildedObject;
    }

    public GameObject getTrapPointObject()
    {
        return this.thisObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
