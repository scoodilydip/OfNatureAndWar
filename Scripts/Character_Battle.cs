using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Battle : MonoBehaviour
{
    private Character_Base character_Base;
    private void Awake()
    {
        character_Base = GetComponent<Character_Base>();
    }
}
