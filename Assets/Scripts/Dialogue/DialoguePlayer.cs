using UnityEngine;

public class DialoguePlayer : IInteractable
{
    [SerializeField]
    private Dialogue dialogue_to_play = default;

    // Dependancies
    private DialogueController dialogue_controller;

    //====================
    // End-game specific reference
    //====================
    public bool is_last_dialogue;
    //====================

    private void Start() {
        dialogue_controller = FindObjectOfType<DialogueController>();
    }

    public override void Action() {
        dialogue_controller.SetCurrentDialogue(dialogue_to_play);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (is_last_dialogue) {
            dialogue_controller.SetCurrentDialogue(dialogue_to_play, true);
            return;
        }

        dialogue_controller.SetCurrentDialogue(dialogue_to_play);
    }
}