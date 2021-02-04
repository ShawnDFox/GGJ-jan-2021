using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPlayer : MonoBehaviour
{
    public Action<GameObject> JugadorEntro;
    public Action<GameObject> JugadorSalio;
    public Action<GameObject> MientrasJugadorEsta;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collider Dentro");
            JugadorEntro?.Invoke(other.gameObject);
            
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            MientrasJugadorEsta?.Invoke(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            Debug.Log("Collider Afuera");
            JugadorSalio?.Invoke(other.gameObject);
        }
    }
}
