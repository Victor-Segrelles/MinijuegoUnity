using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mentorRoom : MonoBehaviour
{
    private BombardmentCore bombcontroller;
    [SerializeField] GameObject forcefield;
    void Start()
    {
        bombcontroller=GameObject.FindWithTag("GameController").GetComponent<BombardmentCore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            forcefield.SetActive(true);
            bombcontroller.mentorRoom=true;
        }
    }
}
