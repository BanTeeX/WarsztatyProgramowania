using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    private float gravity = 10f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float turnSpeed = 90f;
    [SerializeField] private float lerpSpeed = 10f;
    private Vector3 surfaceNormal;
    private Vector3 thisNormal;

    public BoxCollider boxCollider; 
    private Rigidbody thisRigidbody;

    private void Start()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        thisNormal = transform.up;
        thisRigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        thisRigidbody.AddForce(-gravity * thisRigidbody.mass * thisNormal);
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, -thisNormal);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            surfaceNormal = hit.normal;
        }
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        thisNormal = Vector3.Lerp(thisNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
        Vector3 myForward = Vector3.Cross(transform.right, thisNormal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, thisNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
    }
}
