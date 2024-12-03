using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowTheLeader : MonoBehaviour
{
    public Rigidbody2D target;
    public int numFrames = 10;

    Movement player;
    Queue<Vector3> targetMovement;

    public float offset = 3f;

    float distance;

    public bool following = false;

    public GameObject battlePosition;
    void Awake()
    {
        targetMovement = new Queue<Vector3>();
        player = target.GetComponent<Movement>();

        transform.position = new Vector2(player.transform.position.x, player.transform.position.y - offset);
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, target.position);
    }

    void FixedUpdate()
    {
        //int thingAMahgig = 1;
        //print(targetMovement);
        if (player.transform.position != Vector3.zero && !targetMovement.Contains(player.transform.position))
        {

            targetMovement.Enqueue(target.transform.position);
            //thingAMahgig = 0;
        }

        if (!BattleSystem.instance.isInBattle)
        {
            if (targetMovement.Count > numFrames && distance >= offset)
            {
                transform.position = targetMovement.Dequeue();
                following = true;
                //transform.position = targetMovement.Dequeue();
            }
            else
            {
                following = false;
                //Debug.LogWarning("Not Following");
                //Debug.Break();
                
            }
        }
        else
        {
            //transform.position = battlePosition.transform.position;
        }
    }
}


