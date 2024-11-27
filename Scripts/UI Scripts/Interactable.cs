using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region Singleton
    /*
    public static Interactable instance;

    void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("More than one instance of Interactable found!");
            return;
        }
        instance = this;
    }
    */
    #endregion

    public float radius = 5f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    public bool hasInteracted = false;

    #region Search
    /*
    void Start()
    {
        Interactable[] myItems = FindObjectsOfType(typeof(Interactable)) as Interactable[];
        Debug.Log("Found " + myItems.Length + " instances with this script attached");
        foreach (Interactable item in myItems)
        {
            Debug.Log(item.gameObject.name);
        }
    }
    */
    #endregion

    public virtual void Interact()
    {
        //This method is meant to be overwritten.
        
        
        if (BattleSystem.instance.isInBattle)
            return;
        
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector2.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                if (!BattleSystem.instance.isInBattle)
                {
                    Interact();
                    hasInteracted = true;
                }

            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;

        hasInteracted = false;
    }


    public void OnDefocused()
    {
        isFocus = false;
        player = null;

        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
