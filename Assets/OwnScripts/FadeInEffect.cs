using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInEffect : MonoBehaviour
{
    private float alpha = 1;


    // Update is called once per frame
    void Update()
    {
        alpha -= Time.deltaTime;
        GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        if (alpha <= 0) { Destroy(gameObject); }
    }
}
