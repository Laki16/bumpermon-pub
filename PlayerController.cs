using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Control")]
	GameObject groundController;
	private Vector2 initialPos;
	private int lane;
    public float swipeSpeed = 10.0f;

    [Header("Status")]
    [Range(1,100)]
    public float speed;
    private float groundCount;

	void Start()
	{
		lane = 0;

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
		if (Input.GetMouseButtonDown(0))
		{
			initialPos = Input.mousePosition;
		}
        if (Input.GetMouseButtonUp(0))
        {
            Calculate(Input.mousePosition);
        }
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x, transform.position.y, lane) , Time.deltaTime * swipeSpeed);
    }

	void Calculate(Vector3 finalPos)
	{
		float disX = Mathf.Abs(initialPos.x - finalPos.x);
		float disY = Mathf.Abs(initialPos.y - finalPos.y);
		if (disX > 0 || disY > 0)
		{
			if (disX > disY)
			{
				if (initialPos.x > finalPos.x)
				{
                    if (transform.position.z < 1)
                    {
                        Debug.Log("Left");
                        lane++;
                    }
				}
				else
				{
                    if (transform.position.z > -1)
                    {
                        Debug.Log("Right");
                        lane--;
                    }
				}
			}
			else
			{
				if (initialPos.y > finalPos.y)
				{
					Debug.Log("Down");
				}
				else
				{
					Debug.Log("Up");
				}
			}
		}
	}
}
