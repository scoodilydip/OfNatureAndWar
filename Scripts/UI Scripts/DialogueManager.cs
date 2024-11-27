using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    public Button button1;
    public Button button2;
    public TextMeshProUGUI button2Text;
    public Image npcSprite;
    public GameObject dialogueBox;

    public string type;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public Queue<string> sentances;

    int queueNumber = 0;

    bool hasCutscene;

    GameObject cutscene;
    GameObject dialogueObject;
    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();

    }

    public void StartDialogue (Dialogue dialogue)
    {
        dialogueBox.gameObject.SetActive(true);
        animator.SetBool("IsOpen", true);
        nameText.GetComponent<TMPro.TextMeshProUGUI>().text = dialogue.name;
        type = dialogue.type;
        npcSprite.sprite = dialogue.sprite;
        npcSprite.gameObject.SetActive(true);

        sentances.Clear();

        if(dialogue.hasCutscene == true)
        {
            cutscene = dialogue.cutscene;
            dialogueObject = dialogue.parent;
        }

        foreach (string sentance in dialogue.sentances)
        {
            sentances.Enqueue(sentance);
        }

        queueNumber++;
        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        print(sentances.Count);
        if (sentances.Count == 0)
        {
            print("IDK!");
            EndDialogue();
            if(cutscene != null)
            {
                print("stuff");
                cutscene.SetActive(true);
            }
            
            return;
        }
        print("Didn't Return");
        if(type == "Shopkeeper")
        {
            if (sentances.Count == 1)
            {
                button1.gameObject.SetActive(true);
                button2Text.GetComponent<TMPro.TextMeshProUGUI>().text = "No";
            }
        }

        string sentance = sentances.Dequeue();
        queueNumber++;

        StopAllCoroutines();
        StartCoroutine(TypeSentance(sentance));
    }

    IEnumerator TypeSentance (string sentance)
    {
        dialogueText.text = "";
        foreach (char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        animator.SetBool("IsOpen", false);
        npcSprite.gameObject.SetActive(false);
    }
}
