using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogueScript : MonoBehaviour
{
    public GameObject DialoguePortrait;
    public GameObject DialogueBackground;
    public GameObject CurrentDialogueText;
    public GameObject DialogueTitle;

    private Image _currentDialoguePortrait;
    private Text _currentDialogueTextComponent;
    private Text _currentDialogueTitle;

    public bool IsDialogueTextLoaded;

    public Sprite DadPortrait;
    public Sprite MomPortrait;
    public Sprite JanePortrait;

    List<object[]> DialogueStructure = new List<object[]>();
    private int _currentDialogueIndex = 0;
    private bool _isIntroEnded = false;
    private bool _isIntroDisplayed = false;

    public object[] Dad_0 = new object[3];
    public object[] Jane_1 = new object[3];
    public object[] Dad_2 = new object[3];
    public object[] Dad_3 = new object[3];
    public object[] Jane_4 = new object[3];
    public object[] Dad_5 = new object[4];
    public object[] Jane_6 = new object[3];
    public object[] Mom_7 = new object[3];
    public object[] Jane_8 = new object[3];
    public object[] Dad_9 = new object[3];
    public object[] Jane_10 = new object[3];
    public object[] Jane_11 = new object[3];
    
	void Start () {
        _currentDialoguePortrait = DialoguePortrait.GetComponent<Image>();
        _currentDialogueTextComponent = CurrentDialogueText.GetComponent<Text>();
        _currentDialogueTitle = DialogueTitle.GetComponent<Text>();

        Dad_0 = new object[] { "Father", DadPortrait, "Well Jane, have you finished your homework for today?" };
        Jane_1 = new object[] { "Jane", JanePortrait, "No, dad.. I went out after school with some friends." };
        Dad_2 = new object[] { "Father", DadPortrait, ".. your friends are not like you Jane, you’re wasting time that you could use for studying!" };
        Dad_3 = new object[] { "Father", DadPortrait, "Otherwise, you’ll never achieve your dream of becoming a doctor." };
        Jane_4 = new object[] { "Jane", JanePortrait, "Yes father, I am aware.." };
        Dad_5 = new object[] { "Father", DadPortrait, "Then you should act like it already!" };
        Jane_6 = new object[] { "Jane", JanePortrait, "Yes father.." };
        Mom_7 = new object[] { "Mom", MomPortrait, "Oh light up darling, dad only wants the best for you." };
        Jane_8 = new object[] { "Jane", JanePortrait, "the best for me.. or the best for you two?" };
        Dad_9 = new object[] { "Father", DadPortrait, "I will NOT tolerate such insolence in MY house! Go to your room right now young lady!" };
        Jane_10 = new object[] { "Jane", JanePortrait, "Hmpf!" };
        Jane_11 = new object[] { "Jane", JanePortrait, "I need to get out of this place.. I feel like I’m suffocating in here.!" };

        DialogueStructure = new List<object[]>() { Dad_0, Jane_1, Dad_2, Dad_3, Jane_4, Dad_5, Jane_6, Mom_7, Jane_8, Dad_9, Jane_10, Jane_11 };
    }

    private void UpdateDialogueContainer()
    {
        for (int i = 0; i < DialogueStructure.Count - 1; i++)
        {
            if (i == _currentDialogueIndex)
            {
                _currentDialogueTitle.text = (string)DialogueStructure[i][0];
                _currentDialoguePortrait.sprite = (Sprite)DialogueStructure[i][1];
                StartCoroutine(UpdateDialogueText((string)DialogueStructure[i][2]));
            }
        }
    }

    public IEnumerator UpdateDialogueText(string newDialogueText)
    {
        _currentDialogueTextComponent.text = string.Empty;
        IsDialogueTextLoaded = false;

        // Updating the dialogue's text field
        for (int i = 0; i < newDialogueText.Length; i++)
        {
            yield return new WaitForSeconds(0.002f);
            _currentDialogueTextComponent.text += newDialogueText[i];
            yield return new WaitForSeconds(0.002f);
        }

        _currentDialogueIndex++;
        IsDialogueTextLoaded = true;
    }

    private void Update()
    {
        if (_isIntroEnded == false &&
            IsDialogueTextLoaded && 
            Input.GetKeyDown(KeyCode.Space))
        {
            if (_isIntroDisplayed == false)
            {
                _currentDialoguePortrait.GetComponent<Image>().sprite = null;
                _currentDialoguePortrait.GetComponent<Image>().color = new Color(255, 255, 255, 1);
                DialogueBackground.GetComponent<Image>().sprite = null;
                DialogueBackground.GetComponent<Image>().color = new Color(255, 255, 255, 1);

                _isIntroDisplayed = true;
            }
            if (_currentDialogueIndex < DialogueStructure.Count - 1)
            {
                UpdateDialogueContainer();
            } else
            {
                HideDialogue();
                _isIntroEnded = true;
            }
        }
    }

    private void HideDialogue()
    {
        _currentDialoguePortrait.GetComponent<Image>().sprite = null;
        _currentDialoguePortrait.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        DialogueBackground.GetComponent<Image>().sprite = null;
        DialogueBackground.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        _currentDialogueTitle.text = string.Empty;
        _currentDialogueTextComponent.text = string.Empty;
    }
}
