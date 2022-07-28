using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public ObjectManager objectmanager;
	public GameManager gamemanager;
	Color C;
	public float Health;
	public float Twinkling_Time = 0;
	public string type;

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

	Vector2 Target()
	{
		return (Player.Instance.transform.position - transform.position).normalized;
	}

	void TargetingFire(string type, int way, float degree, float speed)
	{
	}

	void RandomFire(string type, float speed)
	{
	}

	void FixedFire(string type, int way, float degree, float speed, float dir)
	{
	}

	void Start()
	{
		C =  gameObject.GetComponent<SpriteRenderer>().color;
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