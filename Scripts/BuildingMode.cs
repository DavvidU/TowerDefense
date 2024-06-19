using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Adam Baginski
 */
public class BuildingControl : MonoBehaviour
{
    public ClickPoint clickPointScript;
    /**
     * Metoda ustawiajaca tryb budowania
     * @author Adam Baginski
     */
    public void SetBuildMode()
    {
        clickPointScript.inBuildMode = true;
        clickPointScript.inDestroyingMode = false;
    }
    /**
     * Metoda ustawiajaca tryb niszczenia
     * @author Adam Baginski
     */
    public void SetDestroyMode()
    {
        clickPointScript.inBuildMode = false;
        clickPointScript.inDestroyingMode = true;
    }
}
