using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    public ClickPoint clickPointScript;

    //author Adam Bagi�ski
    // Metoda ustawiaj�ca tryb budowania
    public void SetBuildMode()
    {
        clickPointScript.inBuildMode = true;
        clickPointScript.inDestroyingMode = false;
    }
    // Metoda ustawiaj�ca tryb niszczenia
    public void SetDestroyMode()
    {
        clickPointScript.inBuildMode = false;
        clickPointScript.inDestroyingMode = true;
    }
}
