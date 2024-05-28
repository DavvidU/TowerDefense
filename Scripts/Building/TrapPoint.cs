using UnityEngine;


public class TrapPoint : MonoBehaviour
{
    private bool isFree;
    private GameObject buildedObject;
    //private Transform transform;
    private GameObject thisObject;
    // Start is called before the first frame update
    void Start()
    {
        this.isFree = true;
       // this.transform = GetComponent<Transform>();
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
    public bool setBuildedObject(GameObject trap)
    {
        if(this.buildedObject == null)
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
