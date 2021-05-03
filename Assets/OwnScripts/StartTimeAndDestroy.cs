using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimeAndDestroy : MonoBehaviour
{
    [SerializeField] private float startTime;
    [SerializeField] private float destroyTime;
    [SerializeField] private float elapsedTime;
    [SerializeField] private GameObject childToAwake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= startTime && !childToAwake.activeInHierarchy)
        {
            childToAwake.SetActive(true);
        }

        if (elapsedTime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
