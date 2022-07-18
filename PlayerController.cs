using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public GameObject P1MainWeapon;
	public float Gamespeed = 1f;
	public float Movespeed = 5f;
	public Vector2 L_B_Pos = new Vector2(-0.1f,1f);
	public Vector2 R_B_Pos = new Vector2(0.1f,1f);

	
    void Start()
    {
	    // gameObject.SetActive(true);
		transform.position = new Vector2(0f , -4f);
    }


    void Update()
    {
	// 이동
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
			transform.Translate(new Vector2(moveX,moveY) * 0.5f * Movespeed * Time.deltaTime * Gamespeed);
		}
		else
		{
			transform.Translate(new Vector2(moveX,moveY) * Movespeed * Time.deltaTime * Gamespeed );
		}
	
	// 공격
		if (Input.GetKey(KeyCode.Z))
		{
			GameObject Left_Bullet = Instantiate(P1MainWeapon);
			GameObject Right_Bullet = Instantiate(P1MainWeapon);
			Left_Bullet.transform.position = Right_Bullet.transform.position = transform.position;
			Left_Bullet.transform.Translate(L_B_Pos);
			Right_Bullet.transform.Translate(R_B_Pos);
	        Left_Bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
			Right_Bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
		}


	// 폭탄 사용
	
	
    }
}
