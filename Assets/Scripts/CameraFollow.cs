using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        offset = this.transform.position - target.position ;

    }
    private void Update()
    {
        if(transform!=null)
            transform.position = target.transform.position + offset;
    }
}
