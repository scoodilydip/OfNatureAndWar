using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyUI : MonoBehaviour
{
    public Party party;
    public Image PartyMember1Icon;
    public Image PartyMember2Icon;
    public Image PartyMember3Icon;

    public TextMeshProUGUI partyMember1Name;
    public TextMeshProUGUI partyMember2Name;
    public TextMeshProUGUI partyMember3Name;

    //public Image[] icons;
    //public TextMeshProUGUI[] partyNames;
    
    void Start()
    {
        //updateIcons();
    }

    public void updateIcons()
    {
        if (party.partyMembers[0] != null) {
            partyMember1Name.text = party.partyMembers[0].name;
            PartyMember1Icon.GetComponent<Image>().sprite = party.partyMembers[0].transform.Find("Icon").GetComponent<SpriteRenderer>().sprite;
        }

        if (party.partyMembers[1] != null) {
            partyMember2Name.text = party.partyMembers[1].name;
            PartyMember2Icon.GetComponent<Image>().sprite = party.partyMembers[1].transform.GetChild(0).Find("Icon").GetComponent<SpriteRenderer>().sprite;
        }

        if (party.partyMembers[2] != null) {
            partyMember3Name.text = party.partyMembers[2].name;
            PartyMember3Icon.GetComponent<Image>().sprite = party.partyMembers[2].transform.GetChild(0).Find("Icon").GetComponent<SpriteRenderer>().sprite;
        }

        /*for (int i = 0; i < party.partyMembers.Count; i++) {
            if (i == 0 && party.partyMembers[0] != null) {
                partyMember1Name.text = party.partyMembers[0].name;
                icons[i].GetComponent<Image>().sprite = party.partyMembers[0].transform.Find("Icon").GetComponent<SpriteRenderer>().sprite;
            } else {
                partyNames[i].text = party.partyMembers[i].name;
                icons[i].GetComponent<Image>().sprite = party.partyMembers[2].transform.GetChild(0).Find("Icon").GetComponent<SpriteRenderer>().sprite;
            }

        thinking about turning the function into a loop.
        }*/ 
    }
}
