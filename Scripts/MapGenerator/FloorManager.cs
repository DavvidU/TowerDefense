using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Klasa zarz�dzaj�ca siatk� pod�ogow� w grze.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class FloorManager : MonoBehaviour
{
    /**
     * Tag obiekt�w, kt�rych siatka pod�ogowa ma by� ukryta/pokazana.
     */
    public string targetTag = "DefaultGridFloor";

    /**
     * Ukrywa siatk� pod�ogow� dla obiekt�w o okre�lonym tagu.
     */
    public void Hide()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject obj in objects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }

    /**
     * Pokazuje siatk� pod�ogow� dla obiekt�w o okre�lonym tagu.
     */
    public void Show()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject obj in objects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
    }
}
