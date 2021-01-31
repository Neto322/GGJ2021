using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Switrcher : MonoBehaviour
{

    [SerializeField]
    CinemachineVirtualCamera[] cams;
    


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
}
