using System;
using UnityEngine;

[Serializable]
public class RealmState {
    public Realm realm;

    [Space(10)]
    public bool is_enabled;
    public bool is_active;
}
