using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier3NpcShoot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform firetipA;
    [SerializeField] private Transform firetipB;
    public AudioSource h_shot;
    public GameObject bullet;
    public float bulletspeed=300.0f;
    public float fireRate=2f;
    private float rpm=0f;


    private int toFire=0;
    // Start is called before the first frame update
    private void ShootA(){
        //TODO que tire un proyectil
        GameObject bllt = Instantiate(bullet, firetipA.position, Quaternion.AngleAxis(90,Vector3.up)*firetipA.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(90,Vector3.up)*firetipA.forward*bulletspeed);
    }
    private void ShootB(){
        //TODO que tire un proyectil

        GameObject bllt = Instantiate(bullet, firetipB.position, Quaternion.AngleAxis(90,Vector3.up)*firetipB.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(90,Vector3.up)*firetipB.forward*bulletspeed);
    }
    public void ShootCheck(){
        if(Time.time>rpm){
            rpm=Time.time+fireRate;
            h_shot.Play();
            if(toFire==0){
                toFire=1;
                ShootA();
            } else {
                toFire=0;
                ShootB();
            }
        }
    }
    public void spinup(){
        if(fireRate>0.3f){
            fireRate-=0.8f*Time.deltaTime;
        }
    }
    public void spindown(){
        if(fireRate<2f){
            fireRate+=0.8f*Time.deltaTime;
        }
    }
}
