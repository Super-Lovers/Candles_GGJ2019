using UnityEngine;

public class Interactable_Candle : IInteractable {
    private RealmModel realm_model;

    private void Start() {
        realm_model = FindObjectOfType<RealmModel>();
    }

    public override void Action() {
        realm_model.ChangeRealm();
    }
}
