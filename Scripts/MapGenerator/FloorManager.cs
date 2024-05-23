using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public string targetTag = "DefaultGridFloor";

    //Ukrywa siatk�
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

    //pokazuje siatk�
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
