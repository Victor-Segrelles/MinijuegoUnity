using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcAIT3 : MonoBehaviour, IDamageable
{
    private enum State{
        Roaming,
        Chasing,
        Returning,
    }
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;

    [SerializeField] private Transform weaponrotation;
    [SerializeField] private Transform weaponrotationB;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    private UnityEngine.AI.NavMeshPath path;
    private Vector3 startingPos;
    private Vector3 roamPosition;
    private State state;
    public Transform player;
    public float targetRange=12f;
    public float attackRange=8f;
    public float engagementRange=16f;
    private Vector3 target;
    private Tier3NpcShoot shooter;

    //
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;
    [SerializeField] private Image phaseBar;

    private float hp=130;
    private float MAXHP=130;
    private float shield=80;
    private float MAXSHIELD=80;
    private float phaseshield=24;
    private float MAXSPHASEHIELD=24;//escudo de fase: inmunidad al daño convencional, solo ionico
    private float shieldtimer=0f;//marca de tiempo
    private float shieldDelay=5f;//retraso de regeneracion despues de que el escudo sufra un golpe
    private float shieldRegenRate=2.5f;//ritmo de regeneración estandar
    private float shieldRegen=15f;//cantidad de regeneracion

    //
    private void Start(){
        shooter = GetComponent<Tier3NpcShoot>();
        path=new UnityEngine.AI.NavMeshPath();
        state=State.Roaming;
        startingPos=transform.position;
        target=startingPos;
        player=GameObject.FindWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("RegenShields", 0.0f, shieldRegenRate);
    }
    private void Update(){
        switch(state){
            default:
            case State.Roaming:
                transform.rotation=Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime*15);
                FindTarget();
                break;
            case State.Chasing:
                //aim constantly at player(aka rotate towards)
                if(Vector3.Distance(transform.position, player.position)<attackRange){
                    navMeshAgent.SetDestination(transform.position);
                    //
                    var lookPos=(new Vector3(player.position.x, weaponrotation.position.y,player.position.z))-(new Vector3(weaponrotation.position.x,weaponrotation.position.y,weaponrotation.position.z));
                    var rotation=Quaternion.LookRotation(lookPos);
                    //rotation *= Quaternion.Euler(0, 0, 0);
                    weaponrotation.rotation=rotation;
                    lookPos=(new Vector3(player.position.x, weaponrotationB.position.y,player.position.z))-(new Vector3(weaponrotationB.position.x,weaponrotationB.position.y,weaponrotationB.position.z));
                    rotation=Quaternion.LookRotation(lookPos);
                    //rotation *= Quaternion.Euler(0, 0, 0);
                    weaponrotationB.rotation=rotation;
                    //
                    shooter.ShootCheck();
                    shooter.spinup();

                    
                } else {
                    if(UnityEngine.AI.NavMesh.CalculatePath(transform.position, player.position, UnityEngine.AI.NavMesh.AllAreas, path)){
                        navMeshAgent.SetDestination(player.position);
                    }
                    shooter.spindown();
                }
                target=player.position;
                if(Vector3.Distance(player.position, startingPos)>engagementRange){
                    state=State.Returning;
                }
                AimAt();
                break;
            case State.Returning:
                navMeshAgent.SetDestination(startingPos);
                target=startingPos;
                if(Vector3.Distance(transform.position, startingPos)>0.2f){
                    state=State.Roaming;
                }
                AimAt();
                break;
        }
        
    }

    private Vector3 Roaming(){
        return startingPos+randomDir()*Random.Range(0.6f,2.5f);
        
    }
    private Vector3 randomDir(){
        return new Vector3(UnityEngine.Random.Range(-1f,1f), 0f,UnityEngine.Random.Range(-1f,1f)).normalized;
    }
    private void FindTarget(){
        if(Vector3.Distance(transform.position, player.position)<targetRange){
            state=State.Chasing;
        }
    }
    private void AimAt(){
        var lookPos=(new Vector3(target.x, transform.position.y,target.z))-transform.position;
        var rotation=Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(0, 0, 0);
        transform.rotation=Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*30);
        //rb.MoveRotation(rotation);
    }
    public void OnTakeDamage(float Damage, bool isIon){
        if(phaseshield<=0){
            if(shield>0 && !isIon){
                if(shield-Damage<0){
                    Damage-=shield;
                    shield=0;
                } else {
                    shield-=Damage;
                    Damage=0;
                }
            }
            if(hp-Damage<=0){
                hp=0;

                m_ExplosionParticles.transform.parent = null;
                m_ExplosionParticles.Play();
                m_ExplosionAudio.Play();
                Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

                Destroy(this.gameObject);
            } else {
                hp-=Damage;
            }
        } else {
            if(isIon){
                if(phaseshield-Damage<=0){
                    phaseshield=0;
                } else {
                    phaseshield-=Damage;
                }
                phaseBar.fillAmount=phaseshield/MAXSPHASEHIELD;
            }
        }

            //cuando recibe el daño
        shieldtimer=Time.time;
        healthBar.fillAmount=hp/MAXHP;
        shieldBar.fillAmount=shield/MAXSHIELD;
    }
    private void RegenShields(){
        if(shieldtimer+shieldDelay<Time.time && shield<MAXSHIELD){
            if(shield+shieldRegen>=MAXSHIELD){
                shield=MAXSHIELD;
            } else {
                shield+=shieldRegen;
            }
            shieldBar.fillAmount=shield/MAXSHIELD;
        }
    }
    
}
