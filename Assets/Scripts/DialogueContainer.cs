using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour {
    public GameObject CharacterBackground;
    public GameObject ItemBackground;
    public GameObject ItemDialogueText;
    public GameObject Portrait;
    public GameObject DialogueTitle;
    public GameObject DialogueText;

    private Text _dialogueTextComponent;
    private Text _dialogueTitleTextComponent;
    private Text _itemDialogueTextComponent;

    public bool IsDialogueTextLoaded = true;

    private void Start()
    {
        _dialogueTitleTextComponent = DialogueTitle.GetComponent<Text>();
        _dialogueTextComponent = DialogueText.GetComponent<Text>();
        _itemDialogueTextComponent = ItemDialogueText.GetComponent<Text>();
    }

    public void DisplayDialogueBox(Sprite portrait, Sprite background, string dialogueTitle, string dialogueText, bool isObjectItem)
    {
        // Updating the dialogue elements
        if (portrait == null)
        {
            ItemBackground.GetComponent<Image>().sprite = background;
            ItemBackground.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            Portrait.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            _dialogueTitleTextComponent.text = string.Empty;
        } else
        {
            CharacterBackground.GetComponent<Image>().sprite = background;
            CharacterBackground.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            Portrait.GetComponent<Image>().sprite = portrait;
            Portrait.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            _dialogueTitleTextComponent.text = dialogueTitle;
        }
        _dialogueTextComponent.text = dialogueText;

        // and initiating the new dialogue text
        StartCoroutine(UpdateDialogueText(dialogueText, isObjectItem));
    }

    public IEnumerator UpdateDialogueText(string newDialogueText, bool isObjectItem)
    {
        IsDialogueTextLoaded = false;
        _itemDialogueTextComponent.text = string.Empty;
        _dialogueTextComponent.text = string.Empty;

        // Updating the dialogue's text field
        for (int i = 0; i < newDialogueText.Length; i++)
        {
            yield return new WaitForSeconds(0.002f);
            if (isObjectItem)
            {
                _itemDialogueTextComponent.text += newDialogueText[i];
            } else
            {
                _dialogueTextComponent.text += newDialogueText[i];
            }
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
        CharacterBackground.GetComponent<Image>().sprite = null;
        CharacterBackground.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        ItemBackground.GetComponent<Image>().sprite = null;
        ItemBackground.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        _dialogueTitleTextComponent.text = string.Empty;
        _dialogueTextComponent.text = string.Empty;
        _itemDialogueTextComponent.text = string.Empty;

        interactableObjectScript.CurrentDialogueIndex = 0;
        interactableObjectScript.IsDialogueBoxInitiated = false;
        PlayerController.IsCharacterInADialogue = false;
    }
}
