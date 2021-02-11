using UnityEngine;
using UnityEngine.UI;

public class LanguageImage : LanguageType
{

    private Image image;
    
    [SerializeField]private Sprite spanish,english;
    
    void Start()
    {
        image=GetComponent<Image>();
        Initialize();
    }
    protected override void ChangeTypeToSpanish(){
        ChangeText(spanish);
    }
    protected override void ChangeTypeToEnglish(){
        ChangeText(english);
    }
    private void ChangeText(Sprite typeOfSprite){
        if(typeOfSprite!=image.sprite)image.sprite=typeOfSprite;
    }
}
