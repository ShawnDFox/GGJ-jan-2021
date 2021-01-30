using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public GameObject TornilloPrefab;
    public GameObject CablePrefab;
    public GameObject ChipPrefab;

    public List<Transform> TrapPoints;
    public List<Transform> DropPoints;
    public List<Transform> HelpPoints;

    private int carry_capacity = 15;

    private GameManager Manager;

    // Start is called before the first frame update
    void Awake()
    {
        Manager = GameManager.Instance;

    }
    private void Start()
    {
        //Saber Cuantos elementos se necesitan activar en el pool
        ObjectPooler.Instance.NewPool(carry_capacity, ChipPrefab.tag, ChipPrefab);
        ObjectPooler.Instance.NewPool(carry_capacity, TornilloPrefab.tag,TornilloPrefab);
        ObjectPooler.Instance.NewPool(carry_capacity, CablePrefab.tag, CablePrefab);
        //faltan las trampas y las recargas

    }

    //logica para idear cuantos elementos debemos pedirle al jugador que recoja dependiendo del nivel actual
    public void GenerateQuest(int level)
    {

    }
    
}
