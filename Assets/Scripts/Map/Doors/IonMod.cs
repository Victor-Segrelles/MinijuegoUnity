using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonMod : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            player.GetComponent<PlayerShoot>().activateIon();
            Destroy(this.gameObject);
        }
    }
}
