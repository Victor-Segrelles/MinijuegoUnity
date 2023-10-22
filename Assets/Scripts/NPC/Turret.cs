using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    [SerializeField] private Transform pylonRotation;
    [SerializeField] private GameObject player;
    public AudioSource h_shot;

    public GameObject bullet;
    public float bulletspeed=300.0f;
    public float fireRate=0.5f;
    private float rpm=0f;

    private float player_turret_dist;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_turret_dist=Vector3.Distance(player.transform.position,pylonRotation.position);
        //pylonRotation.LookAt(new Vector3(player.transform.position.x, pylonRotation.position.y,player.transform.position.z));
        
        //Debug.Log(player_turret_dist);
        if(player_turret_dist<10f){
            //rotating turret
            var lookPos=(new Vector3(player.transform.position.x, pylonRotation.position.y,player.transform.position.z))-pylonRotation.position;
            var rotation=Quaternion.LookRotation(lookPos);
            rotation *= Quaternion.Euler(0, -90, 0);
            pylonRotation.rotation=Quaternion.Slerp(pylonRotation.rotation, rotation, Time.deltaTime*3);

            //raycasting and firing
            RaycastHit hit;
            //Debug.DrawRay(pylonRotation.position, Quaternion.AngleAxis(90,Vector3.up)*pylonRotation.forward,Color.green);
            if (Physics.Raycast (pylonRotation.position, Quaternion.AngleAxis(90,Vector3.up)*pylonRotation.forward,out hit, 12f) && hit.transform.gameObject.tag == "Player") {
                ShootCheck();
            }
        }
    }
    void ShootCheck(){
        if(Time.time>rpm){
            rpm=Time.time+fireRate;
            h_shot.Play();
            Shoot();
        }
    }
    void Shoot(){
        //TODO que tire un proyectil
        GameObject bllt = Instantiate(bullet, pylonRotation.position+new Vector3(0,0.78f,0)+(Quaternion.AngleAxis(90,Vector3.up)*pylonRotation.forward*0.8f), Quaternion.AngleAxis(90,Vector3.up)*pylonRotation.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(90,Vector3.up)*pylonRotation.forward*bulletspeed);
    }
}
