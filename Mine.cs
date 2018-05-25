using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public CameraShake cameraShake;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(cameraShake.Shake(.1f, .1f));
        //if (other.gameObject.GetComponent<PlayerController>().useNitro)
        //{
        //    StartCoroutine(cameraShake.Shake(.1f, .1f));
        //}
        //else
        //{
        //    StartCoroutine(cameraShake.Shake(.1f, .1f));
        //    //other.gameObject.GetComponent<PlayerController>().live--;
        //}
        Destroy(gameObject);
    }
}
