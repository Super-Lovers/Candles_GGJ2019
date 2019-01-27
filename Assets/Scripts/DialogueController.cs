using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    public int CurrentDialogueIndex;
    public Sprite DialoguePortrait;
    public Sprite DialogueBackground;
    public Sprite DialogueItemPreview;
    public string DialogueTitle;
    public string[] DialogueArray;
    private DialogueContainer DialogueContainerScript;
    public bool IsObjectItem;

    private GameObject _player;
    private Animator _playerAnimator;
    private PlayerController _playerScript;

    private GameObject[] pointLights;
    private GameObject _playerLight;
    private GameObject _directionalLight;
    public GameObject GrayscaleWalls;
    public GameObject GrayscaleTiles;
    public GameObject DefaultTiles;
    public GameObject DefaultWalls;

    public GameObject[] InteriorDefaultObjects;
    public Sprite[] InteriorSpookySprites;
    public Sprite[] InteriorDefaultSprites;

    public GameObject Timer;
    private Text _timerText;
    private int _timeCounter = 0;

    private FadeController _fadeControllerScript;

    public GameObject[] SpiritRealmObjects;
    public GameObject[] HumanRealmObjects;
    public Sprite SpiritMotherBlockSprite;

    // **************************
    // Dialogue Paramteres
    public bool IsDialogueBoxInitiated = false;
    
	void Start ()
    {
        _fadeControllerScript = GameObject.FindGameObjectWithTag("Transitioner").GetComponent<FadeController>();

        if (Timer != null)
        {
            _timerText = Timer.GetComponentInChildren<Text>();
            Timer.SetActive(false);
        }

        foreach (GameObject spiritRealmObject in SpiritRealmObjects)
        {
            if (spiritRealmObject.name == "Spirit Mother" ||
                spiritRealmObject.name == "Spirit Mother Kitchen")
            {
                spiritRealmObject.GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                spiritRealmObject.SetActive(false);
            }
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        pointLights = GameObject.FindGameObjectsWithTag("Candle");
        _playerLight = _player.transform.GetChild(1).gameObject;
        _directionalLight = GameObject.FindGameObjectWithTag("Sun");

        // Setting up the initial lighting system settings
        foreach (GameObject lightObj in pointLights)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(false);
        }

        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerScript.IsNecklaceFound = true;

        DialogueContainerScript = GameObject.FindGameObjectWithTag("Dialogue Container").GetComponent<DialogueContainer>();

        if (GrayscaleWalls != null)
        {
            GrayscaleWalls.SetActive(false);
        }
        if (GrayscaleTiles != null)
        {
            GrayscaleTiles.SetActive(false);
        }
        if (DefaultTiles != null)
        {
            DefaultTiles.SetActive(true);
        }
        if (DefaultWalls != null)
        {
            DefaultWalls.SetActive(true);
        }

        _playerLight.SetActive(false);
        _directionalLight.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (CurrentDialogueIndex == DialogueArray.Length)
        {
            DialogueContainerScript.RemoveDialogueBox(gameObject);
            DialogueContainerScript.IsDialogueTextLoaded = true;
            return;
        }

        if (gameObject.transform.name == "Toilet")
        {
            if (_playerScript.IsBedChecked == false ||
                _playerScript.IsBoxChecked == false)
            {
                DialogueContainerScript.DisplayDialogueBox(
                    DialoguePortrait,
                    DialogueBackground,
                    DialogueTitle,
                    "The toilet is very beautiful today.",
                    IsObjectItem,
                    null);

                // This makes sure it wont let you click space again
                // for a new dialogue to pop up.
                CurrentDialogueIndex += 2;

                DialogueContainerScript.IsDialogueTextLoaded = false;
                IsDialogueBoxInitiated = true;
                PlayerController.IsCharacterInADialogue = true;
                return;
            }
        }

        if (gameObject.transform.name == "Box")
        {
            if (_playerScript.IsBedChecked == false)
            {
                DialogueContainerScript.DisplayDialogueBox(
                    DialoguePortrait,
                    DialogueBackground,
                    DialogueTitle,
                    "An abandonned box stored in this storage for a while now...",
                    IsObjectItem,
                    null);

                // This makes sure it wont let you click space again
                // for a new dialogue to pop up.
                CurrentDialogueIndex += 2;

                DialogueContainerScript.IsDialogueTextLoaded = false;
                IsDialogueBoxInitiated = true;
                PlayerController.IsCharacterInADialogue = true;
                return;
            }
        }

        if (_playerScript.IsNecklaceFound == false &&
            gameObject.transform.name == "Spirit Mother")
        {
            DialogueContainerScript.DisplayDialogueBox(
                DialoguePortrait,
                DialogueBackground,
                DialogueTitle,
                "Don't even think about leaving your quarters.",
                IsObjectItem,
                null);

            CurrentDialogueIndex += 2;

            DialogueContainerScript.IsDialogueTextLoaded = false;
            IsDialogueBoxInitiated = true;
            PlayerController.IsCharacterInADialogue = true;
            return;
        }

        for (int i = 0; i < DialogueArray.Length; i++)
        {
            if (i == CurrentDialogueIndex)
            {
                if (IsDialogueBoxInitiated == false)
                {
                    // We check depending on which item the player interacted with
                    // if its a candle, and then we change his mode/realm
                    if (gameObject.transform.tag == "Candle")
                    {
                        if (PlayerController.IsPlayerSpooky)
                        {
                            ReturnToHumanRealm();
                        }
                        else
                        {
                            SendToSpiritRealm();
                        }

                        PlayerController.IsPlayerSpooky = !PlayerController.IsPlayerSpooky;

                        // This makes sure the character stops moving when
                        // it switches realms.
                        _playerAnimator.SetBool("Is Player Idle", true);
                    }

                    if (PlayerController.IsPlayerSpooky && _playerScript.IsJaneDoorOpened == false &&
                        gameObject.transform.name == "Blocked Door")
                    {
                        DialogueContainerScript.DisplayDialogueBox(
                            DialoguePortrait,
                            DialogueBackground,
                            DialogueTitle,
                            "The door can now be opened.",
                            IsObjectItem,
                            null);

                        IsDialogueBoxInitiated = true;
                        PlayerController.IsCharacterInADialogue = true;

                        _playerScript.IsJaneDoorOpened = true;
                    } else
                    {
                        DialogueContainerScript.DisplayDialogueBox(
                            DialoguePortrait,
                            DialogueBackground,
                            DialogueTitle,
                            DialogueArray[CurrentDialogueIndex],
                            IsObjectItem,
                            DialogueItemPreview);

                        IsDialogueBoxInitiated = true;
                        PlayerController.IsCharacterInADialogue = true;
                    }
                } else
                {
                    StartCoroutine(DialogueContainerScript.UpdateDialogueText(DialogueArray[CurrentDialogueIndex], IsObjectItem));
                }
            }
        }

        CurrentDialogueIndex++;
    }

    private void SendToSpiritRealm()
    {
        _playerAnimator.SetBool("Is Player Idle", true);
        _playerAnimator.runtimeAnimatorController = _playerScript.SpookyAnimator;

        foreach (GameObject humanRealmObject in HumanRealmObjects)
        {
            humanRealmObject.SetActive(false);
        }
        foreach (GameObject spiritRealmObject in SpiritRealmObjects)
        {
            if (spiritRealmObject.name == "Spirit Mother" ||
                spiritRealmObject.name == "Spirit Mother Kitchen")
            {
                spiritRealmObject.GetComponent<SpriteRenderer>().sprite = SpiritMotherBlockSprite;
            }
            else
            {
                spiritRealmObject.SetActive(true);
            }
        }

        foreach (GameObject lightObj in pointLights)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(true);
        }

        foreach (GameObject obj in InteriorDefaultObjects)
        {
            SpriteRenderer objRenderer = obj.GetComponent<SpriteRenderer>();
            foreach (Sprite spr in InteriorSpookySprites)
            {
                if ("spooky" + objRenderer.sprite.ToString() == spr.ToString())
                {
                    objRenderer.sprite = spr;
                }
            }
        }

        GrayscaleWalls.SetActive(true);
        GrayscaleTiles.SetActive(true);
        DefaultTiles.SetActive(false);
        DefaultWalls.SetActive(false);

        _playerLight.SetActive(true);
        _directionalLight.SetActive(false);

        // After a limited time, the player will be returned
        // to the human realm.
        Timer.SetActive(true);
        _timerText.text = "10";
        _timeCounter = _playerScript.TimeUntilSentBack;
        Invoke("ReturnToHumanRealm", _playerScript.TimeUntilSentBack);
        StartCoroutine(CountDown());
    }

    private void ReturnToHumanRealm()
    {
        _playerAnimator.SetBool("Is Player Idle", true);
        _playerAnimator.runtimeAnimatorController = _playerScript.DefaultAnimator;

        foreach (GameObject humanRealmObject in HumanRealmObjects)
        {
            humanRealmObject.SetActive(true);
        }
        foreach (GameObject spiritRealmObject in SpiritRealmObjects)
        {
            if (spiritRealmObject.name == "Spirit Mother")
            {
                spiritRealmObject.GetComponent<SpriteRenderer>().sprite = null;
            } else
            {
                spiritRealmObject.SetActive(false);
            }
        }

        foreach (GameObject lightObj in pointLights)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(false);
        }

        foreach (GameObject obj in InteriorDefaultObjects)
        {
            SpriteRenderer objRenderer = obj.GetComponent<SpriteRenderer>();
            foreach (Sprite spr in InteriorDefaultSprites)
            {
                if (objRenderer.sprite.ToString() == "spooky" + spr.ToString())
                {
                    objRenderer.sprite = spr;
                }
            }
        }

        _playerLight.SetActive(false);
        _directionalLight.SetActive(true);

        GrayscaleWalls.SetActive(false);
        GrayscaleTiles.SetActive(false);
        DefaultTiles.SetActive(true);
        DefaultWalls.SetActive(true);

        PlayerController.IsPlayerSpooky = !PlayerController.IsPlayerSpooky;
        Timer.SetActive(false);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (PlayerController.IsPlayerSpooky)
        {
            if (collision.transform.name == "Player" &&
            gameObject.transform.name == "Spirit Mother Kitchen Radius")
            {
                GetComponentInParent<CapsuleCollider2D>().enabled = true;
            }
        }
        else if (PlayerController.IsPlayerSpooky == false &&
          gameObject.transform.name == "Spirit Mother Kitchen Radius")
        {
            GetComponentInParent<CapsuleCollider2D>().enabled = false;
        }

        if (DialogueContainerScript.IsDialogueTextLoaded &&
            PlayerController.IsTeleportingPlayer == false &&
            Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentDialogueIndex >= DialogueArray.Length)
            {
                FinishDialogueBox();
            } else
            {
                if (gameObject.transform.name == "Blocked Door" && _playerScript.IsJaneDoorOpened)
                {
                    Destroy(gameObject);
                }

                if (gameObject.transform.name == "Spirit Mother" && _playerScript.IsNecklaceFound)
                {
                    Invoke("RemoveDialogueAndObject", 3f);
                }

                if (gameObject.transform.name == "Parents Bed")
                {
                    _playerScript.IsBedChecked = true;
                }
                if (gameObject.transform.name == "Box")
                {
                    _playerScript.IsBoxChecked = true;
                }
                if (gameObject.transform.name == "Toilet" &&
                    _playerScript.IsBedChecked &&
                    _playerScript.IsBoxChecked)
                {
                    _playerScript.IsNecklaceFound = true;
                }

                _playerAnimator.SetBool("Is Player Idle", true);
                ContinueDialogue();
            }
        }
    }

    private void RemoveDialogueAndObject()
    {
        // The mother dissapears in both spirit and human form.
        if (GameObject.Find("Human Mother") != null)
        {
            Destroy(GameObject.Find("Human Mother"));
        }

        FinishDialogueBox();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" &&
            gameObject.transform.name == "Human Mother")
        {
            PlayerController.IsTeleportingPlayer = true;
            _playerAnimator.SetBool("Is Player Idle", true);
            ContinueDialogue();

            Invoke("ReturnToRoom", 2f);
            Invoke("RemoveDialogueBox", 2.5f);
        } else if (collision.transform.tag == "Player" &&
            gameObject.transform.name == "Human Mother")
        {
            _playerAnimator.SetBool("Is Player Idle", true);
            ContinueDialogue();
        } else if (collision.transform.tag == "Player" &&
            gameObject.transform.name == "Spirit Father")
        {
            _playerAnimator.SetBool("Is Player Idle", true);
            ContinueDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" &&
            gameObject.transform.name == "Spirit Father")
        {
            Destroy(gameObject);
        }
    }

    private void RemoveDialogueBox()
    {
        ContinueDialogue();
    }

    private void FinishDialogueBox()
    {
        DialogueContainerScript.RemoveDialogueBox(gameObject);
        DialogueContainerScript.IsDialogueTextLoaded = true;
        CurrentDialogueIndex = 0;
    }

    private void ReturnToRoom()
    {
        _fadeControllerScript.StartFade();
    }

    private IEnumerator CountDown()
    {
        for (int i = 0; i < _playerScript.TimeUntilSentBack; i++)
        {
            yield return new WaitForSeconds(1f);
            _timeCounter--;
            _timerText.text = _timeCounter.ToString();
        }
    }
}
