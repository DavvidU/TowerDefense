using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    AudioSource aud;
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        aud = GetComponent<AudioSource>();
        aud.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save(){
        PlayerPrefs.SetFloat("musicvolume", volumeSlider.value);
    }
}
