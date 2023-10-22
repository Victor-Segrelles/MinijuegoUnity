using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BombDrone : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    private UnityEngine.AI.NavMeshPath path;
    public Transform player;
    public HealthBars hps;
    public float damage=10f;
    private void Start(){
        path=new UnityEngine.AI.NavMeshPath();
        hps=GameObject.FindWithTag("healthCore").GetComponent<HealthBars>();
        player=GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.position);
    }
    public void OnTakeDamage(float Damage, bool isIon){
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();
        Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Destroy(this.gameObject);
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
            m_ExplosionAudio.Play();
            Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
            
            Destroy(this.gameObject);
        }
    }
}
