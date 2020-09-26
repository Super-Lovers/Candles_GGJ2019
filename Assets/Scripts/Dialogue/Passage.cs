using System;
using UnityEngine;

[Serializable]
public class Passage {
    public string title;
    public Character character;

    [Space(10)]
    public Sprite character_portrait;
    public Sprite passage_box;

    [Space(10)]
    [TextArea(2, 10)]
    public string passage_text;
}
