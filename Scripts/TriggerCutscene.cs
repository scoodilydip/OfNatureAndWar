using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    public bool triggered = false;
    [SerializeField]
    private GameObject _cutscene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            _cutscene.SetActive(true);
            //_cutscene.SetActive(false);
            triggered = true;
            print("Dude");
        }
    }
}
