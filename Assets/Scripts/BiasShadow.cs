using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class BiasShadow : MonoBehaviour
{
    private float setBias = -0.1f;
 
     // Update is called once per frame
     void Update () {
         GetComponent<Light>().shadowBias = setBias;
     }
}
