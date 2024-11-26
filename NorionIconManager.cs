using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorionIconManager : MonoBehaviour
{
    public GameObject partyMember2;
    public void disapear()
    {
        Instantiate(partyMember2, new Vector2(transform.position.x, transform.position.y - 4), Quaternion.identity);
        transform.gameObject.SetActive(false);
    }
}
