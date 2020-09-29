using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    [SerializeField]
    private Image character_portrait = default;
    [SerializeField]
    private Image passage_box = default;
    [SerializeField]
    private TextMeshProUGUI passage_text = default;

    [Space(10)]
    [SerializeField]
    private Dialogue current_dialogue;

    // Dialogue parameters
    private int current_passage_index = 0;
    private bool is_dialogue_text_loaded;

    // Dependancies
    private PlayerController player;
    private QuestController current_quest_controller;

    //====================
    // End-game specific reference
    //====================
    private bool is_endgame = false;
    //====================

    private void Start() {
        player = FindObjectOfType<PlayerController>();

        LoadPassage(current_passage_index);
        current_passage_index++;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && current_dialogue != null) {
            if (is_dialogue_text_loaded) { 
                if (current_passage_index == current_dialogue.passages.Count) {
                    EndDialogue();
                } else {
                    LoadPassage(current_passage_index);
                    current_passage_index++;
                }
            }
        }
    }

    private void UpdateColorOfPassageText(Passage passage) {
        if (passage.character.ToString() == "Jane") {
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


        if (current_quest_controller != null) {
            for (int i = 0; i < current_quest_controller.objects_to_enable.Count; i++) {
                current_quest_controller.objects_to_enable[i].SetActive(true);
            }
            for (int i = 0; i < current_quest_controller.objects_to_disable.Count; i++) {
                current_quest_controller.objects_to_disable[i].SetActive(false);
            }
        }

        // ********** Conditional ********** //
        // Moves the player in his starting position //
        if (current_dialogue.name == "introduction") {
            FindObjectOfType<FadeController>().StartFade();

            StartCoroutine(MovePlayerToStartTheGame());
        }

        current_dialogue = null;

        if (is_endgame) {
            SceneManager.LoadScene("main_menu");
        }
    }

    private IEnumerator MovePlayerToStartTheGame() {
        yield return new WaitForSeconds(0.5f);

        var player = FindObjectOfType<PlayerController>();
        player.transform.position =
            GameObject.FindGameObjectWithTag("player_starting_position").transform.position;
    }

    public void SetCurrentDialogue(QuestController quest_controller) {
        current_quest_controller = quest_controller;
        current_dialogue = quest_controller.quest_to_complete.dialogue;
        PlayDialogue();
    }

    public void SetCurrentDialogue(Dialogue dialogue) {
        current_quest_controller = null;
        current_dialogue = dialogue;
        PlayDialogue();
    }

    public void SetCurrentDialogue(Dialogue dialogue, bool _is_endgame) {
        is_endgame = _is_endgame;

        current_quest_controller = null;
        current_dialogue = dialogue;
        PlayDialogue();
    }

    public void PlayDialogue() {
        LoadPassage(current_passage_index);
        current_passage_index++;
    }
}
