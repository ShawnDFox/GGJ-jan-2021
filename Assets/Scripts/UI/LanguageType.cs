using UnityEngine;

public abstract class LanguageType :MonoBehaviour
{
    protected void Initialize()
    {
        MenuManager tempMenuManager = FindObjectOfType<MenuManager>();
        tempMenuManager.ChangeLanguageToSpanish += ChangeTypeToSpanish;
        tempMenuManager.ChangeLanguageToEnglish += ChangeTypeToEnglish;
        //Esto se hace por si el idioma fue cambiado antes de añadir las funciones a las acciones
        switch (tempMenuManager.CurrentLanguage)
        {
            case Language.Spanish:
                ChangeTypeToSpanish();
                break;
            case Language.English:
                ChangeTypeToEnglish();
                break;
        }
    }
    protected abstract void ChangeTypeToSpanish();
    protected abstract void ChangeTypeToEnglish();

}
