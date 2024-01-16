using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class SimplePushing : MonoBehaviour
{

    public GameObject strza³a;
    public int ArrowLimit = 10;
    private int ArrowCounter = 0;
    public Transform spawnPoint;
    public float shootingSpeed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {

        string katalogZasobowStrzelajacych = "Assets/Prefabs/Traps/Arrows";
        string[] sciezkiDoZasobowStrzelajacych = AssetDatabase.FindAssets("", new[] { katalogZasobowStrzelajacych });

        foreach (string sciezka in sciezkiDoZasobowStrzelajacych)
        {
            //narazie tylko jedna pulapka w Traps/Spikes
            string pelnaSciezka = AssetDatabase.GUIDToAssetPath(sciezka);

            GameObject zasob = AssetDatabase.LoadAssetAtPath<GameObject>(pelnaSciezka);

            if (zasob != null && PrefabUtility.IsPartOfPrefabAsset(zasob))
            {
                if (zasob.name == "Strza³a")
                {
                    this.strza³a= zasob;
                }
            }
        }


        shootingSpeed = 2f; //jedna sekunda
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
        GameObject arrow = Instantiate(strza³a, spawnPoint.position, spawnPoint.rotation);

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
