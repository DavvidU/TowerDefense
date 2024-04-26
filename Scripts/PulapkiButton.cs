using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulapkiButton : MonoBehaviour
{
    public ClickPoint clickPointScript;
    
    // Start is called before the first frame update
    public void wybierz_kolce()
    {
        clickPointScript.WybranaPu�apka=ClickPoint.BuldableTraps.Kolce;
    }
    public void wybierz_lod()
    {
        clickPointScript.WybranaPu�apka=ClickPoint.BuldableTraps.Oblodzenie;
    }
    public void wybierz_lawa()
    {
        clickPointScript.WybranaPu�apka=(ClickPoint.BuldableTraps.Lawa);
    }
    public void wybierz_strzelajaca()
    {
        clickPointScript.WybranaPu�apka=(ClickPoint.BuldableTraps.Strzelajaca);
    }
}
