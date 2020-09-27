using System.Collections.Generic;
using UnityEngine;

public class QuestController : IInteractable {
    public Quest quest_to_complete;
    [SerializeField]
    private Realm required_realm;

    [Space(10)]
    public List<Quest> required_quests = new List<Quest>();

    [Space(10)]
    public List<GameObject> objects_to_enable = new List<GameObject>();
    public List<GameObject> objects_to_disable = new List<GameObject>();

    // Dependancies
    private QuestsModel quests_model;
    private DialogueController dialogue_controller;
    private RealmModel realm_model;

    private void Start() {
        quests_model = FindObjectOfType<QuestsModel>();
        dialogue_controller = FindObjectOfType<DialogueController>();
        realm_model = FindObjectOfType<RealmModel>();
    }

    public override void Action() {
        if (!AreQuestsCompleted()) { return; }
        if (required_realm != realm_model.GetCurrentRealm()) { return; }

        dialogue_controller.SetCurrentDialogue(this);

        CompleteQuest();
    }

    public bool AreQuestsCompleted() {
        // GUARDS
        if (required_quests.Count == 0) { return true; }

        for (int i = 0; i < required_quests.Count; i++) {
            if (!quests_model.IsQuestCompleted(required_quests[i])) { return false; }
        }

        return true;
    }

    public void CompleteQuest() {
        // GUARD
        if (quests_model.IsQuestCompleted(quest_to_complete)) { return; }

        quests_model.CompleteQuest(quest_to_complete);
    }
}