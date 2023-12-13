using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ZarzadzajObiektami : MonoBehaviour
{

    private void Start()
    {
        
    }

    public bool setObjectsVisibilityByTag(string tag, bool value)
    {
        GameObject[] obiektyDoWylaczenia = GameObject.FindGameObjectsWithTag(tag);

        foreach(GameObject obiekt in obiektyDoWylaczenia)
        {
            obiekt.GetComponent<Renderer>().enabled = value;
        }

        return true;

    }

}
