using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintY : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(transform.position.y > 0.3f)
        {
           rb.MovePosition(new Vector3(transform.position.x, 0.3f, transform.position.z));
        }
    }
}
