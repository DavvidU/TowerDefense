using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource aud; //stworzenie zmiennej audio
    void Start()
    {
        aud = GetComponent<AudioSource>(); //przypisanie dxieku do zmiennej
        aud.Play(); //odworzenie zapętlonego dźwięku

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
