using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudowanieButton : MonoBehaviour
{
    public ClickPoint clickPointScript;
    public List<GameObject> pulapki;

    public void wybierz_pulapki()
    {
        foreach (var button in pulapki)
        {
            button.SetActive(true);
        }
        clickPointScript.TrybBudowania = ClickPoint.BuldableObjects.Pu³apki;
    }
    public void wybierz_sciezke()
    {
        foreach (var button in pulapki)
        {
            button.SetActive(false);
        }
        clickPointScript.TrybBudowania = ClickPoint.BuldableObjects.Sciezka;
    }
    public void wybierz_sciane()
    {
        foreach (var button in pulapki)
        {
            button.SetActive(false);
        }
        clickPointScript.TrybBudowania = (ClickPoint.BuldableObjects.Sciany);
    }

}
