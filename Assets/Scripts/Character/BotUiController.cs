using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BotUiController : MonoBehaviour
{
    public SliderController HP;
    public SliderController Charge;

    public TextMeshProUGUI Tornillos;
    public TextMeshProUGUI Cables;
    public TextMeshProUGUI Chips;

    private void Awake()
    {
        GetComponent<BotHealth>().SetHP += setHp;
        GetComponent<BotHealth>().SetPower += setPower;
        GetComponent<BotHealth>().OnCharge += ChargeCount;
        GetComponent<BotHealth>().OnDisCharge += ChargeCount;
        GetComponent<BotHealth>().OnHeal += HealthCount;
        GetComponent<BotHealth>().OnTakeDamage += HealthCount;
    }

    private void setPower(int val)
    {
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
