using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerResetAlpha : MonoBehaviour
{
    public string treatTag = "Treat";
    public DarkFadeScreen darkFadeScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(treatTag) && darkFadeScreen != null)
        {
            darkFadeScreen.ResetAlpha();
            other.gameObject.SetActive(false);
        }
    }
}

