using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RumblePattern
{
    Constant,
    Pulse,
    Linear
}

public class Rumbler : MonoBehaviour
{
    Characte_Input characterinput;

    private RumblePattern activeRumbePattern;

    float rumbleDurration;
    
    float pulseDurration;
    
   
    float lowA;



    float highA;

 
    [SerializeField]
    float rumbleStep;

    [SerializeField]
    float durration;

    [SerializeField]
    bool isMotorActive = false;

    // Public Methods
  

    public void RumblePulse(float fuerza)
    {
        activeRumbePattern = RumblePattern.Pulse;
        lowA = fuerza;
        highA = fuerza;
        pulseDurration = Time.time + rumbleStep;
        rumbleDurration = Time.time + durration;
        isMotorActive = true;
        var g = GetGamepad();
        g?.SetMotorSpeeds(lowA, highA);
    }

   

    public void StopRumble()
    {
        var gamepad = GetGamepad();
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }


    // Unity MonoBehaviors



    private void Update()
    {

        if (Time.time > rumbleDurration)
        {
            StopRumble();
            return;
        }

        var gamepad = GetGamepad();
        if (gamepad == null)
            return;

   

            switch (activeRumbePattern)
        {
           

            case RumblePattern.Pulse:

                if(Time.time > pulseDurration)
                {
                    isMotorActive = !isMotorActive;
                    pulseDurration = Time.time + rumbleStep;
                    if (!isMotorActive)
                    {
                        gamepad.SetMotorSpeeds(0, 0);
                    }
                    else
                    {
                        gamepad.SetMotorSpeeds(lowA, highA);
                    }
                }

                break;
          
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        StopRumble();
    }

    // Private helpers

    private Gamepad GetGamepad()
    {
        return Gamepad.current;

        
    }
}