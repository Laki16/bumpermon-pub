using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Control")]
	GameObject groundController;
    GameObject blockController;
	private Vector2 initialPos;
	private int lane;
    public float swipeSpeed = 10.0f;
    private bool isKeyPressed = false;

    [Header("Status")]
    [Range(1,300)]
    public float speed;
    private float groundCount;

	void Start()
	{
		lane = 0;
        
        groundController = GameObject.FindGameObjectWithTag("GroundController");
        blockController = GameObject.FindGameObjectWithTag("BlockController");
        groundCount = 0.0f;
	}

	void Update ()
    {
        transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed);
        //spawnGround
        if(transform.position.x - groundCount >= 0)
        {
            groundCount += SpawnGrounds.groundXSize * 10;
            groundController.GetComponent<SpawnGrounds>().SpawnGround();
		}

        //--------------- mouse control ---------------
        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Calculate(Input.mousePosition);
        }
        //---------------------------------------------
        //--------------- keyboard control ------------
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isKeyPressed)
        {
            isKeyPressed = true;
            if (transform.position.z < 1)
            {
                //Debug.Log("Left");
                lane++;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && isKeyPressed)
        {
            isKeyPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !isKeyPressed)
        {
            isKeyPressed = true;
            if (transform.position.z > -1)
            {
                //Debug.Log("Right");
                lane--;
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && isKeyPressed)
        {
            isKeyPressed = false;
        }
        //---------------------------------------------
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
                        //Debug.Log("Left");
                        lane++;
                    }
				}
				else
				{
                    if (transform.position.z > -1)
                    {
                        //Debug.Log("Right");
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

    // 기어시스템 & 속도증가

    
    void IncreaseSpeed(int gear)
    {

    }
}
