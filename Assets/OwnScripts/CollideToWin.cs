using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideToWin : MonoBehaviour
{
    [SerializeField] private GameObject winMessage; //Referencia a panel WinPanel del HUD

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        winMessage.SetActive(true);
    }
}
