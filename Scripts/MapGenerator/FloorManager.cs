using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorManager : MonoBehaviour
{
    private string gridTag = "GridDefaultMesh";
    public void hideGrid()
    {
        ChangeGridMeshVisibility(false);
    }

    public void showGrid()
    {
        ChangeGridMeshVisibility(true);
    }
       
    //This function show or hide the mesh render of all gameobjects with tag (gridTag)
    private void ChangeGridMeshVisibility(bool visibility)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(gridTag);

        foreach (GameObject obj in objectsWithTag)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = visibility;
            }
        }
    }
}
