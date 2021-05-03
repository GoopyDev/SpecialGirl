using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] public float damage;
    [SerializeField] private GameObject explosion;
    [SerializeField] public AudioEngine audioEngineScript;
    [SerializeField] public AudioSource audioEngineReference;
    [SerializeField] private AudioClip audioClipBola;

    private void Start()
    {
        audioEngineReference.PlayOneShot(audioEngineScript.ballClip);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Tuve collision trigger");
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            other.gameObject.GetComponent<ObjectHealth>().TakeDamage(damage);
            Invoke("DestruirBola", 0);
            //Destroy(gameObject);
        }
    }

    private void DestruirBola()
    {
        Destroy(gameObject);
    }
}
