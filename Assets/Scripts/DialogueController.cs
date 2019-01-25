using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    public int CurrentDialogueIndex;
    public Sprite DialoguePortrait;
    public Sprite DialogueBackground;
    public string DialogueTitle;
    public string[] DialogueArray;
    private DialogueContainer DialogueContainerScript;

    // **************************
    // Dialogue Paramteres
    public bool IsDialogueBoxInitiated = false;
    
	void Start () {
        DialogueContainerScript = GameObject.FindGameObjectWithTag("Dialogue Container").GetComponent<DialogueContainer>();
	}

    public void ContinueDialogue()
    {

        if (CurrentDialogueIndex == DialogueArray.Length)
        {
            DialogueContainerScript.RemoveDialogueBox(gameObject);
            DialogueContainerScript.IsDialogueTextLoaded = true;
            return;
        }

        for (int i = 0; i < DialogueArray.Length; i++)
        {
            if (i == CurrentDialogueIndex)
            {
                if (IsDialogueBoxInitiated == false)
                {
                    DialogueContainerScript.DisplayDialogueBox(
                        DialoguePortrait,
                        DialogueBackground,
                        DialogueTitle,
                        DialogueArray[CurrentDialogueIndex]);

                    IsDialogueBoxInitiated = true;
                } else
                {
                    StartCoroutine(DialogueContainerScript.UpdateDialogueText(DialogueArray[CurrentDialogueIndex]));
                }
            }
        }

        CurrentDialogueIndex++;
    }

    private void OnTriggerStay(Collider other)
    {
        if (DialogueContainerScript.IsDialogueTextLoaded &&
            Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialogue();
        }
    }
}
