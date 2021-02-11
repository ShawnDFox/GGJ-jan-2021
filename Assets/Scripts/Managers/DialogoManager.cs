using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogoManager : MonoBehaviour
{
    [Header("Globos de Texto")]
    [SerializeField]private GameObject globoAva;
    [SerializeField]private GameObject globoDot;
    [Header("Dialogo")]
    [SerializeField]private TextMeshProUGUI dialogue;
    [SerializeField]private List<Dialogue> dialogues;
    [SerializeField]private UnityEvent finalEvent;
    private MenuManager menuManager;
    private int index=-1;
    private bool isRepeating=false;
    private bool change;
    private void OnEnable()
    {
        index = -1;
        isRepeating = false;
    }

    private void Start() {
        menuManager=FindObjectOfType<MenuManager>();
        NextDialogue();
    }
    public void NextDialogue(){
        index++;
        ContinueDialogue();
    }
    public void PreviousDialogue(){
        //Desactiva los objetos que estaban activados del dialogo actual
        if (dialogues[index].objectsToActivate.Length >= 1)
        {
            foreach (GameObject item in dialogues[index].objectsToActivate)
            {
                if (item.activeInHierarchy) item.SetActive(false);
            }
        }

        index--;
        ContinueDialogue();
    }
    private void ContinueDialogue(){
        index=Mathf.Clamp(index,-1,dialogues.Count);
        if(index<dialogues.Count && index>=0){
            //Desactiva los objetos
            if(dialogues[index].objectsToDesactivate.Length>=1) {
                foreach (GameObject item in dialogues[index].objectsToDesactivate)
                {
                    if(item.activeInHierarchy) item.SetActive(false);
                }
            }
            //Selecciona el globo de la persona que ha a hablar
            switch(dialogues[index].name){
                case Dialogue.nameCharacter.Ava:
                    globoAva.SetActive(true);
                    globoDot.SetActive(false);
                break;
                case Dialogue.nameCharacter.Dot:
                    globoAva.SetActive(false);
                    globoDot.SetActive(true);
                break;
            }
            //Activa los objetos
            if(dialogues[index].objectsToActivate.Length>=1) {
                foreach (GameObject item in dialogues[index].objectsToActivate)
                {
                   if(!item.activeInHierarchy) item.SetActive(true);
                }
            }
            //Dependiendo el idioma, selecciona un dialogo
            if(menuManager.CurrentLanguage==Language.Spanish) dialogue.text=dialogues[index].spanishDialogue;
            else dialogue.text=dialogues[index].englishDialogue;
        } 
        //Final event
        else if(!isRepeating && index>=0) {
            isRepeating=true;
            finalEvent.Invoke();
        }
    }
}


[System.Serializable]
class Dialogue {
    public enum nameCharacter{
        Ava,Dot
    }
    public nameCharacter name;
    [TextArea(2,6)][Header("English")]
    public string englishDialogue;
    [TextArea(2,6)][Header("Spanish")]
    public string spanishDialogue;
    [Header("")]
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDesactivate;
}
