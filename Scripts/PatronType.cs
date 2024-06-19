using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PatronType : MonoBehaviour
{
    //@author Adam Baginski
    //ktory typ patrona jest aktywny
    public static bool PatronDmgActive { get; private set; }
    public static bool PatronSlowActive { get; private set; }
    public static bool PatronGoldActive { get; private set; }


    //metoda aktywujaca patrona od obrazen
    public static void ActivateDmgPatron()
    {
        PatronDmgActive = true;
        PatronSlowActive = false;
        PatronGoldActive = false;
    }

    //metoda dezaktywujaca patrona od obrazen
    public static void DeactivateDmgPatron()
    {
        PatronDmgActive = false;
    }
    //metoda aktywujaca patrona od spowolneinia
    public static void ActivateSlowPatron()
    {
        PatronSlowActive = true;
        PatronDmgActive = false;
        PatronGoldActive = false;
    }

    //metoda dezaktywujaca patrona od spowolnienia
    public static void DeactivateSlowPatron()
    {
        PatronSlowActive = false;
    }
    //metoda aktywujaca patrona od zlota
    public static void ActivateGoldPatron()
    {
        PatronGoldActive = true;
        PatronDmgActive = false;
        PatronSlowActive = false;
    }

    //metoda dezaktywujaca patrona od zlota
    public static void DeactivateGoldPatron()
    {
        PatronGoldActive = false;
    }
}
