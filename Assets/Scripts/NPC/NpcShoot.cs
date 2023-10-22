using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcShoot : MonoBehaviour
{
    [SerializeField] private Transform firetip;
    public AudioSource m_shot;
    public GameObject bullet;
    public float bulletspeed=300.0f;
    public float fireRate=0.5f;
    private float rpm=0f;
    // Start is called before the first frame update
    private void Shoot(){
        //TODO que tire un proyectil
        GameObject bllt = Instantiate(bullet, firetip.position, Quaternion.AngleAxis(90,Vector3.up)*transform.rotation);
        bllt.GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(90,Vector3.up)*transform.forward*bulletspeed);
    }
    public void ShootCheck(){
        if(Time.time>rpm){
            rpm=Time.time+fireRate;
            m_shot.Play();
            Shoot();
        }
    }
}
