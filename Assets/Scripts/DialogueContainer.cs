using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour {
    public GameObject Background;
    public GameObject Portrait;
    public GameObject DialogueTitle;
    public GameObject DialogueText;

    private Text _dialogueTextComponent;
    private Text _dialogueTitleTextComponent;

    public bool IsDialogueTextLoaded = true;

    private void Start()
    {
        _dialogueTextComponent = DialogueText.GetComponent<Text>();
        _dialogueTitleTextComponent = DialogueTitle.GetComponent<Text>();
    }

    public void DisplayDialogueBox(Sprite portrait, Sprite background, string dialogueTitle, string dialogueText)
    {
        // Updating the dialogue elements
        Portrait.GetComponent<Image>().sprite = portrait;
        Portrait.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        Background.GetComponent<Image>().sprite = background;
        Background.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        _dialogueTextComponent.text = dialogueText;
        _dialogueTitleTextComponent.text = dialogueTitle;

        // and initiating the new dialogue text
        StartCoroutine(UpdateDialogueText(dialogueText));
    }

    public IEnumerator UpdateDialogueText(string newDialogueText)
    {
        IsDialogueTextLoaded = false;
        _dialogueTextComponent.text = string.Empty;

        // Updating the dialogue's text field
        for (int i = 0; i < newDialogueText.Length; i++)
        {
            yield return new WaitForSeconds(0.002f);
            _dialogueTextComponent.text += newDialogueText[i];
            yield return new WaitForSeconds(0.002f);
        }

        IsDialogueTextLoaded = true;
    }

    public void RemoveDialogueBox(GameObject interactableObject)
    {
        DialogueController interactableObjectScript = interactableObject.GetComponent<DialogueController>();

        // Resets all the dialogue objects so that when
        // new ones are loaded, the old ones dont get shown when
        // displaying the new dialogue box and updating it with the new parameters
        Portrait.GetComponent<Image>().sprite = null;
        Portrait.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        Background.GetComponent<Image>().sprite = null;
        Background.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        _dialogueTextComponent.text = string.Empty;
        _dialogueTitleTextComponent.text = string.Empty;

        interactableObjectScript.CurrentDialogueIndex = 0;
        interactableObjectScript.IsDialogueBoxInitiated = false;
    }

    private void ToggleDialogueElements()
    {

    }
}
