using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    GameObject groundController;

    [Header("Status")]
    [Range(1,100)]
    public float speed;
    private float groundCount;

	void Start () {
        speed = 25.0f;
        groundController = GameObject.FindGameObjectWithTag("GroundController");
        groundCount = 0.0f;
	}

	void Update ()
    {
        transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed);
        if(transform.position.x - groundCount >= 0)
        {
            groundCount += SpawnGrounds.groundXSize * 10;
            groundController.GetComponent<SpawnGrounds>().SpawnGround();
        }

    }


}
