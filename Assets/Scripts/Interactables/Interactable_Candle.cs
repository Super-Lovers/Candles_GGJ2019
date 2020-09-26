public class Interactable_Candle : IInteractable {
    private QuestController quest_controller;

    // Dependancies
    private RealmModel realm_model;

    private void Start() {
        realm_model = FindObjectOfType<RealmModel>();
        quest_controller = GetComponent<QuestController>();
    }

    public override void Action() {
        if (quest_controller != null) {
            if (quest_controller.AreQuestsCompleted() && realm_model.GetCurrentRealm() != Realm.Ghost) {
                quest_controller.Action();
            }
        }

        realm_model.ChangeRealm();
    }
}
