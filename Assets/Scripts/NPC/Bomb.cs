using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;
    [SerializeField] private GameObject fallMarkerPrefab = null;
    private GameObject fallMarkerInstance = null;
    private float ground=3.31f;
    public HealthBars hps;
    public float damage=25f;
    private void Start () {
        hps=GameObject.FindWithTag("healthCore").GetComponent<HealthBars>();
        if (fallMarkerPrefab != null) {
                fallMarkerInstance = Instantiate(fallMarkerPrefab, new Vector3(transform.position.x, ground, transform.position.z), Quaternion.Euler(90,0,0)); //  z-fighting ->proyectar sobre el sulo
        }
    }
    private void OnDestroy () {
         if (fallMarkerInstance != null) { Destroy(fallMarkerInstance); }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="datacore"){
            Datacore victim=col.gameObject.GetComponent<Datacore>();
            victim.neutralized();

            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

            Destroy(this.gameObject);
        } else if (col.gameObject.tag =="Player"){
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
        } else {
            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

            Destroy(this.gameObject);
        }
    }
}
