using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float Health = 1000f;
	
	void Damaged(float value)
	{
	    Health -= value;

		if (Health <= 0)
		{
			this.gameObject.SetActive(false);
		}
	}
	
	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag("PlayerBullet"))
		{
			PlayerBullet values = coll.gameObject.GetComponent<PlayerBullet>();
			Damaged(values.Damage);
			if (!values.isPenetrate)
      {
				coll.gameObject.SetActive(false);
			}
		}
	}
}