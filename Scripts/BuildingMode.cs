using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    public ClickPoint clickPointScript;

    public void SetBuildMode()
    {
        clickPointScript.inBuildMode = true;
        clickPointScript.inDestroyingMode = false;
    }
//siema
    public void SetDestroyMode()
    {
        clickPointScript.inBuildMode = false;
        clickPointScript.inDestroyingMode = true;
    }
}
