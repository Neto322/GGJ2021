using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Animator : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    Animator catanim;

    float time = 3;
    [SerializeField]
    Switrcher switche;
    enum Estados
    {
        Dormido,
        Corriendo
    }
    Estados estado;

    void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetFloat("Blend",1f);

        estado = Estados.Dormido;
    }

    public void Animar()
    {
       
        if(estado == Estados.Dormido)
        {
            estado = Estados.Corriendo;
            anim.SetFloat("Blend", 0.5f);
            catanim.SetTrigger("Move");
        }

        StartCoroutine("Move");

    }

    private void Update()
    {
        if(estado == Estados.Dormido)
        {
            anim.SetFloat("Blend", 1f);
        }

        if (estado == Estados.Corriendo)
        {
        }

       
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(6f);

        estado = Estados.Dormido;

        switche.DesenfocarGato();

        yield return null;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            Destroy(other.gameObject);
        }
    }

}
