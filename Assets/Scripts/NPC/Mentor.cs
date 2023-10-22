using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mentor : MonoBehaviour, IDamageable
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image inmunityBar;
    [SerializeField] private Transform bombspawner1;
    [SerializeField] private Transform bombspawner2;
    [SerializeField] private GameObject[] ultimaDefenses;
    [SerializeField] private GameObject[] emergency_light;
    private bool lightsync=false;
    public GameObject bombdrone;
    private BombardmentCore bombcontroller;

    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;
    public AudioSource m_EmergencyAudio;

    private float hp=350;
    private float MAXHP=350;
    private float datacores=4;
    private float MAXDATACORES=4;
    // Start is called before the first frame update
    void Start()
    {
        bombcontroller=GameObject.FindWithTag("GameController").GetComponent<BombardmentCore>();
        foreach (GameObject drone in ultimaDefenses)
        {
            drone.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTakeDamage(float Damage, bool isIon){
        if(datacores==0){
            if(hp-Damage<=0){
                hp=0;

                m_ExplosionParticles.transform.parent = null;
                m_ExplosionParticles.Play();
                m_ExplosionAudio.Play();
                Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

                Invoke("victory", 4f);
            } else {
                hp-=Damage;
            }
        }
        healthBar.fillAmount=hp/MAXHP;
        inmunityBar.fillAmount=datacores/MAXDATACORES;
    }
    public void loseDatacore(){
        datacores-=1;
        inmunityBar.fillAmount=datacores/MAXDATACORES;
        if(datacores==3){

            InvokeRepeating("bombspawnA", 0.0f, 17f);
        }
        if(datacores==2){
            bombcontroller.fireRate=3f;
        }
        if (datacores==1){
            InvokeRepeating("bombspawnB", 0.0f, 17f);
        }
        if (datacores==0){
            bombcontroller.fireRate=2f;
            foreach (GameObject drone in ultimaDefenses)
            {
                drone.SetActive(true);
            }
            InvokeRepeating("Emergency", 0.0f, 0.5f);
            m_EmergencyAudio.Play();
        }

    }
    public void victory(){
        SceneManager.LoadScene("Victory");
    }
    private void bombspawnA(){
        Instantiate(bombdrone, bombspawner1);
    }
    private void bombspawnB(){
        Instantiate(bombdrone, bombspawner2);
    }
    private void Emergency(){
        foreach (GameObject emer in emergency_light)
            {
                emer.SetActive(lightsync);
            }
        lightsync=!lightsync;
    }
}
