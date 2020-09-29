using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestsModel : MonoBehaviour {
    private List<Quest> quests = new List<Quest>();
    private List<Quest> completed_quests = new List<Quest>();

    [NonSerialized]
    public List<StateController> state_controllers = new List<StateController>();

    public bool IsQuestCompleted(Quest quest) {
        return completed_quests.Contains(quest);
    }

    public void CompleteQuest(Quest quest) {
        completed_quests.Add(quest);

        if (App.debugging) { Debug.Log("Quest <color=yellow>" + quest.title + "</color> has been completed!"); }
    }

    public void AddQuest(Quest quest) {
        quests.Add(quest);
    }
}