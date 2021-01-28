using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private Slider Slider;
        

    private void Awake()
    {
        Slider = GetComponent<Slider>();

    }
    public void setMaxVal(int val)
    {
        Slider.maxValue = val;
    }
    public void setVal(float val)
    {
        Slider.value = val;
    }
}
