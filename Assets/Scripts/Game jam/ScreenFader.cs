using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public Material overlayMaterial;
    public float fadeDuration = 10.0f;

    private float elapsedTime = 0.0f;
    private bool isFading = false;

    private void Start()
    {
        StartFade();
    }

    private void Update()
    {
        if (isFading)
        {
            elapsedTime += Time.deltaTime;
            float overlayOpacity = Mathf.Clamp01(elapsedTime / fadeDuration);
            overlayMaterial.SetFloat("_OverlayOpacity", overlayOpacity);

            if (overlayOpacity >= 1.0f)
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
}
