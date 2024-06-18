using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * Klasa zawieraj�ca globalne funkcje, zmienne i sta�e 
 * pozwalaj�ce na przechowywanie informacji o funduszach gracza.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public static class GlobalFunctions {
    // G��wna zmienna przechowuj�ca pieni�dze gracza
    [SerializeField] private static float Money = 100f;

    /**
     * Metoda zwracaj�ca ilo�� pieni�dzy gracza.
     *
     * @return Ilo�� pieni�dzy gracza
     */
    public static float getMoney() {
        return Money;
    }

    /**
     * Metoda odejmuj�ca ilo�� pieni�dzy od gracza.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param amount Ilo�� pieni�dzy, kt�r� chcemy odj��
     * @return Zwraca true, je�li operacja zako�czy�a si� pomy�lnie 
     * (gracz posiada wystarczaj�c� ilo�� pieni�dzy), w przeciwnym razie zwraca false
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
     * Metoda dodaj�ca ilo�� pieni�dzy do gracza.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param amount Ilo�� pieni�dzy, kt�r� chcemy doda�
     * @return Zwraca true, je�li operacja zako�czy�a si� pomy�lnie (ilo�� pieni�dzy jest nieujemna), w przeciwnym razie zwraca false
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
     * Metoda ustawiaj�ca ilo�� pieni�dzy gracza.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param amount Ilo�� pieni�dzy, kt�r� chcemy ustawi�
     * @return Zwraca true, je�li operacja zako�czy�a si� pomy�lnie (ilo�� pieni�dzy jest nieujemna), w przeciwnym razie zwraca false
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
 * Klasa odpowiedzialna za zarz�dzanie obiekatmi w �wiecie Unity.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class ZarzadzajObiektami : MonoBehaviour {

    private void Start() {
        // Inicjalizacja
    }

    /**
     * Metoda ustawiaj�ca widoczno�� obiekt�w z okre�lonym tagiem.
     *
     * @author Artur Leszczak
     * @version 1.0.0
     * @param tag Tag obiekt�w, kt�rych widoczno�� chcemy zmieni�
     * @param value Warto�� logiczna okre�laj�ca widoczno�� (true - widoczne, false - niewidoczne)
     * @return Zwraca true, je�li operacja zako�czy�a si� pomy�lnie
     */
    public bool setObjectsVisibilityByTag(string tag, bool value) {
        GameObject[] obiektyDoWylaczenia = GameObject.FindGameObjectsWithTag(tag);

        foreach(GameObject obiekt in obiektyDoWylaczenia) {
            obiekt.GetComponent<Renderer>().enabled = value;
        }

        return true;
    }
}
