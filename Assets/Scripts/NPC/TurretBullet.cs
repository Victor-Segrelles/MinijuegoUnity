using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;       
    public HealthBars hps;
    public float damage=8f;
    void Start()
    {
        hps=GameObject.FindWithTag("healthCore").GetComponent<HealthBars>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag =="Player"){
            if(hps.shield>0){
                if(hps.shield-damage<0){
                    damage-=hps.shield;
                    hps.shield=0;
                } else {
                    hps.shield-=damage;
                    damage=0;
                }
            }
            if(hps.health-damage<=0){
                hps.health=0;
            } else {
                hps.health-=damage;
            }

            //cuando recibe el daño
            hps.shieldtimer=Time.time;

            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

            Destroy(this.gameObject);
        } else {

            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

            Destroy(this.gameObject);
        }
    }
}
