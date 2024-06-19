using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    public ClickPoint clickPointScript;

    //author Adam Bagiñski
    // Metoda ustawiaj¹ca tryb budowania
    public void SetBuildMode()
    {
        clickPointScript.inBuildMode = true;
        clickPointScript.inDestroyingMode = false;
    }
    // Metoda ustawiaj¹ca tryb niszczenia
    public void SetDestroyMode()
    {
        clickPointScript.inBuildMode = false;
        clickPointScript.inDestroyingMode = true;
    }
}
