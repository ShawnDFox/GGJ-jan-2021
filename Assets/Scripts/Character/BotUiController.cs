using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class BotUiController : MonoBehaviour
{
    public SliderController HP;
    public SliderController Charge;
    [Header("")]
    public TextMeshProUGUI Tornillos;
    public TextMeshProUGUI Cables;
    public TextMeshProUGUI Chips;
    public TextMeshProUGUI Resistencias;
    [Header("")]
    public Image Tornillo;
    public Image Cable;
    public Image Chip;
    public Image Resistencia;
    [Header("")]
    public Sprite TornilloColor;
    public Sprite CableColor;
    public Sprite ChipColor;
    public Sprite ResistenciaColor;

    private void Awake()
    {
        var botHeatlh = GetComponent<BotHealth>();
        botHeatlh.SetHP += setHp;
        botHeatlh.SetPower += setPower;
        botHeatlh.OnCharge += ChargeCount;
        botHeatlh.OnDisCharge += ChargeCount;
        botHeatlh.OnHeal += HealthCount;
        botHeatlh.OnTakeDamage += HealthCount;
        GetComponent<BotInventory>().OnItemPickup += PickUp;
    }

    
    private void PickUp(string arg1, int arg2, int arg3)
    {
        switch (arg1)
        {
            case "Tornillo":
                 Tornillos.text = $"{arg2}/{arg3}";
                if (arg2 > 0)
                {
                    Tornillo.sprite = TornilloColor;
                }
                break;
            case "Cable":
                Cables.text = $"{arg2}/{arg3}";
                if (arg2 > 0)
                {
                    Cable.sprite = CableColor;
                }
                break;
            case "Chip":
                Chips.text = $"{arg2}/{arg3}";
                if (arg2 > 0)
                {
                    Chip.sprite = ChipColor;
                }
                break;
            case "Resistencia":
                Resistencias.text=$"{arg2}/{arg3}";
                if (arg2 > 0)
                {
                    Resistencia.sprite = ResistenciaColor;
                }break;
        }
    }

    private void setPower(int val)
    {
        //Debug.Log("Setting Value");
        Charge.setMaxVal(val);
    }

    private void setHp(int val)
    {
        HP.setMaxVal(val);
    }

    private void ChargeCount(float val)
    {
        Charge.setVal(val);
    }

    private void HealthCount(float val)
    {
        HP.setVal(val);
    }
    
}
