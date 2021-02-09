using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class TrampaAtrapar : MonoBehaviour
{
    [Header("Sonido")]
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private Light2D lightTrap;
    [Header("")]
    [SerializeField]private int maximoOprimirVecesLiberarse;
    [SerializeField]private int tiempo;
    [SerializeField]private int cantidadVidaAQuitar;

    private BotEngine CambiarVelocidad;
    private BotHealth QuitarVida;
    private GameObject interfazBotones;

    private Action<float> EfectoDañoPocoAPoco;
    private CharacterInputs characterInputs;
    private bool jugadorEntro;
    private bool cambiarEntreBotones;
    private bool noEsPrimeraVez;
    private int counterTotalVecesOprimidas=0;
    private int originalSpeed;

    public void ReproducirSonido(bool activate){
        lightTrap.enabled=activate;
        if(activate && audioSource!=null){
            audioSource.Play();
        }
        else{
            audioSource.Pause();
        }
    }
    void Start()
    {
        EfectoDañoPocoAPoco = FindObjectOfType<EfectosVisualesVolume>().EfectoViñeta;
        GetComponent<DetectorPlayer>().JugadorEntro += trapped;
        GetComponent<DetectorPlayer>().JugadorSalio += Released;
        interfazBotones=transform.GetChild(0).gameObject;
    }

    private void Released(GameObject obj)
    {
        jugadorEntro = false;
        //SacarJugador();
    }

    private void trapped(GameObject obj)
    {
        if(!jugadorEntro) {
            jugadorEntro = true;
            ReproducirSonido(true);
            //obtiene los compoenentes del bot que vienen desde el delegado de detector players
            if(CambiarVelocidad==null){
                CambiarVelocidad = obj.GetComponent<BotEngine>();
                QuitarVida = obj.GetComponent<BotHealth>();
                characterInputs = obj.GetComponent<CharacterInputs>();
            }
            originalSpeed = CambiarVelocidad.Velocidad;//obtiene la velocidad el bot
            CambiarVelocidad.ChangeSpeed(originalSpeed / 2);//cambia la velocidad del bot
            Invoke("JugadorDentro", 0.5f);
        }
    }

    private void JugadorDentro(){
        
        EfectoDañoPocoAPoco(tiempo);
        interfazBotones.SetActive(true);
        characterInputs.Liberarse += AtrapandoJugador;
        characterInputs.trapped=true;
        CambiarVelocidad.ChangeSpeed(0);//acceso al metodo que tiene botEngine

        StartCoroutine(SacarAlJugadorTrampa());
    }
    IEnumerator SacarAlJugadorTrampa(){
        yield return new WaitForSeconds(tiempo);
        interfazBotones.SetActive(false);
        QuitarVida.TakeDamage(cantidadVidaAQuitar);
        SacarJugador();
    }
    private void AtrapandoJugador(String direction){
        if (jugadorEntro && Time.timeScale > 0)
        {
            //Detecta si es la primera vez
            if (!noEsPrimeraVez)
            {
                //Se hace esto para que de esta manera, permita que el usuario empiece desde cualquier de los dos botones
                if (direction.ToUpper() == "LEFT")
                {
                    cambiarEntreBotones = true;
                }
                else if (direction.ToUpper() == "RIGHT")
                {
                    cambiarEntreBotones = false;
                }
                noEsPrimeraVez=true;
            }

            if (direction.ToUpper() == "LEFT" && cambiarEntreBotones)
            {
                cambiarEntreBotones = false;
                counterTotalVecesOprimidas++;
            }
            else if (direction.ToUpper() == "RIGHT" && !cambiarEntreBotones)
            {
                cambiarEntreBotones = true;
                counterTotalVecesOprimidas++;
            }

            //Liberar
            if (counterTotalVecesOprimidas >= maximoOprimirVecesLiberarse)
            {
                StopCoroutine(SacarAlJugadorTrampa());
                //Retorna todo a la normalidad
                EfectoDañoPocoAPoco(0);
                interfazBotones.SetActive(false);
                Invoke("SacarJugador", 0.5f);
            }
        }
    }
    private void SacarJugador()
    {
        noEsPrimeraVez=false;
        counterTotalVecesOprimidas=0;
        characterInputs.trapped=false;
        CambiarVelocidad.ChangeSpeed(originalSpeed);//acceso al metodo que tiene botEngine reestablecer velocidad
        characterInputs.Liberarse -= AtrapandoJugador;
        gameObject.SetActive(false);
    }
}
