using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Battle_Manager : MonoBehaviour
{

    [SerializeField] private UnityEvent startBattle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !BattleSystem.instance.isInBattle)
        {
            print(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            TriggerBattle();

            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
       
        
    }

    void TriggerBattle() {
        print("Function Works");
        startBattle.Invoke();

    }
}
