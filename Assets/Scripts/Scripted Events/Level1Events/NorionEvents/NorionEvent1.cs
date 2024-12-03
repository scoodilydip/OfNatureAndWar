using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorionEvent1 : MonoBehaviour
{
    public GameObject partyMember2;
    public void TriggerEvent()
    {
        Instantiate(partyMember2, new Vector2(transform.position.x, transform.position.y - 4), Quaternion.identity);
        transform.gameObject.SetActive(false);
    }
}
