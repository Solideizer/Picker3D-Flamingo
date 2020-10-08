using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintY : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
       _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(transform.position.y > 0.3f)
        {
           _rigidbody.MovePosition(new Vector3(transform.position.x, 0.3f, transform.position.z));
        }
    }
}
