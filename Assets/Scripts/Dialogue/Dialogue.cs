using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Object", menuName = "Scriptable Objects/Dialogue Object", order = 1)]
class Dialogue : ScriptableObject {
    public List<Passage> passages = new List<Passage>();
}