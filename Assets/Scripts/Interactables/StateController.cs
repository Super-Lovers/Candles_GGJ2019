using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    // Dependancies
    private QuestsModel quests_model;
    private QuestController quest_controller;
    private RealmModel realm_model;

    // Components
    private SpriteRenderer[] sprite_renderers;
    private BoxCollider2D collider;

    [SerializeField]
    private List<RealmState> realm_states = new List<RealmState>();

    private void Start() {
        quest_controller = GetComponent<QuestController>();
        quests_model = FindObjectOfType<QuestsModel>();
        realm_model = FindObjectOfType<RealmModel>();

        sprite_renderers = GetComponentsInChildren<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        if (!quests_model.state_controllers.Contains(this)) {
            quests_model.state_controllers.Add(this);
        }
    }

    public void UpdateState() {
        // GUARDS
        if (quest_controller != null && !quest_controller.AreQuestsCompleted()) { return; }

        for (int i = 0; i < realm_states.Count; i++) {
            if (realm_model.GetCurrentRealm() == realm_states[i].realm) {
                if (quest_controller != null) {
                    quest_controller.enabled = realm_states[i].is_enabled;
                }
                
                if (!realm_states[i].is_active) {
                    for (int j = 0; j < sprite_renderers.Length; j++) {
                        sprite_renderers[j].color = new Color(1, 1, 1, 0);
                    }
                    collider.enabled = false;
                } else {
                    for (int j = 0; j < sprite_renderers.Length; j++) {
                        sprite_renderers[j].color = new Color(1, 1, 1, 1);
                    }
                    collider.enabled = true;
                }

                break;
            }
        }
    }
}
