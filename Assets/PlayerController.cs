using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform cam;
    // Start is called before the first frame update

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }




    private void Update()
    {
        var h = Input.GetAxis("Horizontal");


        transform.position += h * 10 * Time.deltaTime * cam.transform.right;
    }

    private void OnTriggerEnter(Collider other)
    {
        var jumppod = other.GetComponent<jumpPod>();
        var dist = jumppod.target.position - transform.position;
        dist.y = 0;
        rb.velocity = Vector3.zero;
        rb.velocity = RequiredInitialVelcoity(transform.position,jumppod.target.position,3);
        var target = jumppod.target.transform.position;
        target.y = 0;
        var selfPos = transform.position;
        selfPos.y = 0;
        transform.rotation = Quaternion.LookRotation(target - selfPos);
    }


    Vector3 RequiredInitialVelcoity(Vector3 throwingpoint, Vector3 target,float time)
    {

        Vector3 distance = target - throwingpoint;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
       //  Sy =3;
        float Sxz = distanceXZ.magnitude;
        float Vxz = Sxz / time;
        float Vy = (Sy / time + .5f * Mathf.Abs(Physics.gravity.y) * time);
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;


    }
}
