using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * Klasa zawieraj¹ca globalne funkcje, zmienne i sta³e 
 * pozwalaj¹ce na przechowywanie informacji o funduszach gracza.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public static class GlobalFunctions {
    // G³ówna zmienna przechowuj¹ca pieni¹dze gracza
    [SerializeField] private static float Money = 100f;

    /**
     * Metoda zwracaj¹ca iloœæ pieniêdzy gracza.
     *
     * @return Iloœæ pieniêdzy gracza
     */
    public static float getMoney() {
        return Money;
    }

    /**
     * Metoda odejmuj¹ca iloœæ pieniêdzy od gracza.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param amount Iloœæ pieniêdzy, któr¹ chcemy odj¹æ
     * @return Zwraca true, jeœli operacja zakoñczy³a siê pomyœlnie 
     * (gracz posiada wystarczaj¹c¹ iloœæ pieniêdzy), w przeciwnym razie zwraca false
     */
    public static bool removeMoney(float amount) {
        if (Money < amount) {
            return false;
        } else {
            Money -= amount;
            return true;
        }
    }

    /**
     * Metoda dodaj¹ca iloœæ pieniêdzy do gracza.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param amount Iloœæ pieniêdzy, któr¹ chcemy dodaæ
     * @return Zwraca true, jeœli operacja zakoñczy³a siê pomyœlnie (iloœæ pieniêdzy jest nieujemna), w przeciwnym razie zwraca false
     */
    public static bool addMoney(float amount) {
        if (amount >= 0) {
            Money += amount;
            return true;
        } else {
            return false;
        }
    }

    /**
     * Metoda ustawiaj¹ca iloœæ pieniêdzy gracza.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param amount Iloœæ pieniêdzy, któr¹ chcemy ustawiæ
     * @return Zwraca true, jeœli operacja zakoñczy³a siê pomyœlnie (iloœæ pieniêdzy jest nieujemna), w przeciwnym razie zwraca false
     */
    public static bool setMoney(float amount) {
        if (amount >= 0) {
            Money = amount;
            return true;
        } else {
            return false;
        }
    }
}

/**
 * Klasa odpowiedzialna za zarz¹dzanie obiekatmi w œwiecie Unity.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class ZarzadzajObiektami : MonoBehaviour {

    private void Start() {
        // Inicjalizacja
    }

    /**
     * Metoda ustawiaj¹ca widocznoœæ obiektów z okreœlonym tagiem.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param tag Tag obiektów, których widocznoœæ chcemy zmieniæ
     * @param value Wartoœæ logiczna okreœlaj¹ca widocznoœæ (true - widoczne, false - niewidoczne)
     * @return Zwraca true, jeœli operacja zakoñczy³a siê pomyœlnie
     */
    public bool setObjectsVisibilityByTag(string tag, bool value) {
        GameObject[] obiektyDoWylaczenia = GameObject.FindGameObjectsWithTag(tag);

        foreach(GameObject obiekt in obiektyDoWylaczenia) {
            obiekt.GetComponent<Renderer>().enabled = value;
        }

        return true;
    }
}
