using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthball : SimpleProjectile
{
    public GameObject earth_pillar;
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Earthball collisiton");
        Instantiate(earth_pillar, new Vector3(transform.position.x, 0, transform.position.y), Quaternion.Euler(0, 0, 0));
        onDestroy();
    }
}
