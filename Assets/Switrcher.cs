using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Switrcher : MonoBehaviour
{

    [SerializeField]
    CinemachineVirtualCamera[] cams;
    
    [SerializeField]
    CinemachineVirtualCamera[] catcams;

    int i = 0;
    public void CambiarCam(string id)
    {

        float ID = float.Parse(id);

        for(int i = 0; i < cams.Length; i++)
        {
            if(i == ID)
            {
                cams[i].Priority = 1;
            }
            else
            {
                cams[i].Priority = 0;
            }

        }
    }



    public void DesenfocarGato()
    {
        

            catcams[i].Priority = 0;




        
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);

        DesenfocarGato();

        yield return null;
    }
}
