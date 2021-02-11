using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	//public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	public Animator animator;

	public bool isopen;
    public float WaitTime=0.01f;

	private Queue<LanguageText> sentences; //manteine un conteo de las oraciones

	// Use this for initialization
	void Start () {
		sentences = new Queue<LanguageText> ();
	}

	public void startDialog(Dialog dialogue)
	{
		animator.SetBool ("isOpen", true);
		isopen = true;
		//nameText.SetText (dialogue.name);
		sentences.Clear ();
		foreach (LanguageText sentence in dialogue.sentences) 
		{
			sentences.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0) {
			EndDialogue ();
		} else {
			LanguageText sentence = sentences.Dequeue ();
			StopAllCoroutines ();
			StartCoroutine (TypeSentence (sentence.ToString()));
		}
		//dialogueText.SetText (sentence);

	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		string finaltext = "";
		foreach (char letter in sentence.ToCharArray()) 
		{
			finaltext += letter;
			dialogueText.text = finaltext; // text += letter;	
			yield return new WaitForSecondsRealtime(WaitTime);
		}

	}

	public void EndDialogue()
	{
		animator.SetBool ("isOpen", false);
		isopen = false;
		//Debug.Log ("End Of conversation");
	}
}
