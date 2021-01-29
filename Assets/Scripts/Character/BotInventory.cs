using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInventory : MonoBehaviour
{
    
    [SerializeField]
    private int Max_Carry = 15;

    public int Tornillos;
    public int Cables;
    public int Chips;

    //Elementos a obtener
    public int Get_Tornillos=1;
    public int Get_Cables=1;
    public int Get_Chips=1;

    public event Action<String,int,int> OnItemPickup;

    //referencia a  las partes las cuales debemos tener en el bot

    public void setQuest(int Torn, int cabl, int chip)
    {
        Get_Tornillos = Torn;
        Get_Cables = Cables;
        Get_Chips = chip;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tornillo": OnItemPickup(collision.tag, 1, Get_Tornillos);
                break;
            case "Cable":
                OnItemPickup(collision.tag, 1, Get_Cables);
                break;
            case "Chip":
                OnItemPickup(collision.tag, 1, Get_Chips);
                break;
        }
        
    }

}
