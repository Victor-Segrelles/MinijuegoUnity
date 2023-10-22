using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _camara;
    public Transform personaje;
    public int speed=50;
    // Update is called once per frame
    void FixedUpdate()
    {
        _camara.transform.position=personaje.position;
    }
    void Update(){
        if(Input.GetKey(KeyCode.Q)){
            transform.Rotate(-Vector3.up*speed*Time.deltaTime, Space.World);
        }
        if(Input.GetKey(KeyCode.E)){
            transform.Rotate(Vector3.up*speed*Time.deltaTime, Space.World);
        }
    }
}
