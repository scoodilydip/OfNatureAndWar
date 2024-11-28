using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;

    public string type;
    public Sprite sprite;

    public bool hasCutscene;

    public GameObject cutscene;
    public GameObject parent;

    [TextArea(3, 10)]
    public string[] sentances;

    //[TextArea(3, 10)]
    //public string[] playerResponses;

}
