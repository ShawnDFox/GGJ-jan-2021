using System;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField]private Language language;
    private Action<Language> Change;
    private void Start() {
        Change=FindObjectOfType<MenuManager>().ChangeLanguage;
    }
    public void ChooseLanguage(){
        switch (language)
        {
            case Language.Spanish:
                Debug.Log("Español");
            break;
            case Language.English:
                Debug.Log("Ingles");
            break;
        }
        Change(language);
    }
}
