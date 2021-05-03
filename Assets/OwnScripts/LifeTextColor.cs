using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTextColor : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textMesh;
    [SerializeField] private ObjectHealth player;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Convert.ToInt32(player.GetComponent<ObjectHealth>().health) == 100)
        {
            textMesh.color = new Color(0, 255, 0, 1);
        }
        else if (System.Convert.ToInt32(player.GetComponent<ObjectHealth>().health) < 100 && System.Convert.ToInt32(player.health) > 40)
        {
            textMesh.color = new Color(255, 255, 255, 1);
        }
        else
        {
            textMesh.color = new Color(255, 0, 0, 1);
        }
    }
}
