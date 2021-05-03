using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLife : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private float contador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (contador > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
