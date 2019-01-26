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

    // **************************
    // Dialogue Paramteres
    public bool IsDialogueBoxInitiated = false;
    
	void Start ()
    {
        /*
        _grayscaleTiles = GameObject.FindGameObjectWithTag("Grayscale Tiles");
        _grayscaleWalls = GameObject.FindGameObjectWithTag("Grayscale Walls");
        _defaultTiles = GameObject.FindGameObjectWithTag("Default Tiles");
        _defaultWalls = GameObject.FindGameObjectWithTag("Default Tiles");
        */

        _player = GameObject.FindGameObjectWithTag("Player");
        pointLights = GameObject.FindGameObjectsWithTag("Candle");
        _playerLight = _player.transform.GetChild(1).gameObject;
        _directionalLight = GameObject.FindGameObjectWithTag("Sun");

        // Setting up the initial lighting system settings
        foreach (GameObject lightObj in pointLights)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(false);
        }

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

        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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
                    // We check depending on which item the player interacted with
                    // if its a candle, and then we change his mode/realm
                    if (gameObject.transform.tag == "Candle")
                    {
                        if (PlayerController.IsPlayerSpooky)
                        {
                            _playerAnimator.runtimeAnimatorController = _playerScript.DefaultAnimator;

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
                        }
                        else
                        {
                            _playerAnimator.runtimeAnimatorController = _playerScript.SpookyAnimator;

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
                        }

                        PlayerController.IsPlayerSpooky = !PlayerController.IsPlayerSpooky;

                        // This makes sure the character stops moving when
                        // it switches realms.
                        _playerAnimator.SetBool("Is Player Idle", true);
                    }

                    DialogueContainerScript.DisplayDialogueBox(
                        DialoguePortrait,
                        DialogueBackground,
                        DialogueTitle,
                        DialogueArray[CurrentDialogueIndex],
                        IsObjectItem);

                    IsDialogueBoxInitiated = true;
                    PlayerController.IsCharacterInADialogue = true;
                } else
                {
                    StartCoroutine(DialogueContainerScript.UpdateDialogueText(DialogueArray[CurrentDialogueIndex], IsObjectItem));
                }
            }
        }

        CurrentDialogueIndex++;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DialogueContainerScript.IsDialogueTextLoaded &&
            Input.GetKeyDown(KeyCode.X))
        {
            ContinueDialogue();
        }
    }
}
