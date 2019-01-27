using System;
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
    private Image _currentDialogueBackground;
    private Text _currentDialogueTextComponent;
    private Text _currentDialogueTitle;
    private Text _optionOne;
    private Text _optionTwo;

    public bool IsDialogueTextLoaded;

    public Sprite DadPortrait_1;
    public Sprite DadPortrait_2;
    public Sprite MomPortrait_1;
    public Sprite MomPortrait_2;
    public Sprite JanePortrait;

    public Sprite DadDialogueBackground;
    public Sprite MomDialogueBackground;
    public Sprite JaneDialogueBackground;

    List<object[]> DialogueStructure = new List<object[]>();
    private int _currentDialogueIndex = 0;
    public bool IsIntroEnded = false;
    private bool _isIntroDisplayed = false;
    private bool _areOptionsGiven = false;
    private string _currentResponse = string.Empty;

    public object[] Dad_0 = new object[4];
    public object[] Jane_1 = new object[4];
    public object[] Dad_2 = new object[4];
    public object[] Dad_3 = new object[4];
    public object[] Dad_4 = new object[4];
    public object[] Jane_5 = new object[5];
    public object[] Mom_6 = new object[4];
    public object[] Jane_7 = new object[4];
    public object[] Dad_8 = new object[4];
    public object[] Jane_9 = new object[4];
    public object[] Jane_10 = new object[4];

    private string _temporaryOptionOneStr = string.Empty;
    private string _temporaryOptionTwoStr = string.Empty;

    void Start () {
        _optionOne = GameObject.FindGameObjectWithTag("Option 1").GetComponent<Text>();
        _optionTwo = GameObject.FindGameObjectWithTag("Option 2").GetComponent<Text>();

        _currentDialoguePortrait = DialoguePortrait.GetComponent<Image>();
        _currentDialogueTextComponent = CurrentDialogueText.GetComponent<Text>();
        _currentDialogueBackground = DialogueBackground.GetComponent<Image>();
        _currentDialogueTitle = DialogueTitle.GetComponent<Text>();

        Dad_0 = new object[] { "Father", DadPortrait_1, DadDialogueBackground, "Well Jane, have you finished your homework for today?" };
        Jane_1 = new object[] { "Jane", JanePortrait, JaneDialogueBackground, "No, dad.. I went out after school with some friends." };
        Dad_2 = new object[] { "Father", DadPortrait_2, DadDialogueBackground, ".. your friends are not like you Jane, you’re wasting time that you could use for studying!" };
        Dad_3 = new object[] { "Father", DadPortrait_2, DadDialogueBackground, "Otherwise, you’ll never achieve your dream of becoming a doctor.",  "Yes father, I am aware..", "I don’t even want to become a doctor..." };
        Dad_4 = new object[] { "Father", DadPortrait_1, DadDialogueBackground, "" };
        Jane_5 = new object[] { "Jane", JanePortrait, JaneDialogueBackground, "Yes father.." };
        Mom_6 = new object[] { "Mom", MomPortrait_2, MomDialogueBackground, "Oh light up darling, dad only wants the best for you." };
        Jane_7 = new object[] { "Jane", JanePortrait, JaneDialogueBackground, "the best for me.. or the best for you two?" };
        Dad_8 = new object[] { "Father", DadPortrait_2, DadDialogueBackground, "I will NOT tolerate such insolence in MY house! Go to your room right now young lady!" };
        Jane_9 = new object[] { "Jane", JanePortrait, JaneDialogueBackground, "Hmpf!" };
        Jane_10 = new object[] { "Jane", JanePortrait, JaneDialogueBackground, "I need to get out of this place.. I feel like I’m suffocating in here.!" };

        DialogueStructure = new List<object[]>() { Dad_0, Jane_1, Dad_2, Dad_3, Dad_4, Jane_5, Mom_6, Jane_7, Dad_8, Jane_9, Jane_10 };
        
        if (IsIntroEnded == false)
        {
            if (_isIntroDisplayed == false)
            {
                _currentDialoguePortrait.GetComponent<Image>().color = new Color(255, 255, 255, 1);
                DialogueBackground.GetComponent<Image>().color = new Color(255, 255, 255, 1);

                UpdateDialogueContainer();

                _isIntroDisplayed = true;
            }
        }
    }

    private IEnumerator ShowOptionsInDialogue(string character, string optionOne, string optionTwo)
    {
        yield return new WaitForSeconds(1.5f);

        if (character == "Father" || character == "Mom")
        {
            _optionOne.color = new Color(217, 217, 217, 1f);
            _optionTwo.color = new Color(217, 217, 217, 1);
        }
        else if (character == "Jane")
        {
            _optionOne.color = new Color(0, 0, 0, 1f);
            _optionTwo.color = new Color(0, 0, 0, 1f);
        }

        _temporaryOptionOneStr = optionOne;
        _temporaryOptionTwoStr = optionTwo;

        _optionOne.text = optionOne;
        _optionTwo.text = optionTwo;

        _areOptionsGiven = true;
    }

    private void UpdateDialogueContainer()
    {
        for (int i = 0; i < DialogueStructure.Count - 1; i++)
        {
            if (i == _currentDialogueIndex)
            {
                _currentDialogueTitle.text = (string)DialogueStructure[i][0];
                _currentDialoguePortrait.sprite = (Sprite)DialogueStructure[i][1];
                _currentDialogueBackground.sprite = (Sprite)DialogueStructure[i][2];

                // This changes the text color when loading different dialogue
                // boxes that have different color schemes that dont blend well.
                if ((string)DialogueStructure[i][0] == "Father" || (string)DialogueStructure[i][0] == "Mom")
                {
                    _currentDialogueTitle.color = new Color(217, 217, 217, 1);
                    _currentDialogueTextComponent.color = new Color(217, 217, 217, 0.7f);

                    _optionOne.color = new Color(217, 217, 217, 0.7f);
                    _optionTwo.color = new Color(217, 217, 217, 0.7f);
                }
                else if ((string)DialogueStructure[i][0] == "Jane")
                {
                    _currentDialogueTitle.color = new Color(0, 0, 0, 1);
                    _currentDialogueTextComponent.color = new Color(0, 0, 0, 0.7f);

                    _optionOne.color = new Color(0, 0, 0, 0.7f);
                    _optionTwo.color = new Color(0, 0, 0, 0.7f);
                }

                if (DialogueStructure[i].Length > 4)
                {
                    StartCoroutine(UpdateDialogueText((string)DialogueStructure[i][3]));

                    StartCoroutine(ShowOptionsInDialogue(
                        (string)DialogueStructure[i][0],
                        (string)DialogueStructure[i][4],
                        (string)DialogueStructure[i][5]));
                } else
                {
                    if (_currentResponse != string.Empty
                        )
                    {
                        if (_currentResponse == "Yes father, I am aware..")
                        {
                            StartCoroutine(UpdateDialogueText("Then you should act like it already!"));
                        } else if (_currentResponse == "I don’t even want to become a doctor...")
                        {
                            StartCoroutine(UpdateDialogueText("Nonsense, our family has a long history bringing up doctors. You shall not bring shame to our family name."));
                        }
                    } else
                    {
                        StartCoroutine(UpdateDialogueText((string)DialogueStructure[i][3]));
                    }
                }
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
        if (_areOptionsGiven && IsDialogueTextLoaded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _currentResponse = _temporaryOptionOneStr;
                _optionTwo.text = _temporaryOptionTwoStr;
                _optionOne.text += " <<<";
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _currentResponse = _temporaryOptionTwoStr;
                _optionOne.text = _temporaryOptionOneStr;
                _optionTwo.text += " <<<";
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpdateDialogueContainer();

                _optionOne.text = string.Empty;
                _optionTwo.text = string.Empty;
                _areOptionsGiven = false;
            }
        } else
        {
            _currentResponse = string.Empty;
        }

        if (IsIntroEnded == false &&
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
                IsIntroEnded = true;
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
