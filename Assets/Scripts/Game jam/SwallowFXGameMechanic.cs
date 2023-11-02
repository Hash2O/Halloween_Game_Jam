using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowFXGameMechanic : MonoBehaviour
{
    public float transitionDuration = 10f;
    public string treatTag = "Treat";

    private Material material;
    private float initialAlpha;
    private float targetAlpha;
    private float startTime;
    private Coroutine transitionCoroutine;

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
            transitionCoroutine = StartCoroutine(TransitionAlpha());

            // Abonnez-vous � l'�v�nement OnTriggerEnter pour d�tecter les GameObjects avec le tag "Treat".
            // R�initialisez la coroutine si un GameObject "Treat" entre dans le trigger.
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }
        }
        else
        {
            Debug.LogError("Le GameObject doit avoir un Renderer avec un Material pour effectuer la transition d'alpha.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(treatTag))
        {
            Debug.Log("Swallowing Treat");

            // Si un GameObject "Treat" entre dans le trigger, r�initialisez la coroutine.
            if (transitionCoroutine != null)
            {
                Debug.Log("Coroutine en cours... Stop coroutine");
                StopCoroutine(transitionCoroutine);
            }

            // D�marrez une nouvelle coroutine pour r�initialiser l'alpha � z�ro.
            transitionCoroutine = StartCoroutine(ResetAlpha());
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

    private IEnumerator ResetAlpha()
    {
        // R�initialisez la coroutine pour ramener l'alpha � z�ro.
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            float currentAlpha = Mathf.Lerp(initialAlpha, 0f, t);

            Color materialColor = material.color;
            materialColor.a = currentAlpha;
            material.color = materialColor;

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // Assurez-vous que l'alpha atteint exactement z�ro � la fin de la transition.
        Color finalMaterialColor = material.color;
        finalMaterialColor.a = 0f;
        material.color = finalMaterialColor;
    }
}
