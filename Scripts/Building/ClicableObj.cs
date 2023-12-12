using Unity.VisualScripting;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    
    void OnMouseDown()
    {
        Transform objectData = GetComponent<Transform>();

        if (objectData != null)
        {
         
            Debug.Log("X: " + objectData.position.x + ", Y: " + objectData.position.z);
        }
        else
        {
            Debug.LogWarning("Brak komponentu Transform na tym obiekcie.");
        }
    }
}
