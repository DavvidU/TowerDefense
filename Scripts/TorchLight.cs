using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Klasa reprezentuj¹ca oœwietlenie w pochodniach.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class TorchLight : MonoBehaviour
{
    // Zmienna do przechowywania komponentu Light
    private Light pointLight;

    // Zakresy losowania intensywnoœci i zasiêgu
    public float minIntensity = 0.5f;
    public float maxIntensity = 2.0f;
    public float minRange = 5.0f;
    public float maxRange = 20.0f;
    public float changeInterval = 1.0f;
    public float transitionDuration = 1.0f;

    void Start()
    {
        // Pobieranie komponentu Light z obiektu
        pointLight = GetComponent<Light>();

        if (pointLight == null)
        {
            Debug.LogError("Brak komponentu Light na obiekcie");
            return;
        }

        // Uruchomienie korutyn odpowiedzialnych za losowe zmiany
        StartCoroutine(ChangeLightIntensity());
        StartCoroutine(ChangeLightRange());
    }

    // Korutyna do p³ynnej zmiany intensywnoœci œwiat³a
    IEnumerator ChangeLightIntensity()
    {
        while (true)
        {
            float targetIntensity = Random.Range(minIntensity, maxIntensity);
            float initialIntensity = pointLight.intensity;
            float elapsedTime = 0;

            while (elapsedTime < transitionDuration)
            {
                pointLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            pointLight.intensity = targetIntensity;
            yield return new WaitForSeconds(changeInterval);
        }
    }

    // P³ynna zmiana zasiêgu œwiat³a
    IEnumerator ChangeLightRange()
    {
        while (true)
        {
            float targetRange = Random.Range(minRange, maxRange);
            float initialRange = pointLight.range;
            float elapsedTime = 0;

            while (elapsedTime < transitionDuration)
            {
                pointLight.range = Mathf.Lerp(initialRange, targetRange, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            pointLight.range = targetRange;
            yield return new WaitForSeconds(changeInterval);
        }
    }
}
