using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraficoVida : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float currentLife;

    void LateUpdate()
    {
        currentLife = player.GetComponent<ObjectHealth>().health;
        if (currentLife > 0 && currentLife <= 100)
        {
            GetComponent<Image>().fillAmount = currentLife / 100;
        }
        else if (currentLife >= 0)
        {
            GetComponent<Image>().fillAmount = 0;
        }
        else
        {
            GetComponent<Image>().fillAmount = 1;
        }
    }
}
