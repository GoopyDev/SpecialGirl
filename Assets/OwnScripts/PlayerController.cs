using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float  attackRange      = default; //Distancia a la cual podemos comenzar a atacar enemigos
    [SerializeField] private float  attackInterval   = default; //Tiempo entre cada ataque
    [SerializeField] private float  attackTime       = default; //Contador para intervalos entre cada ataque
    //[SerializeField] private bool   attacking        = default; //Para conocer si estamos atacando
    //[SerializeField] private string currentAttack    = default; //El ataque actual, pasado a String
    [SerializeField] private GameObject currentEnemy = default; //A quién estamos atacando actualmente

    [Header("References")]
    [SerializeField] private NavMeshAgent agent;                //Referencia a nuestro jugador
    [SerializeField] private GameObject ballIce;                //Bola de hielo
    [SerializeField] private GameObject ballIceSelector;        //Para mostrar qué bola actualmente se utiliza
    [SerializeField] private GameObject ballFire;               //Bola de fuego
    [SerializeField] private GameObject ballFireSelector;       //Para mostrar qué bola actualmente se utiliza
    [SerializeField] private GameObject ballThunder;            //Bola de electricidad
    [SerializeField] private GameObject ballThunderSelector;    //Para mostrar qué bola actualmente se utiliza
    [SerializeField] private GameObject currentBall;            //La bola actual, en base al ataque seleccionado actualmente (ice, fire, thunder)
    [SerializeField] private GameObject cursor;                 //Cursor para mostrar cuando hacemos click en el suelo
    [SerializeField] private GameObject instanciaCursor;        //Instancia del cursor
    [SerializeField] private AudioEngine audioEngineScript;     //Nuestro Script AudioEngine
    [SerializeField] private AudioSource audioReference;        //Referencia al AudioSource de AudioEngine
    
    private Animator m_Animator = null;


    // Variables privadas //
    private enum AttackType { ice, fire, thunder };
    [SerializeField] private AttackType attack = AttackType.ice;
    //private AttackType lastAttack = AttackType.ice;



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Contador para los ataques. Es un cooldown timer.
        if (attackTime < 3)
        {
            attackTime += Time.deltaTime;       //Contamos el tiempo para los intervalos entre cada ataque
        }

        #region Seleccion de MagicBall
        //Seleccion de MagicBall con los nros 1, 2 y 3

        if      (Input.GetKeyDown(KeyCode.Alpha1)) { SetMagicBall(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { SetMagicBall(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { SetMagicBall(3); }
        #endregion

        //currentAttack = attack.ToString();
        if (Input.GetMouseButtonDown(1))
        {
            StopAttack(); //Al tocar el boton 1 del mouse, detenemos cualquier ataque en curso
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.gameObject.layer);
                //Al hacer click, borramos el cursor que hubiera en el mapa
                if (instanciaCursor != null) { Destroy(instanciaCursor); }
                
                //Si hacemos clic en el suelo, simplemente nos dirigimos a ese punto
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
                {
                    agent.stoppingDistance = 0.1f;
                    agent.SetDestination(hit.point);

                    instanciaCursor = Instantiate(cursor, new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z), transform.rotation);
                }
                //Si hacemos click en un enemigo, nos preparamos para atacar, y establecemos un rango mayor para detenernos antes de llegar
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    //attacking = true;
                    if      (attack.ToString() == "ice")
                    {
                        attackRange = 5f;
                        currentBall = ballIce;
                    }
                    else if (attack.ToString() == "fire")
                    {
                        attackRange = 6f;
                        currentBall = ballFire;
                    }
                    else if (attack.ToString() == "thunder")
                    {
                        attackRange = 7f;
                        currentBall = ballThunder;
                    }
                    currentEnemy = hit.collider.gameObject; // Guardamos referencia de nuestro enemigo actual
                    agent.stoppingDistance = attackRange;
                    agent.SetDestination(currentEnemy.transform.position);
                }
            }
        }


        if (currentEnemy != null)
        {
            Debug.Log("Distancia al enemigo: " + Vector3.Distance(transform.position, currentEnemy.transform.position));
        }
        if (currentEnemy != null && (Vector3.Distance(transform.position, currentEnemy.transform.position) <= attackRange ))
        {
            CorregirRotacion();                 //Para que el personaje mire directamente a un enemigo

            if (attackTime >= attackInterval)   //Si el contador supera nuestro tiempo de intervalo establecido, atacamos.
            {
                attackTime = 0;                 //Restablecemos el contador para los intervalos.
                m_Animator.SetBool("isAttacking", true);
            }
        }

        //Para animar al personaje mientras se mueve
        if (agent.velocity.magnitude > new Vector3(0.1f, 0.1f, 0.1f).magnitude)
        {
            m_Animator.SetBool("isMove", true);
        }
        else
        {
            m_Animator.SetBool("isMove", false);
        }
    }

    private void CorregirRotacion()
    {
        //Debug.Log("Distancia al corregir la rotacion: " + agent.remainingDistance);

        //Esta rotación se ejecutará si ya estamos dentro del rango de ataque al enemigo, para apuntarle correctamente.
        if (currentEnemy != null)
        {
            Vector3 lookAt = currentEnemy.transform.position - transform.position;
            gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(lookAt.x, 0, lookAt.z), Vector3.up);
        }
    }
    public void LanzarAtaque()
    {
        GameObject newBall = Instantiate(currentBall);
        newBall.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        newBall.transform.rotation = CorregirRotacionBola(newBall.transform.position);

        newBall.GetComponent<MoveBall>().audioEngineScript = audioEngineScript;
        newBall.GetComponent<MoveBall>().audioEngineReference = audioReference;
        //newBall.GetComponent<MoveBall>().damage = attackRange;
    }

    private Quaternion CorregirRotacionBola(Vector3 ballPos)
    {
        Quaternion direccion = transform.rotation;
        if (currentEnemy != null)
        {
            //Corrige la altura del lanzamiento de la bola de energía
            Vector3 lookAt = currentEnemy.transform.GetChild(0).transform.position - ballPos; //Esto obtiene el "AIM POINT" de nuestro enemigo
            direccion = Quaternion.LookRotation(lookAt, Vector3.up); //El AIMPOINT debe estar primero en la lista de objetos del enemigo
        }
        return direccion;
    }

    public void StopAttack()
    {
        m_Animator.SetBool("isAttacking", false);
        currentEnemy = null;
    }

    #region SetMagicBall
    public void SetMagicBall(int ballNumber)
    {
        audioReference.PlayOneShot(audioEngineScript.selectBallClip);
        if (ballNumber == 1) 
        {
            currentBall = ballIce;
            attack = AttackType.ice;
            ballIceSelector.SetActive(true);
            ballFireSelector.SetActive(false);
            ballThunderSelector.SetActive(false);
        }

        else if (ballNumber == 2)
        { 
            currentBall = ballFire;
            attack = AttackType.fire;
            ballIceSelector.SetActive(false);
            ballFireSelector.SetActive(true);
            ballThunderSelector.SetActive(false);
        }

        else if (ballNumber == 3)
        {
            currentBall = ballThunder;
            attack = AttackType.thunder;
            ballIceSelector.SetActive(false);
            ballFireSelector.SetActive(false);
            ballThunderSelector.SetActive(true);
        }
    }
    #endregion
}
