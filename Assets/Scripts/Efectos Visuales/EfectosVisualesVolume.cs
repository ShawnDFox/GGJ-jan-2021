using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class EfectosVisualesVolume : MonoBehaviour
{
    [SerializeField]private Color32 colorEfectoViñeta;
    private float durationVignette;
    private float currentValueVignette;
    private float speed;
    private bool isRepeating;
    //Guardar valores originales
    private ColorParameter originalColorVignette;
    private float originalIntesity;
    private Vignette vignette;
    // Start is called before the first frame update
    void Start()
    {
        VolumeProfile volumeProfile=GetComponent<Volume>().profile;
        if(!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
    }
    public void EfectoViñeta(float duracionTotal=0){
        if(duracionTotal==0){
            Debug.Log("Parar");
            VolverValorOriginal();
            StopAllCoroutines();
        }
        else{
            currentValueVignette=0;
            durationVignette=duracionTotal;

            originalColorVignette=vignette.color;
            originalIntesity=vignette.intensity.value;

            vignette.color.Override(colorEfectoViñeta);

            StartCoroutine(ConfigurarViñeta());            
        }

    }
    IEnumerator ConfigurarViñeta(){
        float tiempoDividido=0;
        tiempoDividido=durationVignette/3;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(tiempoDividido);
            Debug.Log("Ejecutandose");
            //Pause
            if(Time.timeScale==0){
                currentValueVignette=vignette.intensity.value;
                VolverValorOriginal();
                isRepeating=false;
                i--;
            }
            //Normal Game
            else if(Time.timeScale==1){
                //El juego fue despausado
                if(!isRepeating){ isRepeating=true; vignette.intensity.Override(currentValueVignette); }
                speed+=0.3f;
                vignette.intensity.Override(speed);
            }
        }
        //
        VolverValorOriginal();
    }
    private void VolverValorOriginal(){
        vignette.color.Override(originalColorVignette.value);
        vignette.intensity.Override(originalIntesity);
    }
}
