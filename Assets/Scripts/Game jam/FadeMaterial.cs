using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMaterial : MonoBehaviour
{
    public float fadeDuration = 10.0f; // Durée en secondes pour le fondu

    private float elapsedTime = 0.0f;
    private bool isFading = false;
    private Material fadeMaterial;

    private void Start()
    {
        // Créer un nouveau matériau avec un shader adapté au fondu
        fadeMaterial = new Material(Shader.Find("Unlit/Color"));
        fadeMaterial.color = Color.black;
    }

    private void Update()
    {
        if (isFading)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeMaterial.color = new Color(0, 0, 0, alpha);

            if (alpha >= 1.0f)
            {
                isFading = false;
            }
        }
    }

    public void StartFade()
    {
        isFading = true;
        elapsedTime = 0.0f;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Utiliser le matériau de fondu pour rendre l'écran
        Graphics.Blit(src, dest, fadeMaterial);
    }
}
