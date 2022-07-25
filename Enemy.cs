using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameManager
{
	public GameObject player;
	Color C;
	public float Health;
	public float Twinkling_Time = 0;

	void Start()
	{
		C =  gameObject.GetComponent<SpriteRenderer>().color;
	}

	void Damaged(float value)
	{
	  Health -= value;
		if (Health <= 0)
		{
			this.gameObject.SetActive(false);
		}
		if (Twinkling_Time == 0 & value != 0)
		{
			Twinkling_Time = 5;
			Twinkling();
		}
	}

  void Twinkling()
  {
		C.a = 1 - (Twinkling_Time) % 2 ;
    gameObject.GetComponent<SpriteRenderer>().color = C;
	}
	
	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag("PlayerBullet"))
		{
			PlayerBullet values = coll.gameObject.GetComponent<PlayerBullet>();
			coll.gameObject.GetComponent<PlayerBullet>().Effect(transform.position);
			Damaged(values.Damage);
			if (!values.isPenetrate)
      {
				coll.gameObject.SetActive(false);
			}
		}
	}

	public Vector3 Aming()
	{
		Vector3 dir = (this.gameObject.GetComponent<GameManager>().Player_Pos - transform.position).normalized;
		return dir;
	}

	void FixedUpdate()
	{
		if (Twinkling_Time > 0)
		{
			Twinkling_Time--;
			Twinkling();
		}
	}
}