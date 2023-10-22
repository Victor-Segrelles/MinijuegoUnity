using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardmentCore : MonoBehaviour
{
    private Transform player;
    public GameObject bomb;
    public float bombspeed=300.0f;
    public float fireRate=5f;
    private float rpm=0f;
    public float startH=40;
    public bool mentorRoom=false;
    void Start()
    {
        player=GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mentorRoom){
            ShootCheck();
        }
    }
    private void ShootCheck(){
        if(Time.time>rpm){
            rpm=Time.time+fireRate;
            shoot();
        }
    }
    private void shoot(){
        GameObject bllt = Instantiate(bomb, new Vector3(player.position.x,startH,player.position.z), Quaternion.Euler(180,0,0));
        bllt.GetComponent<Rigidbody>().AddForce(Vector3.down*bombspeed);
    }
}
