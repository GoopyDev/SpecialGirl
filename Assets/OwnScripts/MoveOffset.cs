using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float speed;
    private Renderer renderizador;

    // Start is called before the first frame update
    void Start()
    {
        renderizador = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += speed * Time.deltaTime;
        renderizador.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        if (offset >= 10) { offset = 0; } //Reiniciamos el offset para no tener valores altos a futuro
    }
}
