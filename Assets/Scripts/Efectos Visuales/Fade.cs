using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        FindObjectOfType<EscenaManager>().FadeAnimation += StartAnimation;
    }
    public void StartAnimation(){
        anim.SetBool("Oscurecer",true);
    }

}
