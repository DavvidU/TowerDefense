using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulapkiButton : MonoBehaviour
{
    public ClickPoint clickPointScript;
    
    // Start is called before the first frame update
    public void wybierz_kolce()
    {
        clickPointScript.WybranaPu³apka=ClickPoint.BuldableTraps.Kolce;
    }
    public void wybierz_lod()
    {
        clickPointScript.WybranaPu³apka=ClickPoint.BuldableTraps.Oblodzenie;
    }
    public void wybierz_lawa()
    {
        clickPointScript.WybranaPu³apka=(ClickPoint.BuldableTraps.Lawa);
    }
    public void wybierz_strzelajaca()
    {
        clickPointScript.WybranaPu³apka=(ClickPoint.BuldableTraps.Strzelajaca);
    }
}
