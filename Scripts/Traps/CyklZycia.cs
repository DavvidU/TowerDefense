using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyklZycia : MonoBehaviour
{
    int licznik = 0;
    public int iloscUzyc;
    void OnTriggerEnter(Collider other)
    {
        licznik++;
        if (licznik == iloscUzyc)
        {
            Destroy(gameObject);
        }
    }
}
