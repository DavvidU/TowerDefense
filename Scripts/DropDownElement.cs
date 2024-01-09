using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownElement : MonoBehaviour
{
    public ClickPoint clickPointScript;
    public TMP_Dropdown dropdown;
    public GameObject trapsDropdownGameObject; // GameObject z rozwijanym menu dla pu�apek

    void Start()
    {
        // ustawienie na bazow� warto�� aktualnego trybu budowania
        dropdown.value = (int)clickPointScript.TrybBudowania;
        // nas�uch warto�ci
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        // wy��czenie domy�lenie listy z pu�apkami
        UpdateTrapsDropdownVisibility(dropdown.value);
    }
    private void OnDropdownValueChanged(int value)
    {
        clickPointScript.TrybBudowania = (ClickPoint.BuldableObjects)value;
        // aktualizacja widoczno�ci
        UpdateTrapsDropdownVisibility(value);
    }
    private void UpdateTrapsDropdownVisibility(int value)
    {
        // je�li value 2 to poka� liste pu�apek (2 to index pu�apek w li�cie budowli)
        trapsDropdownGameObject.SetActive(value == 2);
    }
}