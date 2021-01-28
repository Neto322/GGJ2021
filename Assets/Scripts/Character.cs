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


    Color raycolor;

    [SerializeField]
    float Raydistance;

    float _velocityY;


    [SerializeField]
    float rotationspeed;

    Rigidbody rb;

    float angle;

    Vector3 lookDirection;

    Quaternion lookrotation;

    float step,horizontal,vertical;

    Vector3 finalvector;

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

        direction = characterinput.Land.Move.ReadValue<Vector2>();

        finalvector.x = direction.x;

        finalvector.z = direction.y;


        velocity = finalvector * speed;

     
        
        if(cc.isGrounded)
        {
            if (characterinput.Land.Jump.triggered)
               _velocityY = jumpstrenght;
        }
        if(cc.isGrounded == false)
        {
            _velocityY -= gravityforce;
        }
            
        

        velocity.y = _velocityY;

        cc.Move(velocity * Time.deltaTime);


     
    }

   


}
