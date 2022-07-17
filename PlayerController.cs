using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
	    gameObject.SetActive(true);
		transform.position = new Vector2(0 , -4);
    }

	public float movespeed = 5f;

    void Update()
    {
		float moveX = 0f;
		float moveY = 0f;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			moveY += 1f;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			moveY -= 1f;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			moveX += 1f;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			moveX -= 1f;
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
		transform.Translate(new Vector2(moveX,moveY) * 0.5f * movespeed * Time.deltaTime) ;
		}
		else
		{
		transform.Translate(new Vector2(moveX,moveY) * movespeed * Time.deltaTime ) ;
		}
    }
}