using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private float enemyDamage;
    [SerializeField] private bool waitForAttack;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float attackInterval;
    [SerializeField] private TMPro.TextMeshProUGUI healthAmount;

    
    // Update is called once per frame
    void Update()
    {
        if (waitForAttack)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= attackInterval)
            {
                waitForAttack = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!waitForAttack)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                other.GetComponent<ObjectHealth>().TakeDamage(enemyDamage);
                healthAmount.text = other.GetComponent<ObjectHealth>().health.ToString() + " / 100";
                waitForAttack = true;
            }
        }
    }
}
