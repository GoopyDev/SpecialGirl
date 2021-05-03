using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        Quaternion rot = Quaternion.Euler(10, 0, 0);
        transform.Rotate(new Vector3(10, 0, 0), 1f);
    }
}
