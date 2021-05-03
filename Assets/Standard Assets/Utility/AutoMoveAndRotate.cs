using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class AutoMoveAndRotate : MonoBehaviour
    {
        public Vector3andSpace moveUnitsPerSecond;
        public Vector3andSpace rotateDegreesPerSecond;
        public bool ignoreTimescale;
        private float m_LastRealTime;

        [SerializeField] private float lifetime;
        [SerializeField] private float lifetimecounter = 0;
        [SerializeField] private float slowdown = default;


        private void Start()
        {
            m_LastRealTime = Time.realtimeSinceStartup;
        }


        // Update is called once per frame
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            if (ignoreTimescale)
            {
                deltaTime = (Time.realtimeSinceStartup - m_LastRealTime);
                m_LastRealTime = Time.realtimeSinceStartup;
            }
            transform.Translate(moveUnitsPerSecond.value*deltaTime, moveUnitsPerSecond.space);
            transform.Rotate(rotateDegreesPerSecond.value*deltaTime, moveUnitsPerSecond.space);

            //Ralentizar la velocidad de ascenso del texto
            moveUnitsPerSecond.value -= new Vector3(0, slowdown*Time.deltaTime, 0);

            //Timer para determinar el tiempo de vida de un objeto. Si lifetime es -1, el objeto no se destruye porque el contador no avanza
            if (lifetime != -1)
            {
                lifetimecounter += Time.deltaTime;
                
                //Si el contador llega a ser igual a lifetime, destruimos el objeto
                if (lifetimecounter >= lifetime)
                {
                    Destroy(gameObject);
                }
            }
            
            
        }


        [Serializable]
        public class Vector3andSpace
        {
            public Vector3 value;
            public Space space = Space.Self;
        }
    }
}
