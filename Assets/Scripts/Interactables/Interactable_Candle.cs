public class Interactable_Candle : IInteractable {
    private RealmModel realm_model;
    private QuestController quest_controller;

    private void Start() {
        realm_model = FindObjectOfType<RealmModel>();
        quest_controller = GetComponent<QuestController>();
    }

    public override void Action() {
        if (realm_model.GetCurrentRealm() != Realm.Ghost) {
            quest_controller.Action();
            realm_model.ChangeRealm();
        }
    }
}
