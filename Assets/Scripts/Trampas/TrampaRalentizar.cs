using System;
using UnityEngine;

public class TrampaRalentizar : MonoBehaviour
{
    private int originalSpeed;
    private Action<int> CambiarVelocidad;
    // Start is called before the first frame update
    void Start()
    {
        BotEngine botEngine= FindObjectOfType<BotEngine>();
        CambiarVelocidad=botEngine.ChangeSpeed;
        originalSpeed=botEngine.Velocidad;

        DetectorPlayer detectorPlayer=GetComponent<DetectorPlayer>();
        detectorPlayer.JugadorEntro= ()=> {CambiarVelocidad(originalSpeed/2);};
        detectorPlayer.JugadorSalio= ()=> {CambiarVelocidad(originalSpeed);};
    }

}
