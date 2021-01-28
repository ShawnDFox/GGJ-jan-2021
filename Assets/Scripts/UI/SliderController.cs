using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private Slider Slider;
    private int maxval=1;
    private int currentval = 0;
    public int MaxVal
    {
        get => maxval;
        set
        {
            maxval = value;
            Slider.maxValue = MaxVal;
        }
    }
    public int CurrentValue
    {
        get => currentval;
        set {
            currentval = value;
            Slider.value = currentval;
        }
    }

    private void Awake()
    {
        Slider = GetComponent<Slider>();

    }

}
