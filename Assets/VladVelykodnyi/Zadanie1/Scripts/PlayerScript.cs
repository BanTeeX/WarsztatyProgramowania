using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float gravity = 10f;

    private Vector3 thisNormal;
    private Rigidbody thisRigidbody;
    private BoxCollider thisCollider;

    private float distanceToGround;
    private bool isGrounded;
    private Vector3 thisForward;
    void Start()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        thisCollider = gameObject.GetComponent<BoxCollider>();
        thisNormal = transform.up;
        thisForward = Vector3.Cross(transform.right, thisNormal);

        distanceToGround = thisCollider.bounds.extents.y - thisCollider.bounds.center.y;
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int playerLayer = LayerMask.GetMask("Player");
        if (Physics.Raycast(ray, out hit, distanceToGround * 2))
        {
            ChangeWall();
        }
        if (isGrounded) thisRigidbody.AddForce(gravity * thisRigidbody.mass * -thisNormal);
    }

    private void ChangeWall()
    {
        thisForward = Vector3.Cross(transform.right, thisNormal);
    }
}
