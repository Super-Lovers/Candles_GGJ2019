public class Interactable_Candle : IInteractable {
    private QuestController quest_controller;

    // Dependancies
    private RealmModel realm_model;
    private QuestsModel quest_model;

    private void Start() {
        realm_model = FindObjectOfType<RealmModel>();
        quest_model = FindObjectOfType<QuestsModel>();
        quest_controller = GetComponent<QuestController>();
    }

    public override void Action() {
        if (realm_model.GetCurrentRealm() != Realm.Ghost) {
            if (quest_controller != null) { quest_controller.Action(); }
            realm_model.ChangeRealm();

            for (int i = 0; i < quest_model.state_controllers.Count; i++) {
                quest_model.state_controllers[i].UpdateState();
            }
        }
    }
}
