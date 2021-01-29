using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaBajarBateria : MonoBehaviour
{
    [SerializeField]private int cantidadABajar=-2;
    [SerializeField]private float tiempoEspera=0.5f;
    private event Action<float> BajarBateria;
    private void Start() {
        BajarBateria=FindObjectOfType<BotHealth>().Charge;
        DetectorPlayer detectorPlayer=GetComponent<DetectorPlayer>();
        detectorPlayer.MientrasJugadorEsta=()=> {BajarBateria(0.1f);};
    }
}
