using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFadeScreen : MonoBehaviour
{
    public float transitionDuration = 10f;

    private Material material;
    private float initialAlpha;
    private float targetAlpha = 1.0f;
    private float elapsedTime = 0f;

    private void Start()
    {
        // Assurez-vous que le GameObject poss�de un Renderer avec un Material qui a une propri�t� "_Color" g�rant l'alpha.
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;

            // R�cup�rez l'alpha initial du mat�riau.
            Color materialColor = material.color;
            initialAlpha = materialColor.a;

            // D�finissez l'alpha initial � 0.
            Color initialMaterialColor = material.color;
            initialMaterialColor.a = 0f;
            material.color = initialMaterialColor;
        }
        else
        {
            Debug.LogError("Le GameObject doit avoir un Renderer avec un Material pour effectuer la transition d'alpha.");
        }
    }

    private void Update()
    {
        // Incr�mentez l'alpha en fonction du temps.
        if (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            float currentAlpha = Mathf.Lerp(initialAlpha, targetAlpha, t);

            // Mettez � jour l'alpha du mat�riau.
            Color materialColor = material.color;
            materialColor.a = currentAlpha;
            material.color = materialColor;
        }
        else
        {
            // Assurez-vous que l'alpha atteint exactement 1.0 � la fin de la transition.
            Color finalMaterialColor = material.color;
            finalMaterialColor.a = targetAlpha;
            material.color = finalMaterialColor;
        }
    }

    public void ResetAlpha()
    {
        // R�initialisez l'alpha � 0.
        Color materialColor = material.color;
        materialColor.a = 0f;
        material.color = materialColor;

        // R�initialisez le temps �coul�.
        elapsedTime = 0f;
    }
}
