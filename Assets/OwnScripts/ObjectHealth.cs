using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private Color damageTextColor = default;
    //[SerializeField] private string damageText;
    [SerializeField] private Vector3 damageTextPosition;
    [SerializeField] private GameObject damageTextObj = default;
    [SerializeField] private GameObject player = default; // Referencia a nuestro jugador
    [SerializeField] private GameObject diePanel; //El panel que mostrará que hemos muerto, si los enemigos nos matan

    public void TakeDamage(float damage)
    {
        health -= damage;
        // Show damage text over character
        Debug.Log("Posicion: " + transform.position.x + damageTextPosition.x + " " + transform.position.y + damageTextPosition.y + 1 + " " + transform.position.z + damageTextPosition.z);
        //damageTextPosition = new Vector3(transform.localPosition.x + damageTextPosition.x, transform.localPosition.y + damageTextPosition.y + 1, transform.localPosition.z + damageTextPosition.z); //Posición del texto
        damageTextPosition = new Vector3(transform.position.x, transform.position.y +1, transform.position.z); //Posición del texto
        damageTextObj.GetComponent<TMPro.TextMeshPro>().text = damage.ToString(); //
        Instantiate(damageTextObj, damageTextPosition, damageTextObj.transform.rotation);

        if (health <= 0)
        {
            // Play death animation

            //Detener ataque del jugador (Si no es que se trata del jugador mismo. En tal caso dejamos "player" en valor null)
            if (player != null)
            {
                player.GetComponent<PlayerController>().StopAttack();
            }
            else
            {
                diePanel.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
