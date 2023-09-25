using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private GameController game_controller;
    // Start is called before the first frame update
    void Start()
    {
        game_controller = GameObject.FindGameObjectWithTag("Hammer").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        
        if(transform.gameObject.name.Equals("mole_attack"))
        {
            if (other.tag == "Hammer")
            {
                game_controller.onHitMonster();
                game_controller.AddPlayerScore(10);
            }
        }
        
    }
}
