using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    public ClickPoint clickPointScript;
    //author Adam Baginski
    // Metoda ustawiajaca tryb budowania
    public void SetBuildMode()
    {
        clickPointScript.inBuildMode = true;
        clickPointScript.inDestroyingMode = false;
    }
    // Metoda ustawiajaca tryb niszczenia
    public void SetDestroyMode()
    {
        clickPointScript.inBuildMode = false;
        clickPointScript.inDestroyingMode = true;
    }
}
