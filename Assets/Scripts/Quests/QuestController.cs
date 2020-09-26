using System.Collections.Generic;
using UnityEngine;

public class QuestController : IInteractable {
    [SerializeField]
    public Quest quest_to_complete;

    [Space(10)]
    public List<Quest> required_quests = new List<Quest>();

    // Dependancies
    private QuestsModel quests_model;
    private DialogueController dialogue_controller;

    private void Start() {
        quests_model = FindObjectOfType<QuestsModel>();
        dialogue_controller = FindObjectOfType<DialogueController>();
    }

    public override void Action() {
        dialogue_controller.SetCurrentDialogue(quest_to_complete.dialogue);
        dialogue_controller.PlayDialogue();

        CompleteQuest();

        if (!AreQuestsCompleted()) { return; }
    }

    private bool AreQuestsCompleted() {
        for (int i = 0; i < required_quests.Count; i++) {
            if (!required_quests[i].is_completed) { return false; }
        }

        return true;
    }

    public void CompleteQuest() {
        if (required_quests.Count > 0) {
            for (int i = 0; i < required_quests.Count; i++) {
                if (required_quests[i] == quest_to_complete) {
                    quests_model.CompleteQuest(quest_to_complete);
                }
            }
        } else {
            quests_model.CompleteQuest(quest_to_complete);
        }
    }
}