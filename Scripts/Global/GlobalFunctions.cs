using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    [SerializeField] private float Money = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Money = 100.0f;
    }

    public float getMoney()
    {
        return this.Money;
    }
    public bool removeMoney(float amount)
    {
        if (this.Money < amount)
        {
            return false;
        }
        else
        {
            this.Money -= amount;
            return true;
        }

    }
    public bool addMoney(float amount)
    {
        if (amount >= 0)
        {
            this.Money += amount;
            return true;
        }
        else
        {
            
            return false;
        }

    }
    public bool setMoney(float amount)
    {
        if (amount >= 0)
        {
            this.Money = amount;
            return true;
        }
        else
        {
            return false;
        }

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
