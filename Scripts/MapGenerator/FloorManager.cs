using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Klasa zarz¹dzaj¹ca siatk¹ pod³ogow¹ w grze.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class FloorManager : MonoBehaviour
{
    /**
     * Tag obiektów, których siatka pod³ogowa ma byæ ukryta/pokazana.
     */
    public string targetTag = "DefaultGridFloor";

    /**
     * Ukrywa siatkê pod³ogow¹ dla obiektów o okreœlonym tagu.
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
     * Pokazuje siatkê pod³ogow¹ dla obiektów o okreœlonym tagu.
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
