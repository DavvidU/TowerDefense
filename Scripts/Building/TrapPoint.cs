using UnityEngine;

/**
 * Klasa reprezentuj¹ca punkt pu³apki, niezbedne dla pu³apki strzelaj¹cej.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class TrapPoint : MonoBehaviour
{
    /**
     * @brief Zmienna przechowuj¹ca informacjê czy mo¿na postawiæ obiekt na okreœlonym miejscu.
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
     * @brief Sprawdza czy mo¿na postawiæ obiekt
     * 
     * @return True - je¿eli mozna postawiæ 
     * @return False - je¿eli nie mozna postawiæ
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
    * Metoda ustawiaj¹ca zbudowany obiekt na danym polu.
    *
    * @param GameObject trap, objekt pu³apki który ma byæ zbudowany.
    * @return Zwraca true, jeœli obiekt zosta³ pomyœlnie zbudowany, w przeciwnym razie zwraca false.
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
