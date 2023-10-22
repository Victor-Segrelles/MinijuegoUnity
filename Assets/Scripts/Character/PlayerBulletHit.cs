using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHit : MonoBehaviour
{
    public float damage=8f;
    public bool ion=false;

    public ParticleSystem m_ExplosionParticles;       

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="enemy" || col.gameObject.tag=="datacore"){
            IDamageable victim=col.gameObject.GetComponent<IDamageable>();
            victim.OnTakeDamage(damage,ion);

            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

            Destroy(this.gameObject);
        } else if(col.gameObject.tag !="player"){

            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

            Destroy(this.gameObject);
        }
    }
}
