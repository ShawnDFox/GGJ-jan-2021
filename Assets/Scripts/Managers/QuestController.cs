using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour
{
    [HideInInspector]public bool tareaCompleta;
    public Collider2D deliveringPoint;
    public List<TipoDeObjetoASpawnear> objetosASpawnear=new List<TipoDeObjetoASpawnear>();

    //public List<Transform> PlayerSpawn;
   [SerializeField]
    private List<PointsOfSpawn> puntosDeSpawn=new List<PointsOfSpawn>(1);

    private int carry_capacity = 15;
    [Header("Capacidad De Spawn")]
    [SerializeField] private int capacidadPorCadaTrampa=30;
    [SerializeField] private int capacidadCargadores;


    private List<GameObject> objetosRecolectables=new List<GameObject>();
    private List<GameObject> trampas=new List<GameObject>();
    private List<GameObject> objetosAyuda=new List<GameObject>();
    private GameManager Manager;
    private BotInventory botInventory;

    private enum Dificultad{
        Facil,Medio,Dificil
    }
    private Dificultad nivelDeDificultad;

    public event Action<String, int, int> PickUp;
    public event Action<GameObject> TerminarNivel;

    // Start is called before the first frame update
    void Awake()
    {   
        Manager = GameManager.Instance;
        puntosDeSpawn=new List<PointsOfSpawn>();
        objetosRecolectables=new List<GameObject>();
    }

    private void Start()
    {   
        
        //Obtiene y asigna el evento de productos entregados
        deliveringPoint.enabled=false;
        TerminarNivel+=(GameObject obj)=>{tareaCompleta=true; Debug.Log("Termino");};
        deliveringPoint.GetComponent<DetectorPlayer>().JugadorEntro=TerminarNivel;

        //Busca los puntos de spawn
        puntosDeSpawn.Add(BuscarYObtenerPuntosDeSpawn("AnclaObjetoRecogible",TypeOfPoints.Recogible));
        puntosDeSpawn.Add(BuscarYObtenerPuntosDeSpawn("AnclaTrampa",TypeOfPoints.Trampa));
        puntosDeSpawn.Add(BuscarYObtenerPuntosDeSpawn("AnclaCargador",TypeOfPoints.PuntoDeAyuda));


        //Genera los pool
        //Recogibles
        
        int tempPosicion=GetPositionTypeOfObjectToSpawn(TypeOfPoints.Recogible);
        for (int i = 0; i < objetosASpawnear[tempPosicion].objetosASpawnear.Count; i++)
        {
            GameObject tempObject=objetosASpawnear[tempPosicion].objetosASpawnear[i];
            ObjectPooler.Instance.NewPool(carry_capacity,tempObject.tag,tempObject);
        }
        //Trampas
        tempPosicion=GetPositionTypeOfObjectToSpawn(TypeOfPoints.Trampa);
        for (int i = 0; i < objetosASpawnear[tempPosicion].objetosASpawnear.Count; i++)
        {
            GameObject tempObject=objetosASpawnear[tempPosicion].objetosASpawnear[i];
            ObjectPooler.Instance.NewPool(capacidadPorCadaTrampa,tempObject.tag,tempObject);
        }
        //Recargas
        tempPosicion=GetPositionTypeOfObjectToSpawn(TypeOfPoints.PuntoDeAyuda);
        for (int i = 0; i < objetosASpawnear[tempPosicion].objetosASpawnear.Count; i++)
        {
            GameObject tempObject=objetosASpawnear[tempPosicion].objetosASpawnear[i];
            ObjectPooler.Instance.NewPool(capacidadCargadores,tempObject.tag,tempObject);
        }

    }
   
#region GenerarQuest
    //logica para idear cuantos elementos debemos pedirle al jugador que recoja dependiendo del nivel actual
    public void GenerateQuest(int level)
    {
        botInventory = FindObjectOfType<BotInventory>();
        deliveringPoint.enabled = false;
        tareaCompleta =false;
        //Desactiva y ubica en la posicion zero los objetos
        if(objetosRecolectables.Count>=1)DesactivarObjetos(objetosRecolectables);
        if(trampas.Count>=1) DesactivarObjetos(trampas);
        if(objetosAyuda.Count>=1) DesactivarObjetos(objetosAyuda);

        if(level<=3){
            ElegirNivelDeDificultad(Dificultad.Facil);
        } 
        else if(level<=6){
            ElegirNivelDeDificultad(Dificultad.Medio);
        } 
        else if(level<=9){
            ElegirNivelDeDificultad(Dificultad.Dificil);
        }
        botInventory.SetQuest();
    }
    private void ElegirNivelDeDificultad(Dificultad dificultad){
        switch (dificultad)
        {
            case Dificultad.Facil:
                GeneradorDificultad(1,3,5,10,3,5);
            break;
            case Dificultad.Medio:
                GeneradorDificultad(3,6,15,20,2,3);
            break;
            case Dificultad.Dificil:
                GeneradorDificultad(6,8,20,25,3,4);
            break;
        }
    }
    private void GeneradorDificultad(int minimoObjetosAGenerar, int maximoObjetosAGenerar, int minimoTrampas, int maximoTrampas, int minimoAyudas, int maximoAyudas)
    {
        ResetearListas();
        //Genera los objetos recogibles
        int positionTypeOfObject = GetPositionTypeOfObjectToSpawn(TypeOfPoints.Recogible);
        for (int i = 0; i < objetosASpawnear[positionTypeOfObject].objetosASpawnear.Count; i++)
        {
            int randomNumber=UnityEngine.Random.Range(minimoObjetosAGenerar,maximoObjetosAGenerar);
            GenerarObjetosAleatorios(objetosRecolectables, TypeOfPoints.Recogible, objetosASpawnear[positionTypeOfObject].objetosASpawnear[i].tag, randomNumber);
        }
        
        //Genera las trampas
        positionTypeOfObject = GetPositionTypeOfObjectToSpawn(TypeOfPoints.Trampa);
        for (int i = 0; i < objetosASpawnear[positionTypeOfObject].objetosASpawnear.Count; i++)
        {
            int randomNumber=UnityEngine.Random.Range(minimoTrampas,maximoTrampas);
            GenerarObjetosAleatorios(trampas, TypeOfPoints.Trampa, objetosASpawnear[positionTypeOfObject].objetosASpawnear[i].tag, randomNumber);
        }
        //Genera los Cargadores
        positionTypeOfObject = GetPositionTypeOfObjectToSpawn(TypeOfPoints.PuntoDeAyuda);
        for (int i = 0; i < objetosASpawnear[positionTypeOfObject].objetosASpawnear.Count; i++)
        {
            int randomNumber = UnityEngine.Random.Range(minimoAyudas, maximoAyudas);
            GenerarObjetosAleatorios(objetosAyuda, TypeOfPoints.PuntoDeAyuda, objetosASpawnear[positionTypeOfObject].objetosASpawnear[i].tag, randomNumber);
        }

    }
    private List<GameObject> GenerarObjetosAleatorios(List<GameObject> listObjeto,TypeOfPoints tipoDeObjeto, string tagObjeto, int cantidadASpawnear){

        //Asigna la cantidad a recoger en el texto
        if(tipoDeObjeto==TypeOfPoints.Recogible){
            int posicion=botInventory.GetPositionTypeOfPickUpObject(tagObjeto);
            botInventory.cantidadObjetosARecoger[posicion].cantidadARecoger=cantidadASpawnear;
            botInventory.cantidadObjetosARecoger[posicion].cantidadActual=0;
        }
        //Debug.Log($"//======================= Type {tagObjeto}, Size {cantidadASpawnear}, objType {tipoDeObjeto} =============================//");
        for (int i = 0; i < cantidadASpawnear; i++)
        {
            int posicionTipoDeObjeto = GetPositionTypeOfPoint(tipoDeObjeto);
            PuntosDeAncla tempPointOfSpawn = puntosDeSpawn[posicionTipoDeObjeto].puntos[UnityEngine.Random.Range(0,puntosDeSpawn[posicionTipoDeObjeto].puntos.Count-1)];
            Vector3 tempPosition = tempPointOfSpawn.point.position;
            if (!tempPointOfSpawn.isFull)
            {
                tempPointOfSpawn.isFull = true;
                listObjeto.Add(ObjectPooler.Instance.SpawnFromPool(tagObjeto, tempPosition));
            }
            else
            {
                i--;
            }
        }
        return listObjeto;
    }
    private void ResetearListas(){
        objetosRecolectables = new List<GameObject>();
        objetosAyuda=new List<GameObject>();
        trampas=new List<GameObject>();
        //Habilita todos los puntos
        for (int i = 0; i < puntosDeSpawn.Count; i++)
        {
            for (int j = 0; j < puntosDeSpawn[i].puntos.Count; j++)
            {
                if(puntosDeSpawn[i].puntos[j].isFull) puntosDeSpawn[i].puntos[j].isFull=false;
            }
        }
    }
    private void DesactivarObjetos(List<GameObject> listaDeObjetos){
        foreach (GameObject item in listaDeObjetos)
        {
            if(item.activeInHierarchy) item.SetActive(false);
            item.transform.position=Vector3.zero;
        }
    }
#endregion
    /*public  void ObjetoRecogido(string tagName, int amount){
        int posicion=GetPositionTypeOfPickUpObject(tagName);
        cantidadObjetosARecoger[posicion].cantidadActual+=amount;
        //cantidadObjetosARecoger[posicion].textoCantidad.text=""+cantidadObjetosARecoger[posicion].cantidadActual+"/"+carry_capacity;
        PickUp(tagName,cantidadObjetosARecoger[posicion].cantidadActual,carry_capacity);
        if(cantidadObjetosARecoger[posicion].cantidadActual>=cantidadObjetosARecoger[posicion].cantidadARecoger){
            contadorTareasHechas++;
            if(contadorTareasHechas>=3) {
                
            }
        }
    }*/
    //Busca y asigna los puntos de spawn
    private PointsOfSpawn BuscarYObtenerPuntosDeSpawn(string tag,TypeOfPoints tipoDePunto){
        GameObject[] tempObjects=GameObject.FindGameObjectsWithTag(tag);
        PointsOfSpawn tempPointsOfSpawn=new PointsOfSpawn();
        tempPointsOfSpawn.tipoDePunto=tipoDePunto;
        for (int i = 0; i < tempObjects.Length; i++)
        {
            PuntosDeAncla tempPuntosDeAncla=new PuntosDeAncla();
            tempPuntosDeAncla.point=tempObjects[i].transform;
            tempPointsOfSpawn.puntos.Add(tempPuntosDeAncla);
        }
        return tempPointsOfSpawn;
    }

#region Obtener Posicion de un item en una lista como puede ser Tipo de Objeto u tipo de cantidad de objeto
    private int GetPositionTypeOfObjectToSpawn(TypeOfPoints tipoDePuntoABuscar){
        int position=-1;
        for (int i = 0; i < objetosASpawnear.Count; i++)
        {
            if(objetosASpawnear[i].tipoDeObjeto==tipoDePuntoABuscar){
                position=i;
                break;
            }
        }
        return position;
    }
    private int GetPositionTypeOfPoint(TypeOfPoints tipoDePuntoABuscar){
        int position=-1;
        for (int i = 0; i < puntosDeSpawn.Count; i++)
        {
            if(puntosDeSpawn[i].tipoDePunto==tipoDePuntoABuscar){
                position=i;
                break;
            }
        }
        return position;
    }
#endregion

#region Clases
    [System.Serializable]
    public class PointOfObjectsTag{
        public TypeOfPoints tipoDePunto;
        public string tag;
    }
    [System.Serializable]
    public class PointsOfSpawn
    {
        public TypeOfPoints tipoDePunto;
        public List<PuntosDeAncla> puntos=new List<PuntosDeAncla>();
    }
    [System.Serializable]
    public class PuntosDeAncla{
        public Transform point;
        public bool isFull;
    }
    [System.Serializable]
    public class TipoDeObjetoASpawnear
    {
        public TypeOfPoints tipoDeObjeto;
        public List<GameObject> objetosASpawnear=new List<GameObject>();
    }

    public enum TypeOfPoints
    {
        Trampa, Recogible, PuntoDeAyuda
    }
#endregion
}
