using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PatronType : MonoBehaviour
{
    public static bool PatronDmgActive { get; private set; }
    public static bool PatronSlowActive { get; private set; }
    public static bool PatronGoldActive { get; private set; }

    public static void ActivateDmgPatron()
    {
        PatronDmgActive = true;
        PatronSlowActive = false;
        PatronGoldActive = false;

        Debug.Log("Wybor patrona: Wojownik");
    }

    public static void DeactivateDmgPatron()
    {
        PatronDmgActive = false;
    }
    public static void ActivateSlowPatron()
    {
        PatronSlowActive = true;
        PatronDmgActive = false;
        PatronGoldActive = false;

        Debug.Log("Wybor patrona: Balwan");
    }

    public static void DeactivateSlowPatron()
    {
        PatronSlowActive = false;
    }
    public static void ActivateGoldPatron()
    {
        PatronGoldActive = true;
        PatronDmgActive = false;
        PatronSlowActive = false;

        Debug.Log("Wybor patrona: Zlodziej");
    }

    public static void DeactivateGoldPatron()
    {
        PatronGoldActive = false;
    }
}
