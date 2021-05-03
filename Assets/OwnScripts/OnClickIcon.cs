using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClickIcon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject player;
    [SerializeField] private int iconNumber;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Boton del mouse: " + eventData.button);
        if (eventData.button.ToString() == "Left")
        {
            player.GetComponent<PlayerController>().SetMagicBall(iconNumber);
        }
    }
}
