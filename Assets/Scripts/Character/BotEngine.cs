using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotEngine : MonoBehaviour
{
    
    [SerializeField]
    private int Velocidad_max;
    public int Velocidad;
    
    private BotHealth Health;
    private Animator anims;

    bool Facinright= true;

    private void Awake()
    {
        Health = GetComponent<BotHealth>();
        GetComponent<CharacterInputs>().OnMove += Move;
        anims = GetComponentInChildren<Animator>();
        //GetComponent<CharacterInputs>().OnInteract += Interact;
    }


    private void Move(float Hor,float vert)
    {
        //ultimo vertical
        float LastHor=0;
        float LastVert=0;
        if (Hor != LastHor)
        {
            vert = 0;
            anims.SetBool("Side", true);
            if (Hor > 0)
            {
                if (!Facinright)
                {
                    Facinright = true;
                    flip();
                }
            }
            else
            {
                if (Facinright)
                {
                    Facinright = false;
                    flip();
                }
            }
            LastHor = Hor;
        }

        if (vert != LastVert)
        {
            Hor = 0;
            anims.SetFloat("Vertical", vert);
            anims.SetBool("Side", false);
            LastVert = vert;
        }
        
        Health.Carga -= 0.025f;
        
        Vector3 movement = new Vector3(Hor, vert, 0);
        
        transform.Translate(movement * Time.deltaTime * Velocidad);
        
    }

    public void flip()
    {
        //voltea tanto la imagen como el colisionador en ella

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void ChangeSpeed(int amount){
        Velocidad=amount;
    }
   
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        var charger = collision.GetComponent<Charger>();
        var Discharger = collision.GetComponent<DisCharger>();
        if (charger != null)
        {
            Charge(charger.amount);
        }
        if (Discharger != null)
        {
            DisCharge(charger.amount);
        }
    }*/
}
