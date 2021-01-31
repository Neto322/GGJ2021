using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "assol")
        Destroy(gameObject);    
    
    }
}
