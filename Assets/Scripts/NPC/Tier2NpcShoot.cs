using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier2NpcShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource m_shot;
    public AudioSource h_shot;
    [SerializeField] private Transform firetipA;
    [SerializeField] private Transform firetipB;
    [SerializeField] private Transform firetipHeavyA;
    [SerializeField] private Transform firetipHeavyB;
    public GameObject bullet;
    public GameObject heavybullet;
    public float bulletspeed=300.0f;
    public float fireRate=0.5f;
    private float rpm=0f;

    public float heavybulletspeed=200.0f;
    public float heavyfireRate=5f;
    private float heavyrpm=0f;


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
    private void ShootHeavyA(){
        //TODO que tire un proyectil
        GameObject bllt = Instantiate(heavybullet, firetipHeavyA.position, Quaternion.AngleAxis(90,Vector3.up)*firetipHeavyA.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(90,Vector3.up)*firetipHeavyA.forward*heavybulletspeed);
    }
    private void ShootHeavyB(){
        //TODO que tire un proyectil
        GameObject bllt = Instantiate(heavybullet, firetipHeavyB.position, Quaternion.AngleAxis(90,Vector3.up)*firetipHeavyB.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(90,Vector3.up)*firetipHeavyB.forward*heavybulletspeed);
    }
    public void ShootCheck(){
        if(Time.time>rpm){
            rpm=Time.time+fireRate;
            m_shot.Play();
            if(toFire==0){
                toFire=1;
                ShootA();
            } else {
                toFire=0;
                ShootB();
            }
        }
        if(Time.time>heavyrpm){
            heavyrpm=Time.time+heavyfireRate;
            h_shot.Play();
            ShootHeavyA();
            ShootHeavyB();
        }
    }
}
