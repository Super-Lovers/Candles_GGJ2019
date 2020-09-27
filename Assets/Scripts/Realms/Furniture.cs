using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    private Sprite human_realm_sprite;
    [SerializeField]
    private Sprite ghost_realm_sprite;

    private RealmModel realm_model;
    private SpriteRenderer sprite_renderer;

    private void Start() {
        realm_model = FindObjectOfType<RealmModel>();
        sprite_renderer = GetComponent<SpriteRenderer>();

        if (!realm_model.furniture.Contains(this)) {
            realm_model.furniture.Add(this);
        }

        Turn(realm_model.GetCurrentRealm());
    }

    public void Turn(Realm realm) {
        if (realm == Realm.Human) {
            sprite_renderer.sprite = human_realm_sprite;
        } else if (realm == Realm.Ghost) {
            sprite_renderer.sprite = ghost_realm_sprite;
        }
    }
}
