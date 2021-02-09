using UnityEngine;

public class DesactivateDialogue : MonoBehaviour
{
    [SerializeField]GameObject dialogoInterfaz;
    private void OnEnable() {
        dialogoInterfaz.SetActive(false);
    }

    public void ActivateDialogue(){
        dialogoInterfaz.SetActive(true);
    }
}
