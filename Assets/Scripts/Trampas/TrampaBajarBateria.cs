using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaBajarBateria : MonoBehaviour
{
    private event Action<int> BajarBateria;
    private bool descargarBateria;
    private void Start() {
        BajarBateria=FindObjectOfType<BotHealth>().Charge;
        DetectorPlayer detectorPlayer=GetComponent<DetectorPlayer>();
        detectorPlayer.JugadorEntro= ()=> {descargarBateria=true;   StartCoroutine(BajarNivelBateria());};
        detectorPlayer.JugadorSalio= ()=> {descargarBateria=false; };
    }
    private IEnumerator BajarNivelBateria(){
        
        if(descargarBateria){
            yield return new WaitForSeconds(0.5f);
            BajarBateria(-2);
            StartCoroutine(BajarNivelBateria());  
        }
        
    }
}
