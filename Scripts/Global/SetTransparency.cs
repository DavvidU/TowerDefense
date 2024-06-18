using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Klasa s³u¿¹ca do ustawiania przezroczystoœci dla obiektów.
 *
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class SetTransparency : MonoBehaviour
{
    [Range(0, 1)]
    public float alpha = 0.5f; // Ustawienie przezroczystoœci (0 = niewidoczny, 1 = widoczny w 100%)

    void Start()
    {
        SetObjectTransparency(alpha);
    }

    void SetObjectTransparency(float alpha)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                // SprawdŸ, czy materia³ obs³uguje przezroczystoœæ
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

