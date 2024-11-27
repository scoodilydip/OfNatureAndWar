using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class containerInteraction : Interactable
{
    public Canvas containerUI;

    public ContainerScript conScript;
    public ContainerUI conUI;

    PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = PauseMenu.instance;
    }
    public override void Interact()
    {
        base.Interact();
        
        //print("Interacting with " + transform.name)

        if(!conUI.inUI && !BattleSystem.instance.isInBattle)
            OpenContainer();
        conUI.inUI = true;
    }

    void OpenContainer()
    {
        //print("Opening " + transform.name);

        containerUI.gameObject.SetActive(true);
        conUI.containerName.GetComponent<TMPro.TextMeshProUGUI>().text = transform.name;
        conUI.containerScript = conScript;
        print(conUI.containerScript);
        conScript.Init();
        if (conScript.onItemChangedCallback != null)
            conScript.onItemChangedCallback.Invoke();
        conUI.PauseGame();

    }
}
