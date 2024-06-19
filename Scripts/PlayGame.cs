using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    /**
     * Start is called before the first frame updat
     * @author Marcin Żamojda
     */
    public void Play_Game(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //zmiana sceny z menu na gre
    }
    public void QuitGame(){
        Application.Quit(); //wyjście z gry
    }
}
