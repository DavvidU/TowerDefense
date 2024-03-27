using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class GlobalFunctions 
{
    [SerializeField] private static float Money = 100f;
   

    // Start is called before the first frame update
    

    public static float getMoney()
    {
        return Money;
    }
    public static bool removeMoney(float amount)
    {
        if (Money < amount)
        {
            return false;
        }
        else
        {
            Money -= amount;
            return true;
        }

    }
    public static bool addMoney(float amount)
    {
        if (amount >= 0)
        {
            Money += amount;
            return true;
        }
        else
        {
            
            return false;
        }

    }
    public static bool setMoney(float amount)
    {
        if (amount >= 0)
        {
            Money = amount;
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
