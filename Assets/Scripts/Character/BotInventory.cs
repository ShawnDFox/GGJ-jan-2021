using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotInventory : MonoBehaviour
{
    
    [SerializeField]
    private int Max_Carry = 15;
    public List<CantidadObjetoARecoger> cantidadObjetosARecoger;
    

    public event Action<String,int,int> OnItemPickup;
    public event Action ItemsEncontrados;
    private int contadorTareasHechas;

    //referencia a  las partes las cuales debemos tener en el bot
    private void Start()
    {
        ItemsEncontrados+=()=>{FindObjectOfType<QuestController>().deliveringPoint.enabled=true;};
        SetQuest();
    }

    public void SetQuest(int Torn=0, int cabl=0, int chip=0, int resis=0)
    {
        //Asigna los numeros
        int index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Tornillo);
        if(index>=0){
            cantidadObjetosARecoger[index].cantidadActual=Torn;
        } 
        index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Cable);
        if(index>=0){
            cantidadObjetosARecoger[index].cantidadActual=cabl;
        }
        index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Chip);
        if(index>=0){
           cantidadObjetosARecoger[index].cantidadActual=chip; 
        }
        index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Resistencia);
        if(index>=0){
            cantidadObjetosARecoger[index].cantidadActual=resis;
        }

        //Asigna los textos
        foreach (CantidadObjetoARecoger item in cantidadObjetosARecoger)
        {
            OnItemPickup(GetNameTypeOfPickUpObject(item.tipoDeObjetoARecoger), 0,item.cantidadARecoger);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int cantidadActual=0;
        switch (collision.tag)
        {
            case "Tornillo":
                int indexTornillo=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Tornillo);
                cantidadActual=cantidadObjetosARecoger[indexTornillo].cantidadActual++;
                OnItemPickup(collision.tag, cantidadObjetosARecoger[indexTornillo].cantidadActual++,cantidadObjetosARecoger[indexTornillo].cantidadARecoger);
                ConfirmarSiTerminoLaTarea(indexTornillo);
                collision.gameObject.SetActive(false);
                break;
            case "Cable":
                int indexCable=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Cable);
                cantidadActual=cantidadObjetosARecoger[indexCable].cantidadActual++;
                OnItemPickup(collision.tag, cantidadObjetosARecoger[indexCable].cantidadActual++,cantidadObjetosARecoger[indexCable].cantidadARecoger);
                ConfirmarSiTerminoLaTarea(indexCable);
                collision.gameObject.SetActive(false);
                break;
            case "Chip":
                int indexChip=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Chip);
                cantidadActual=cantidadObjetosARecoger[indexChip].cantidadActual++;
                OnItemPickup(collision.tag, cantidadObjetosARecoger[indexChip].cantidadActual++,cantidadObjetosARecoger[indexChip].cantidadARecoger);
                ConfirmarSiTerminoLaTarea(indexChip);
                collision.gameObject.SetActive(false);
                break;
            case "Resistencia":
                int indexResistencia=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Resistencia);
                cantidadActual=cantidadObjetosARecoger[indexResistencia].cantidadActual++;
                OnItemPickup(collision.tag, cantidadObjetosARecoger[indexResistencia].cantidadActual++,cantidadObjetosARecoger[indexResistencia].cantidadARecoger);
                ConfirmarSiTerminoLaTarea(indexResistencia);
                collision.gameObject.SetActive(false);
                break;
        }
        
    }
    private void ConfirmarSiTerminoLaTarea(int index){
        if(cantidadObjetosARecoger[index].cantidadActual>=cantidadObjetosARecoger[index].cantidadARecoger){
            contadorTareasHechas++;
            if(contadorTareasHechas>=3) {
                contadorTareasHechas=0;
                ItemsEncontrados();
            }
        }
    }
    private int GetPositionTypeOfAmountObject(TipoDeObjetoARecoger tipoDeObjetoARecoger){
        int position=-1;
        for (int i = 0; i < cantidadObjetosARecoger.Count; i++)
        {
            if(cantidadObjetosARecoger[i].tipoDeObjetoARecoger==tipoDeObjetoARecoger){
                position=i;
                break;
            }
        }
        return position;
    }
    private string GetNameTypeOfPickUpObject(TipoDeObjetoARecoger tipoDeObjetoARecoger){
        string name=null;
        switch (tipoDeObjetoARecoger)
        {
            case TipoDeObjetoARecoger.Tornillo:
                name="Tornillo";
            break;
            case TipoDeObjetoARecoger.Cable:
                name="Cable";
            break;
            case TipoDeObjetoARecoger.Chip:
               name="Chip";
            break;
            case TipoDeObjetoARecoger.Resistencia:
                name="Resistencia";
                Debug.Log("Resistencia");
            break;
        }
        return name;
    }
    public int GetPositionTypeOfPickUpObject(String nameTag){
        int index=-1;
        switch (nameTag)
        {
            case "Tornillo":
                index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Tornillo);
                break;
            case "Cable":
                index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Cable);
                break;
            case "Chip":
                index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Chip);
                break;
            case "Resistencia":
                index=GetPositionTypeOfAmountObject(TipoDeObjetoARecoger.Resistencia);
                break;
        }
        return index;
    }
    [System.Serializable]
    public class CantidadObjetoARecoger{
        public TipoDeObjetoARecoger tipoDeObjetoARecoger;
        public int cantidadActual;
        public int cantidadARecoger;
    }
    public enum TipoDeObjetoARecoger
    {
        Tornillo, Chip, Cable, Resistencia
    }

}
