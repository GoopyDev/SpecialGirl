using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private Color damageTextColor = default;
    [SerializeField] private Vector3 damageTextPosition;
    [SerializeField] private GameObject damageTextObj = default;
    [SerializeField] private GameObject player = default; // Referencia a nuestro jugador
    [SerializeField] private GameObject diePanel; //El panel que mostrará que hemos muerto, si los enemigos nos matan

    public void TakeDamage(float damage)
    {
        health -= damage;
        // Show damage text over character
        damageTextPosition = new Vector3(transform.position.x, transform.position.y +1, transform.position.z); //Posición del texto
        damageTextObj.GetComponent<TMPro.TextMeshPro>().text = damage.ToString(); //Indicamos qué texto colocar (el número de fuerza de ataque)
        damageTextObj.GetComponent<TMPro.TextMeshPro>().color = damageTextColor; //Tomamos el color que posee establecido el componente ObjectHealth (este mismo script)
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
