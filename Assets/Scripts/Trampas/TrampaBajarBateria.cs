using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaBajarBateria : MonoBehaviour
{
    [SerializeField]private int cantidadABajar=-2;
    [SerializeField]private float tiempoEspera=0.5f;
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
            Debug.Log("descargando");
            yield return new WaitForSeconds(tiempoEspera);
            BajarBateria(-2);
            StartCoroutine(BajarNivelBateria());  
        }
        
    }
}
