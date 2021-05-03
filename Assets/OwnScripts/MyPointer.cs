using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPointer : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }
}
