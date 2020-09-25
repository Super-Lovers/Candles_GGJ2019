using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RealmSetting {
    public string title;
    public Realm realm;

    [Space(10)]
    public List<GameObject> objects_to_enable;
    public List<GameObject> objects_to_disable;
}