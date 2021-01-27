using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterVars vars;
    private void Awake()
    {
        vars = GetComponent<CharacterVars>();
        GetComponent<CharacterInputs>().OnMove += Move;
        //GetComponent<CharacterInputs>().OnInteract += Interact;
    }

    private void Move(float Hor,float vert)
    {
        //
        Debug.Log("Moving");
        if (Hor != 0)
        {
            vert = 0;
        }

        Vector3 movement = new Vector3(Hor, vert, 0);
        transform.Translate(movement * Time.deltaTime * vars.Velocidad);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
