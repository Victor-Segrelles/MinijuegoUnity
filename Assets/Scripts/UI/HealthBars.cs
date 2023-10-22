using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public float MAX_HEALTH;
    public float MAX_SHIELD;
    public float shield;
    public float health;
    public float shieldtimer;

    public float fireRate=0;
    public float shottedtime=0;

    [SerializeField] private Image healthBar;

    [SerializeField] private Image shieldBar;

    [SerializeField] private Image reload;
    // Start is called before the first frame update
    void Start()
    {
        MAX_HEALTH=100f;
        MAX_SHIELD=20f;
        shield=MAX_SHIELD;
        health= MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount=health/MAX_HEALTH;
        shieldBar.fillAmount=shield/MAX_SHIELD;
        if(Time.time-shottedtime>fireRate+shottedtime){
            reload.fillAmount=1;
        } else {
            reload.fillAmount=(Time.time-shottedtime)/(fireRate);
        }
    }
    public void changeColor(Color32 color){
        reload.color=color;
    }
}
