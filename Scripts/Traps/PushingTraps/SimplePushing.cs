using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class SimplePushing : MonoBehaviour
{
    [SerializeField]
    GameObject StrzalaPrefab;

    
    public int ArrowLimit = 50;
    private int ArrowCounter = 0;
    public Transform spawnPoint;
    public float shootingSpeed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {

        

        shootingSpeed = 1f; //jedna sekunda
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= shootingSpeed && ArrowCounter<=ArrowLimit)
        {
            ArrowCounter++;
            shoot();
            timer = 0f;
        }
       
    }
    void shoot()
    {
        GameObject arrow = Instantiate(StrzalaPrefab, spawnPoint.position, spawnPoint.rotation);

        // Ustawienie kierunku strza³y zgodnie z kierunkiem pu³apki
        Strzala arrowScript = arrow.GetComponent<Strzala>();
        if (arrowScript != null)
        {
            arrowScript.UstawKierunek(-spawnPoint.right);
            arrowScript.setSpeed(8f); 
            arrowScript.setRange(5f); 
        }
    }


}
