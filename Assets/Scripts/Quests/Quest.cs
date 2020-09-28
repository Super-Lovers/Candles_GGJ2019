using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest", order = 1)]
public class Quest : ScriptableObject {
    public string title;
    public bool is_completed;

    [Space(10)]
    public Dialogue dialogue;

    [Space(10)]
    public List<GameObject> objects_to_enable = new List<GameObject>();
    public List<GameObject> objects_to_disable = new List<GameObject>();
}