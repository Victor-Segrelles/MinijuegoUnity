using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private GameObject textmsg;
    private float state_closed;
    private float state_open;
    private float current_y; 
    public float speed;
    private int opening;
    // Start is called before the first frame update
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.F)){
                if(door.position.z>=state_open){
                    opening=-1;
                } else if (door.position.z<=state_closed){
                    opening=1;
                }
            }
        }
    }
    void OnTriggerEnter(Collider collision){
        if (collision.gameObject.CompareTag("Player")){
            textmsg.SetActive(true);
        }
    }
    void OnTriggerExit(Collider collision){
        if (collision.gameObject.CompareTag("Player")){
            textmsg.SetActive(false);
        }
    }
    void Start()
    {
         state_closed = door.position.z;
         state_open = state_closed + 1.7f;
         opening=0;
    }
    void Update(){
        if(opening==1 && door.position.z<state_open){
            door.Translate(Vector3.forward*Time.deltaTime*speed);
        }
        if(opening==-1 && door.position.z>state_closed){
            door.Translate(-Vector3.forward*Time.deltaTime*speed);
        }
        
        
    }
}
