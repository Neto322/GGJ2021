using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    CharacterController cc;

    Characte_Input characterinput;

    [SerializeField]
    float topspeed;

    [SerializeField]
    float jumpstrenght;

    [SerializeField]
    float gravityforce;

    Vector3 direction;

    Vector3 velocity;

    [SerializeField]
    LayerMask Ground;




    [SerializeField]
    float Raydistance;

    float _velocityY;


    [SerializeField]
    float rotationspeed;


    Rumbler rumbler;


    [SerializeField]
    Transform cat;


    Vector3 finalvector;


    [SerializeField]
    float turnspeed;

    [SerializeField]
    Transform cam;

    float angle;

    Quaternion targetRotation;

    Animator anim;

    float distance;

    float catfarness;


    [SerializeField]
    float purringOffset;

    float purringstrenght;

    float purr = 0;

    [SerializeField]
    float purrs_per_second;

    float joy;


    [SerializeField]
    float aceleration;


    float speed;


    [SerializeField]
    float acl;

    float momentum;

    [SerializeField]
    Switrcher switrcher;

    bool estado;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        characterinput = new Characte_Input();

        anim = GetComponent<Animator>();

        rumbler = GetComponent<Rumbler>();

        estado = true;
    }

    private void OnEnable()
    {
        characterinput.Enable();
    }

    private void OnDisable()
    {
        characterinput.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x != 0 || direction.y != 0)
        {
                speed += Time.time * aceleration;


        }

        else
        {
            
            speed -= Time.time * aceleration;
        }

        if (speed >= topspeed)
            speed = topspeed;

        if (speed < 0)
            speed = 0;

           

        //Calcular distancia entre personaje y gato

        distance = Vector3.Distance(cat.position, transform.position);

        //Hacer ronronidos su fuerza cambiara dependiendo de la distancia

      
        catfarness = purringOffset / distance;

       if(distance > 1f)
        {
            if (Time.time > purr)
            {

                rumbler.RumblePulse(catfarness);

                purr = Time.time + purrs_per_second;
            }
        }
        else
        {
            rumbler.RumblePulse(catfarness);
        }

        //Movimiento



        direction = characterinput.Land.Move.ReadValue<Vector2>();

      
       if(characterinput.Land.Jump.triggered)
        {
            StartCoroutine("Grab");
        }
        

        velocity = new Vector3(0,1,0);



        if (cc.isGrounded)
        {
            if (characterinput.Land.Jump.triggered)
            {
                _velocityY = jumpstrenght;

            }
        }
        if (cc.isGrounded == false)
        {
            _velocityY -= gravityforce;
        }


        if(estado == true)
        cc.Move(transform.forward  * speed * Time.deltaTime);
        

        velocity.y = _velocityY;


        cc.Move(velocity * Time.deltaTime);

       


        // rotacion

        if ( direction.x != 0 || direction.y != 0)
        {

            angle = Mathf.Atan2(direction.x, direction.y);

            angle = Mathf.Rad2Deg * angle;

            angle += cam.eulerAngles.y;

            targetRotation = Quaternion.Euler(0, angle, 0);

            if (estado == true)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

        }


    }

    private void LateUpdate()
    {
       

        if (direction.x != 0 || direction.y != 0 )
        {
            if(momentum <1)
            momentum += Time.time * acl;
            else
            {

            }

        }

        

        else
        {
            momentum -= Time.time * acl;
        }

        if(momentum < 0)
        {
            momentum = 0;
        }


        anim.SetFloat("Blend", momentum);


    }


    private void OnTriggerEnter(Collider other)
    {
        
        switrcher.CambiarCam(other.name);

        


     
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "a")
        {
            anim.SetTrigger("Dance");
        }

        if (estado == false)
        {
            Cat_Animator cat = other.GetComponent<Cat_Animator>();

            cat.Animar();

          
        }
    }

    IEnumerator Grab()
    {
        anim.SetBool("Grab",true);
        estado = false;
        yield return new WaitForSeconds(3f);

        estado = true;
        anim.SetBool("Grab", false);
        yield return null;
    }
}
