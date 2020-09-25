using System.Collections;
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
    private AudioSource _audioSource;

    private GameObject[] pointLights;
    public GameObject PlayerLight;
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
    public Sprite SpiritMotherKitchenBlockSprite;
    public Sprite SpiritFatherBlockSprite;
    public Sprite SpiritFatherWorkplaceBlockSprite;
    public GameObject HumanFatherAtPhone;
    public GameObject PhoneBlockade;

    // **************************
    // Dialogue Paramteres
    public bool IsDialogueBoxInitiated = false;
    
	void Start ()
    {
        if (PhoneBlockade)
        {
            PhoneBlockade.SetActive(false);
        }

        if (HumanFatherAtPhone)
        {
            HumanFatherAtPhone.SetActive(false);
        }
        _fadeControllerScript = GameObject.FindGameObjectWithTag("Transitioner").GetComponent<FadeController>();

        if (Timer != null)
        {
            _timerText = Timer.GetComponentInChildren<Text>();
            Timer.SetActive(false);
        }

        foreach (GameObject spiritRealmObject in SpiritRealmObjects)
        {
            if (spiritRealmObject)
            {
                if (spiritRealmObject.name == "Spirit Mother" ||
                    spiritRealmObject.name == "Spirit Father Block" ||
                    spiritRealmObject.name == "Spirit Father Block Workplace" ||
                spiritRealmObject.name == "Spirit Mother Kitchen")
                {
                    spiritRealmObject.GetComponent<SpriteRenderer>().sprite = null;
                }
                else
                {
                    spiritRealmObject.SetActive(false);
                }
            }
        }

        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player");
        pointLights = GameObject.FindGameObjectsWithTag("Candle");
        _directionalLight = GameObject.FindGameObjectWithTag("Sun");

        // Setting up the initial lighting system settings
        foreach (GameObject lightObj in pointLights)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(false);
        }

        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //_playerScript.IsNecklaceFound = true;

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

        if (PlayerLight)
        {
            PlayerLight.SetActive(false);
        }
        if (_directionalLight)
        {
            _directionalLight.SetActive(true);
        }
    }

    public void ContinueDialogue()
    {
        if (CurrentDialogueIndex == DialogueArray.Length)
        {
            DialogueContainerScript.RemoveDialogueBox(gameObject);
            DialogueContainerScript.IsDialogueTextLoaded = true;
            return;
        }
        
        if (gameObject.transform.name == "Phone" &&
            (_playerScript.IsBedChecked == false ||
            _playerScript.IsBoxChecked == false ||
            _playerScript.IsNecklaceFound == false) &&
            _playerScript.IsAlarmActive == false)
        {
            DialogueContainerScript.DisplayDialogueBox(
                DialoguePortrait,
                DialogueBackground,
                DialogueTitle,
                "This is your brother's phone.",
                IsObjectItem,
                null);

            // This makes sure it wont let you click space again
            // for a new dialogue to pop up.
            CurrentDialogueIndex += 2;

            DialogueContainerScript.IsDialogueTextLoaded = false;
            IsDialogueBoxInitiated = true;
            PlayerController.IsCharacterInADialogue = true;
            return;
        } else if (gameObject.transform.name == "Phone" && (_playerScript.IsBedChecked ||
            _playerScript.IsBoxChecked ||
            _playerScript.IsNecklaceFound ||
            _playerScript.IsAlarmActive))
        {
            if (PhoneBlockade)
            {
                PhoneBlockade.SetActive(true);
            }
            _playerScript.IsAlarmActive = true;
            GameObject.Find("Phone").GetComponent<Animator>().enabled = true;
        }

        if (gameObject.transform.name == "Cabinet Front Hide" &&
            _playerScript.IsBedChecked &&
            _playerScript.IsBoxChecked &&
            _playerScript.IsNecklaceFound &&
            _playerScript.IsAlarmActive)
        {
            _fadeControllerScript.StartFade();
            PlayerController.IsPlayerHiding = true;
            Invoke("HidePlayer", 0.5f);
            Invoke("ShowPlayer", 5f);
            return;
        }

        if (gameObject.transform.name == "Fireplace" &&
            (_playerScript.AreLogsPickedUp == false ||
            PlayerController.IsPlayerSpooky == false))
        {
            DialogueContainerScript.DisplayDialogueBox(
                DialoguePortrait,
                DialogueBackground,
                DialogueTitle,
                "The fireplace is very empty.",
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

        if (gameObject.transform.name == "Toilet")
        {
            if (_playerScript.IsBedChecked == false ||
                _playerScript.IsBoxChecked == false ||
                PlayerController.IsPlayerSpooky == false)
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

        if (gameObject.transform.name == "Spirit Father Block" ||
            gameObject.transform.name == "Spirit Father Block Workplace")
        {
            DialogueContainerScript.DisplayDialogueBox(
                DialoguePortrait,
                DialogueBackground,
                DialogueTitle,
                "This is not the room you are looking for.",
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

        if (_playerScript.IsNecklaceFound &&
            gameObject.transform.name == "Spirit Mother" &&
            PlayerController.IsPlayerSpooky == false)
        {
            DialogueContainerScript.DisplayDialogueBox(
                DialoguePortrait,
                DialogueBackground,
                DialogueTitle,
                "Where do you think you are going...",
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
                        _audioSource.PlayOneShot(_audioSource.clip);

                        StopAllCoroutines();
                        _timerText.text = "10";
                        _timeCounter = _playerScript.TimeUntilSentBack;
                        if (PlayerController.IsPlayerSpooky)
                        {
                            ReturnToHumanRealm();
                            PlayerController.IsPlayerSpooky = false;
                        }
                        else
                        {
                            SendToSpiritRealm();
                            PlayerController.IsPlayerSpooky = true;
                        }

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
            if (humanRealmObject != null)
            {
                humanRealmObject.SetActive(false);
            }
        }
        foreach (GameObject spiritRealmObject in SpiritRealmObjects)
        {
            if (spiritRealmObject)
            {
                if (spiritRealmObject.name == "Spirit Mother")
                {
                    spiritRealmObject.GetComponent<SpriteRenderer>().sprite = SpiritMotherBlockSprite;
                } else if (spiritRealmObject.name == "Spirit Mother Kitchen")
                {
                    spiritRealmObject.GetComponent<SpriteRenderer>().sprite = SpiritMotherKitchenBlockSprite;
                }
                else if (spiritRealmObject.name == "Spirit Father Block")
                {
                    spiritRealmObject.GetComponent<SpriteRenderer>().sprite = SpiritFatherBlockSprite;
                } else if (spiritRealmObject.name == "Spirit Father Block Workplace")
                {
                    spiritRealmObject.GetComponent<SpriteRenderer>().sprite = SpiritFatherWorkplaceBlockSprite;
                }
                else
                {
                    spiritRealmObject.SetActive(true);
                }
            }
        }

        foreach (GameObject lightObj in pointLights)
        {
            if (lightObj)
            {
                lightObj.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        foreach (GameObject obj in InteriorDefaultObjects)
        {
            if (obj)
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
        }

        GrayscaleWalls.SetActive(true);
        GrayscaleTiles.SetActive(true);
        DefaultTiles.SetActive(false);
        DefaultWalls.SetActive(false);

        PlayerLight.SetActive(true);
        _directionalLight.SetActive(false);

        // After a limited time, the player will be returned
        // to the human realm.
        Timer.SetActive(true);
        _timerText.text = "10";
        _timeCounter = _playerScript.TimeUntilSentBack;
        StartCoroutine(CountDown());
    }

    private void ReturnToHumanRealm()
    {
        _playerAnimator.SetBool("Is Player Idle", true);
        _playerAnimator.runtimeAnimatorController = _playerScript.DefaultAnimator;

        foreach (GameObject humanRealmObject in HumanRealmObjects)
        {
            if (humanRealmObject)
            {
                humanRealmObject.SetActive(true);
            }
        }
        foreach (GameObject spiritRealmObject in SpiritRealmObjects)
        {
            if (spiritRealmObject)
            {
                if (spiritRealmObject.name == "Spirit Mother" ||
                spiritRealmObject.name == "Spirit Father Block" ||
                spiritRealmObject.name == "Spirit Father Block Workplace")
                {
                    spiritRealmObject.GetComponent<SpriteRenderer>().sprite = null;
                }
                else
                {
                    spiritRealmObject.SetActive(false);
                }
            }
        }

        foreach (GameObject lightObj in pointLights)
        {
            if (lightObj)
            {
                lightObj.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        foreach (GameObject obj in InteriorDefaultObjects)
        {
            if (obj)
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
        }

        if (GameObject.Find("Spirit Mother").GetComponent<CapsuleCollider2D>().enabled == false)
        {
            if (GameObject.Find("Human Mother").gameObject)
            {
                Destroy(GameObject.Find("Human Mother").gameObject);
            }
        }

        PlayerLight.SetActive(false);
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
        if (gameObject.transform.name == "Spectral Logs" &&
            _playerScript.AreLogsPickedUp)
        {
            return;
        }

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
            Input.GetKeyDown(KeyCode.E))
        {
            if (CurrentDialogueIndex >= DialogueArray.Length)
            {
                FinishDialogueBox();
                if (gameObject.transform.name == "Blocked Door" && _playerScript.IsJaneDoorOpened)
                {
                    Destroy(gameObject);
                }
            } else
            {
                if (gameObject.transform.name == "Blocked Door" && _playerScript.IsJaneDoorOpened)
                {
                    Destroy(gameObject);
                }

                if (gameObject.transform.name == "Spirit Mother" &&
                GetComponent<SpriteRenderer>().sprite != null &&
                _playerScript.IsNecklaceFound)
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
                    _playerScript.IsBoxChecked &&
                    PlayerController.IsPlayerSpooky)
                {
                    _playerScript.IsNecklaceFound = true;
                }
                if (gameObject.transform.name == "Spectral Logs")
                {
                    _playerScript.AreLogsPickedUp = true;
                    Invoke("RemoveDialogueAndObject", 2f);
                }
                if (gameObject.transform.name == "Fireplace" &&
                    _playerScript.AreLogsPickedUp &&
                    PlayerController.IsPlayerSpooky)
                {
                    Invoke("ReturnToRoom", 2f);
                    Invoke("RemoveFatherAtWorkplaceDoor", 2.5f);
                }

                _playerAnimator.SetBool("Is Player Idle", true);
                ContinueDialogue();
            }
        }
    }

    private void RemoveFatherAtWorkplaceDoor()
    {
        Destroy(GameObject.Find("Spirit Father Block Workplace").gameObject);
    }

    private void RemoveDialogueAndObject()
    {
        // The mother dissapears in both spirit and human form.
        if (GameObject.Find("Human Mother") != null)
        {
            Destroy(GameObject.Find("Human Mother"));
        }

        if (gameObject.transform.name == "Spirit Mother")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject.transform.GetChild(0).gameObject);
        } else
        {
            Destroy(gameObject);
        }
        FinishDialogueBox();
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
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            ContinueDialogue();

            Invoke("RemoveDialogueAndObject", 2.5f);
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

        ReturnToHumanRealm();
    }

    private void HidePlayer()
    {
        HumanFatherAtPhone.SetActive(true);
        _player.GetComponent<SpriteRenderer>().enabled = false;
        _player.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //_player.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
    private void ShowPlayer()
    {
        if (PhoneBlockade)
        {
            PhoneBlockade.SetActive(false);
        }
        if (GameObject.Find("Spirit Father Block"))
        {
            Destroy(GameObject.Find("Spirit Father Block").gameObject);
        }
        PlayerController.IsPlayerHiding = false;
        PlayerController.IsTeleportingPlayer = false;
        _player.GetComponent<SpriteRenderer>().enabled = true;
        _player.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
