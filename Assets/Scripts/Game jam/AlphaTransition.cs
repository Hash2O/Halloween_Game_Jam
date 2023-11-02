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
        // Assurez-vous que le GameObject possède un Renderer avec un Material qui a une propriété "_Color" gérant l'alpha.
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;

            // Récupérez l'alpha initial du matériau.
            Color materialColor = material.color;
            initialAlpha = materialColor.a;

            // Définissez l'alpha cible à 1.0 (255 en échelle de 0 à 1).
            targetAlpha = 1.0f;

            // Enregistrez le temps de début de la transition.
            startTime = Time.time;

            // Démarrez la coroutine de transition d'alpha.
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
            // Calculez l'alpha actuel en fonction du temps écoulé et de la durée de la transition.
            float t = elapsedTime / transitionDuration;
            float currentAlpha = Mathf.Lerp(initialAlpha, targetAlpha, t);

            // Mettez à jour l'alpha du matériau.
            Color materialColor = material.color;
            materialColor.a = currentAlpha;
            material.color = materialColor;

            // Attendez une frame.
            yield return null;

            // Mettez à jour le temps écoulé.
            elapsedTime = Time.time - startTime;
        }

        // Assurez-vous que l'alpha atteint exactement 1.0 à la fin de la transition.
        Color finalMaterialColor = material.color;
        finalMaterialColor.a = targetAlpha;
        material.color = finalMaterialColor;
    }

}
