using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainAttack : MonoBehaviour
{
    private PlayerController parentScript;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = transform.parent.GetComponent<PlayerController>();
    }

    public void AttackEnemy()
    {
        parentScript.LanzarAtaque();
        //Invoke("LaunchAttack", 0);
    }
}
