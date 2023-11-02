using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTransition : MonoBehaviour
{
    public float transitionDuration = 10f;

    private Material material;
    private float startTime;
    private float initialAlpha;
    private float targetAlpha;

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

            // D�finissez l'alpha cible � 1.0 (255 en �chelle de 0 � 1).
            targetAlpha = 1.0f;

            // Enregistrez le temps de d�but de la transition.
            startTime = Time.time;

            // D�marrez la coroutine de transition d'alpha.
            StartCoroutine(TransitionAlpha());
        }
        else
        {
            Debug.LogError("Le GameObject doit avoir un Renderer avec un Material pour effectuer la transition d'alpha.");
        }
    }

    private IEnumerator TransitionAlpha()
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            // Calculez l'alpha actuel en fonction du temps �coul� et de la dur�e de la transition.
            float t = elapsedTime / transitionDuration;
            float currentAlpha = Mathf.Lerp(initialAlpha, targetAlpha, t);

            // Mettez � jour l'alpha du mat�riau.
            Color materialColor = material.color;
            materialColor.a = currentAlpha;
            material.color = materialColor;

            // Attendez une frame.
            yield return null;

            // Mettez � jour le temps �coul�.
            elapsedTime = Time.time - startTime;
        }

        // Assurez-vous que l'alpha atteint exactement 1.0 � la fin de la transition.
        Color finalMaterialColor = material.color;
        finalMaterialColor.a = targetAlpha;
        material.color = finalMaterialColor;
    }

}
