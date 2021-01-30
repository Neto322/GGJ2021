using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    CharacterController cc;

    Characte_Input characterinput;

    [SerializeField]
    float speed;

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



 

  


    Vector3 finalvector;


    [SerializeField]
    float turnspeed;

    [SerializeField]
    Transform cam;

    float angle;

    Quaternion targetRotation;





    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        characterinput = new Characte_Input();



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


        //Movimiento

        direction = characterinput.Land.Move.ReadValue<Vector2>();



        velocity = new Vector3(0,1,0);



        if (cc.isGrounded)
        {
            if (characterinput.Land.Jump.triggered)
                _velocityY = jumpstrenght;
        }
        if (cc.isGrounded == false)
        {
            _velocityY -= gravityforce;
        }



        velocity.y = _velocityY;

        cc.Move(transform.forward * speed * Time.deltaTime);
        cc.Move(velocity * Time.deltaTime);


        // rotacion

      if( direction.x != 0 || direction.y != 0)
        {
            angle = Mathf.Atan2(direction.x, direction.y);

            angle = Mathf.Rad2Deg * angle;

            angle = Mathf.Round(angle);

            targetRotation = Quaternion.Euler(0, angle, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

        }


    }



}
