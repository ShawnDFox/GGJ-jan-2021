using UnityEngine;
using TMPro;

public class LanguageText : LanguageType
{
    private TextMeshProUGUI textMesh;
    [TextArea(3,6)]
    [SerializeField]private string spanish,english;
    
    void Start()
    {
        textMesh=GetComponent<TextMeshProUGUI>();
        Initialize();
    }
    protected override void ChangeTypeToSpanish(){
        ChangeText(spanish);
    }
    protected override void ChangeTypeToEnglish(){
        ChangeText(english);
    }
    private void ChangeText(string message){
        if(message!=textMesh.text)textMesh.text=message;
    }
}
