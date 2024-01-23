using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource aud;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
