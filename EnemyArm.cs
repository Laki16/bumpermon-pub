using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArm : MonoBehaviour {

    [Header("SYSTEM")]
    public float changedPosition;
    public GameObject player;
    public float zPosition;
    public float speed;
    private float movingTime;
	// Use this for initialization
	void Start () {
        //changedPosition = 0.0f;
        StartCoroutine(MoveArm());
	}
	
	// Update is called once per frame
	void Update () {
        speed = player.GetComponent<PlayerController>().speed;
        transform.Translate(new Vector3(0.1f + changedPosition, 0, 0)* speed * Time.deltaTime);
    }

    public IEnumerator MoveArm()
    {
        while(true)
        {
            movingTime = Random.Range(1, 2);
            changedPosition = 0.3f;
            yield return new WaitForSeconds(movingTime);
            changedPosition = -0.3f;
            yield return new WaitForSeconds(movingTime);
        }
    }
}
