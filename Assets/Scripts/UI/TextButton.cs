using UnityEngine;
using TMPro;

public class TextButton : MonoBehaviour
{
    TextMeshProUGUI text;
    Color originalColor;
    private void Start() {
        text=GetComponent<TextMeshProUGUI>();
        originalColor=text.color;
    }
    public void PointerEnter(){
        text.color=new Color(originalColor.r,originalColor.g,originalColor.b,originalColor.a-0.3f);
    }
    public void PointerDown(){
        text.color=new Color(originalColor.r,originalColor.g,originalColor.b,originalColor.a-0.5f);
    }
    public void PointerExit(){
        text.color=originalColor;
    }
}
