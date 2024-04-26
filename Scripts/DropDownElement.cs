using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownElement : MonoBehaviour
{
    public ClickPoint clickPointScript;
    public TMP_Dropdown dropdown;
    public GameObject trapsDropdownGameObject; // GameObject z rozwijanym menu dla pu³apek

    void Start()
    {
        // ustawienie na bazow¹ wartoœæ aktualnego trybu budowania
        dropdown.value = (int)clickPointScript.TrybBudowania;
        // nas³uch wartoœci
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        // wy³¹czenie domyœlenie listy z pu³apkami
        UpdateTrapsDropdownVisibility(dropdown.value);
    }
    private void OnDropdownValueChanged(int value)
    {
        clickPointScript.TrybBudowania = (ClickPoint.BuldableObjects)value;
        // aktualizacja widocznoœci
        UpdateTrapsDropdownVisibility(value);
    }
    private void UpdateTrapsDropdownVisibility(int value)
    {
        // jeœli value 2 to poka¿ liste pu³apek (2 to index pu³apek w liœcie budowli)
        trapsDropdownGameObject.SetActive(value == 2);
    }
}