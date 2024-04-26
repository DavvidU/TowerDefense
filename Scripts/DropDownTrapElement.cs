using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class DropDownTrapElement : MonoBehaviour
{
    public ClickPoint clickPointScript; // Referencja do skryptu ClickPoint
    public TMP_Dropdown dropdown; // Referencja do komponentu TMP_Dropdown

    void Start()
    {
        // Ustawienie początkowej wartości dropdown zgodnie z aktualnym trybem budowania
        dropdown.value = (int)clickPointScript.WybranaPułapka;
        // Dodanie nasłuchiwania zmiany wartości dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Metoda wywoływana, gdy wartość dropdown ulegnie zmianie
    private void OnDropdownValueChanged(int value)
    {
        // Ustawienie nowego trybu budowania w skrypcie ClickPoint
        clickPointScript.WybranaPułapka = (ClickPoint.BuldableTraps)value;
    }
}
