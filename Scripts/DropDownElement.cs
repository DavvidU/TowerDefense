using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownElement : MonoBehaviour
{
    public ClickPoint clickPointScript; // Referencja do skryptu ClickPoint
    public TMP_Dropdown dropdown; // Referencja do komponentu TMP_Dropdown

    void Start()
    {
        // Ustawienie pocz�tkowej warto�ci dropdown zgodnie z aktualnym trybem budowania
        dropdown.value = (int)clickPointScript.TrybBudowania;
        // Dodanie nas�uchiwania zmiany warto�ci dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Metoda wywo�ywana, gdy warto�� dropdown ulegnie zmianie
    private void OnDropdownValueChanged(int value)
    {
        // Ustawienie nowego trybu budowania w skrypcie ClickPoint
        clickPointScript.TrybBudowania = (ClickPoint.BuldableObjects)value;
    }
}
