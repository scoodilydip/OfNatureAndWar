using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    public bool canMove = true;

    public Interactable focus;

    public Camera cam;

    public LayerMask groundLayer;  

    public float maxDistance;

    public bool inBattle = false;

    public bool playable;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (canMove && playable)
        {
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        } 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetMouseButtonDown(0) && playable)
        {
            /*
            RaycastHit2D hit = new RaycastHit2D();
            Ray2D ray = cam.ScreenPointToRay(Input.mousePosition);
            */

            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if(rayHit.collider != null && !BattleSystem.instance.isInBattle)
            {
                //print("detects thing");
                Interactable interactable = rayHit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    //print("detects interactable");
                }
                
            }
            else if (focus != null && !BattleSystem.instance.isInBattle)
            {
                RemoveFocus();
            }

        }
        #region failure
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100.0f))
            {
                print(rayHit.collider.gameObject);
                if (rayHit.collider.tag == "Item") //I test tag, but you could what you want..
                {
                    print("detects thing");
                    Interactable interactable = rayHit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                        print("detects interactable");

                    }
                }

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                print("detects thing");
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    print("detects interactable");
                }
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

            if(hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    print("detects interactable");
                }
            }

        }
        */
        #endregion

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Item" && playable)
        {
            Interactable interactable = col.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                //SetFocus(interactable);
                print("detects interactable");
            }
        }
    }



    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();

            focus = newFocus;
        }
        newFocus.OnFocused(transform);


    }

    public void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus.OnDefocused();
        focus = null;

        canMove = true;
        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        if (canMove && playable)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        if (focus != null)
        {
            canMove = false;
            moveToFocus();
        }
    }

    public void moveToFocus()
    {
        float distance = Vector2.Distance(transform.position, focus.interactionTransform.position);

        if(focus != null)
        {
            rb.position = Vector2.MoveTowards(rb.position, focus.interactionTransform.position, 3f * Time.fixedDeltaTime);

            Quaternion rotation = Quaternion.LookRotation(focus.gameObject.transform.position - transform.position, transform.TransformDirection(Vector2.up));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            /*
            if (distance >= maxDistance)
            {
                rb.position = Vector2.MoveTowards(rb.position, focus.interactionTransform.position, 3f * Time.fixedDeltaTime);

                Quaternion rotation = Quaternion.LookRotation(focus.interactionTransform.position - transform.position, transform.TransformDirection(Vector2.up));
                transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            }
            */
        }
    }

    public void MakePlayable()
    {
        playable = true;
    }

    public void MakeNotPlayable()
    {
        playable = false;
    }
}
