using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    // Update is called once per frame
    public AudioSource m_shot;
    public HealthBars hps;
    public GameObject bullet;
    public GameObject ionbullet;
    public float bulletspeed=300.0f;
    public float ionbulletspeed=500.0f;
    public float fireRate=0.7f;
    private float rpm=0f;
    [SerializeField]
    private Transform rifle;
    private bool ion=false;
    void Start()
    {
        hps=GameObject.FindWithTag("healthCore").GetComponent<HealthBars>();
    }
    void FixedUpdate()
    {
        ShootCheck();
    }
    void ShootCheck(){
        if(Input.GetButton("Fire1") && Time.time>rpm){
            rpm=Time.time+fireRate;
            hps.shottedtime=Time.time;
            hps.fireRate=fireRate;
            hps.changeColor(new Color32(24,219,219,255));
            m_shot.Play();
            Shoot();
        } else if (Input.GetButton("Fire2") && Time.time>rpm && ion){
            rpm=Time.time+fireRate*3;
            hps.shottedtime=Time.time;
            hps.fireRate=fireRate*3;
            hps.changeColor(new Color32(219,138,24,255));
            m_shot.Play();
            ShootIon();
        }

    }
    void Shoot(){
        GameObject bllt = Instantiate(bullet, rifle.position, transform.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(transform.forward*bulletspeed);
    }
    void ShootIon(){
        GameObject bllt = Instantiate(ionbullet, rifle.position, transform.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(transform.forward*ionbulletspeed);
    }
    public void activateIon(){
        ion=true;
    }
}
