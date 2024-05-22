using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransparency : MonoBehaviour
{
    [Range(0, 1)]
    public float alpha = 0.5f; // Ustawienie przezroczysto�ci (0 = ca�kowicie niewidoczny, 1 = ca�kowicie widoczny)

    void Start()
    {
        SetObjectTransparency(alpha);
    }

    void SetObjectTransparency(float alpha)
    {
        // Pobierz wszystkie renderery w prefabrykacie
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            // Przejd� przez wszystkie materia�y przypisane do renderera
            foreach (Material mat in renderer.materials)
            {
                // Sprawd�, czy materia� obs�uguje przezroczysto��
                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;

                    // Ustawienia trybu renderowania na transparentne
                    mat.SetFloat("_Mode", 3); // 3 oznacza tryb Transparent
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;
                }
            }
        }
    }
}

