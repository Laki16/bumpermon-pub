using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
	}
}
