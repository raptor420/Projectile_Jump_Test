using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPod : MonoBehaviour
{
    [SerializeField]  public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(target.position, new Vector3(6, 2.5f, 6));

    }
}
