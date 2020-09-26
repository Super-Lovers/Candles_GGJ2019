using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    [SerializeField]
    private Image character_portrait;
    [SerializeField]
    private Image passage_box;
    [SerializeField]
    private TextMeshProUGUI passage_text;

    [Space(10)]
    [SerializeField]
    private Dialogue current_dialogue;

    // Dialogue parameters
    private int current_passage_index = 0;
    private bool is_dialogue_text_loaded;

    // Dependancies
    private PlayerController player;

    private void Start() {
        player = FindObjectOfType<PlayerController>();

        //LoadPassage(current_passage_index);
        //current_passage_index++;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && current_dialogue != null) {
            if (is_dialogue_text_loaded) { 
                if (current_passage_index == current_dialogue.passages.Count) {
                    EndDialogue();

                    current_dialogue = null;
                } else {
                    LoadPassage(current_passage_index);
                    current_passage_index++;
                }
            }
        }
    }

    private void UpdateColorOfPassageText(Passage passage) {
        if (passage.character_name == "Jane") {
            passage_text.color = new Color(0, 0, 0, 1);
        } else {
            passage_text.color = new Color(1, 1, 1, 1);
        }
    }

    private void LoadPassage(int index) {
        player.can_move = false;

        if (index == 0) {
            character_portrait.color = new Color(1, 1, 1, 1);
            passage_box.color = new Color(1, 1, 1, 1);
        }

        var passage = current_dialogue.passages[index];
        UpdateColorOfPassageText(passage);
        UpdateDialogueSprites(passage);
        StartCoroutine(UpdateDialogueText(passage));
    }

    private IEnumerator UpdateDialogueText(Passage passage) {
        is_dialogue_text_loaded = false;
        passage_text.text = string.Empty;

        for (int i = 0; i < passage.passage_text.Length; i++) {
            yield return new WaitForSeconds(0.002f);
            passage_text.text += passage.passage_text[i];
            yield return new WaitForSeconds(0.002f);
        }

        is_dialogue_text_loaded = true;
    }

    private void UpdateDialogueSprites(Passage passage) {
        character_portrait.sprite = passage.character_portrait;
        passage_box.sprite = passage.passage_box;
    }

    private void EndDialogue() {
        StopAllCoroutines();

        character_portrait.sprite = null;
        character_portrait.color = new Color(1, 1, 1, 0);
        passage_box.sprite = null;
        passage_box.color = new Color(1, 1, 1, 0);
        passage_text.text = string.Empty;

        current_passage_index = 0;
        player.can_move = true;

        // ********** Conditional ********** //
        // Moves the player in his starting position //
        if (current_dialogue.name == "introduction") {
            FindObjectOfType<FadeController>().StartFade();

            StartCoroutine(MovePlayerToStartTheGame());
        }
    }

    private IEnumerator MovePlayerToStartTheGame() {
        yield return new WaitForSeconds(0.5f);

        var player = FindObjectOfType<PlayerController>();
        player.transform.position =
            GameObject.FindGameObjectWithTag("player_starting_position").transform.position;
    }

    public void SetCurrentDialogue(Dialogue dialogue) {
        current_dialogue = dialogue;
        PlayDialogue();
    }

    public void PlayDialogue() {
        EndDialogue();

        LoadPassage(current_passage_index);
        current_passage_index++;
    }
}
