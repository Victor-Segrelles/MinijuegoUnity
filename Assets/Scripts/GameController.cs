using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public HealthBars hps;
    public PlayerController player;

    //player
    private float shieldTick=3f;
    private float shieldRegen=2f;
    private float shieldDelay=15f;
    // Start is called before the first frame update
    void Start()
    {
        hps=GameObject.FindWithTag("healthCore").GetComponent<HealthBars>();
        InvokeRepeating("ShieldManagement", 0.0f, shieldTick);
        InvokeRepeating("defeat", 0.0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void ShieldManagement(){
        
        if (Time.time>hps.shieldtimer+shieldDelay && hps.shield<hps.MAX_SHIELD) {

            if(hps.shield+shieldRegen>=hps.MAX_SHIELD){
                hps.shield=hps.MAX_SHIELD;
            } else {
                hps.shield+=shieldRegen;
            }
        }
    }
    void defeat(){
        if(hps.health<=0){
            SceneManager.LoadScene("Defeat");
        }
    }

}
