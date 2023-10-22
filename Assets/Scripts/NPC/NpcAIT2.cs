using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcAIT2 : MonoBehaviour, IDamageable
{
    private enum State{
        Roaming,
        Chasing,
        Returning,
    }
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;

    [SerializeField] private Transform weaponrotation;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    private UnityEngine.AI.NavMeshPath path;
    private Vector3 startingPos;
    private Vector3 roamPosition;
    private State state;
    public Transform player;
    public float targetRange=6f;
    public float attackRange=3f;
    public float engagementRange=16f;
    private Vector3 target;
    private Tier2NpcShoot shooter;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;

    private float hp=30;
    private float MAXHP=30;
    private float shield=25;
    private float MAXSHIELD=25;
    private float shieldtimer=0f;//marca de tiempo
    private float shieldDelay=10f;//retraso de regeneracion despues de que el escudo sufra un golpe
    private float shieldRegenRate=5f;//ritmo de regeneración estandar
    private float shieldRegen=10f;//cantidad de regeneracion


    private void Start(){
        shooter = GetComponent<Tier2NpcShoot>();
        state=State.Roaming;
        hp=MAXHP;
        path=new UnityEngine.AI.NavMeshPath();
        startingPos=transform.position;
        roamPosition=Roaming();
        target=roamPosition;
        InvokeRepeating("RegenShields", 0.0f, shieldRegenRate);
    }
    private void Update(){
        switch(state){
            default:
            case State.Roaming:
                if(UnityEngine.AI.NavMesh.CalculatePath(transform.position, roamPosition, UnityEngine.AI.NavMesh.AllAreas, path)){
                    navMeshAgent.SetDestination(roamPosition);
                    target=roamPosition;
                    if(Vector3.Distance(transform.position, roamPosition)<0.3f){
                        roamPosition=Roaming();
                    }
                } else {
                    roamPosition=Roaming();
                }
                
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
                    //
                    shooter.ShootCheck();

                    
                } else {
                    if(UnityEngine.AI.NavMesh.CalculatePath(transform.position, player.position, UnityEngine.AI.NavMesh.AllAreas, path)){
                        navMeshAgent.SetDestination(player.position);
                    }
                }
                target=player.position;
                if(Vector3.Distance(player.position, startingPos)>engagementRange){
                    state=State.Returning;
                }
                break;
            case State.Returning:
                navMeshAgent.SetDestination(startingPos);
                if(Vector3.Distance(transform.position, startingPos)>0.2f){
                    state=State.Roaming;
                }
                break;
        }
        AimAt();
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
        rotation *= Quaternion.Euler(0, -90, 0);
        transform.rotation=Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*30);
        //rb.MoveRotation(rotation);
    }
    public void OnTakeDamage(float Damage, bool isIon){

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
