using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

/**
*The `SimplePushing` class is responsible for shooting arrows at a defined speed and limit,
* with functionality to create and shoot arrows in a game environment, you should add this class
* to GameObject .
* @author Artur Leszczak
* @version 1.0.0
*/
public class SimplePushing : MonoBehaviour
{
    [SerializeField]
    GameObject StrzalaPrefab; //prefab of arrow

    //Defines a limit of arrows
    public int ArrowLimit = 50;
    private int ArrowCounter = 0;
    public Transform spawnPoint; //point of arrow spawn 
    public float shootingSpeed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        shootingSpeed = 1f; //one second
        timer = 0f;
    }
    
    /**
    * This method is called once per frame. It checks if it's time to shoot an arrow and does so if conditions are met.
    *
    * @author Artur Leszczak
    * @version 1.0.0
    */
    void Update() {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if the timer has reached or exceeded the shooting speed and if the arrow limit has not been reached
        if(timer >= shootingSpeed && ArrowCounter <= ArrowLimit) {
            // Increment the arrow counter
            ArrowCounter++;

            // Call the shoot method to create and shoot an arrow
            shoot();

            // Reset the timer
            timer = 0f;
        }
    }

    /**
    * This method is responsible for creating and shooting an arrow.
    *
    * @return void
    * @author Artur Leszczak
    * @version 1.0.0
    */
    void shoot() {
        // Instantiate a new arrow GameObject at the spawn point's position and rotation
        GameObject arrow = Instantiate(StrzalaPrefab, spawnPoint.position, spawnPoint.rotation);

        // Get the Strzala component from the instantiated arrow GameObject
        Strzala arrowScript = arrow.GetComponent<Strzala>();

        // Check if the Strzala component exists
        if (arrowScript != null) {
            // Set the arrow's direction opposite to the spawn point's right direction
            arrowScript.UstawKierunek(-spawnPoint.right);

            // Set the arrow's speed to 8 units per second
            arrowScript.setSpeed(8f); 

            // Set the arrow's range to 5 units
            arrowScript.setRange(5f); 
        }
    }


}
