﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class RealmModel : MonoBehaviour {
    private Realm current_realm;

    private int realm_countdown = 3;
    private float time_passed = 0;

    [NonSerialized]
    public List<Furniture> furniture = new List<Furniture>();

    [SerializeField]
    private List<RealmSetting> realm_settings = new List<RealmSetting>();

    private void Update() {
        if (current_realm == Realm.Ghost) {
            time_passed += Time.deltaTime;

            if (time_passed >= realm_countdown) {
                ChangeRealm();
            }
        }
    }

    public void ChangeRealm() {
        if (current_realm == Realm.Ghost && time_passed < realm_countdown)
            return;

        var new_realm = Realm.Human;
        if (current_realm == Realm.Ghost) {
            new_realm = Realm.Human;
        } else if (current_realm == Realm.Human) {
            new_realm = Realm.Ghost;
        }

        current_realm = new_realm;

        for (int i = 0; i < furniture.Count; i++) {
            furniture[i].Turn(new_realm);
        }

        for (int i = 0; i < realm_settings.Count; i++) {
            if (realm_settings[i].realm == current_realm) {
                var realm_setting = realm_settings[i];

                for (int j = 0; j < realm_setting.objects_to_enable.Count; j++) {
                    realm_setting.objects_to_enable[j].SetActive(true);
                }
                for (int j = 0; j < realm_setting.objects_to_disable.Count; j++) {
                    realm_setting.objects_to_disable[j].SetActive(false);
                }

                break;
            }
        }

        time_passed = 0;
    }

    public Realm GetCurrentRealm() {
        return current_realm;
    }
}
