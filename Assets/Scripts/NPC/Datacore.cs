using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Datacore : MonoBehaviour, IDamageable
{
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;
    [SerializeField] private Mentor mentor;
    private bool isNeutralized=false;
    private float hp=40;
    private float MAXHP=40;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image inmunityBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTakeDamage(float Damage, bool isIon){
        if(isNeutralized){
            if(hp-Damage<=0){
                hp=0;
                mentor.loseDatacore();
                
                m_ExplosionParticles.transform.parent = null;
                m_ExplosionParticles.Play();
                m_ExplosionAudio.Play();
                Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

                Destroy(this.gameObject);
            } else {
                hp-=Damage;
            }
            healthBar.fillAmount=hp/MAXHP;
        }
    }
    public void neutralized(){
        isNeutralized=true;
        inmunityBar.fillAmount=0;
        Invoke("repair", 10f);
    }
    private void repair(){
        isNeutralized=false;
        inmunityBar.fillAmount=1;
    }
}
