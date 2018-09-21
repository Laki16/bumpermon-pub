using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public CameraShake cameraShake;
    public GameObject boomFX;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerController>().ghostMode)
        {
            StartCoroutine(SplitMesh(true));
        }
    }

    public IEnumerator SplitMesh(bool destroy)
    {
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }

        boomFX.SetActive(true);
        GetComponent<SkinnedMeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.2f);
        if (destroy == true)
        {
            boomFX.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
