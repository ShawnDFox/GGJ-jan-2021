using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPlayer : MonoBehaviour
{
    public Action JugadorEntro;
    public Action JugadorSalio;
    public Action MientrasJugadorEsta;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            JugadorEntro?.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            MientrasJugadorEsta?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            JugadorSalio?.Invoke();
        }
    }
}
