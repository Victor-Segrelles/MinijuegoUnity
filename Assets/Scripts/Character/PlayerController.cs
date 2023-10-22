using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _input;
    public Vector3 _mouseScreenP;
    public Vector3 _worldpointer;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5;

    [SerializeField] private Transform camera_rotation;

    //intento control de velocidad propia
    private Vector3 trackVelocity;
    private Vector3 lastPos;

    //targeting colliders
    [SerializeField] private Collider coll;
    void Update(){
        _mouseScreenP=Input.mousePosition;
        RaycastHit hit;
        var ray =Camera.main.ScreenPointToRay(_mouseScreenP);
        if(coll.Raycast(ray, out hit, 1000000f)){
            _worldpointer=hit.point;
            _worldpointer.y=_rb.position.y;
        }
    }
    void FixedUpdate(){
        GatherInput();
        Quaternion target=Quaternion.LookRotation(_worldpointer-_rb.position);
        _rb.MoveRotation(target);
        //ShootCheck();
        GravityIsPain();
    }

    void GatherInput(){
        _input=new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        AnimationProcessorV2();
        _input=Vector3.ClampMagnitude(_input, 1);
        _input=Quaternion.Euler(0,camera_rotation.eulerAngles.y,0)*_input;
        
        Vector3 movement=_input*_speed;
        Vector3 targetPos=_rb.position+movement*Time.fixedDeltaTime;
        _rb.MovePosition(targetPos);
    }
    void AnimationProcessorV2(){

        var normtrack=Quaternion.Euler(0,camera_rotation.eulerAngles.y,0)*_input;

        normtrack=transform.InverseTransformDirection(normtrack);
        normtrack=Vector3.Normalize(normtrack);
        //Debug.Log();
        var tolerador=0.55f;
        if(normtrack.x<tolerador && normtrack.x>-tolerador){
            normtrack.x=0.0f;
        }
        if(normtrack.z<tolerador && normtrack.z>-tolerador){
            normtrack.z=0.0f;
        }
        _animator.SetFloat("InputX", normtrack.x);
        //Debug.Log(_rb.rotation * Vector3.forward);
        _animator.SetFloat("InputY", normtrack.z);
    }
    bool IsGrounded(){
        bool hitrampa=Physics.Raycast(_rb.position, Vector3.down, 0.25f);
        if(hitrampa){
            return true;
        }
        return false;
    }
    void GravityIsPain(){
        if(IsGrounded()){
            _rb.useGravity=false;
        } else {
            _rb.useGravity=true;
        }
    }
}
