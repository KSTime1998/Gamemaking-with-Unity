using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIichan : Player
{
	public GameObject Weapon;
	public GameObject BombWeapon;
	float Cooltime = 0f;
	float BombCooltime = 0f;
	byte BombBulletNum = 0;

	// 생성 위치
	Vector2 L_B_Pos = new Vector2(-0.2f,0.1f);
	Vector2 R_B_Pos = new Vector2(0.2f,0.1f);

	// 탄 스피드 / 폭탄 스피드
	public short BulletSpeed = 1000;
	public short BombSpeed = 2000;


	void Start()
	{
		//this.gameObject.SetActive(false);
		transform.position = new Vector2(0f , -4f);

		// 이하 캐릭터별 스테이터스
		Movespeed = 7f;
		SlowMovespeed = 4f;
		StartBomb = 3;
		Life = 2;
		Bomb = StartBomb;
	}


	void FixedUpdate()
	{
		this.gameObject.GetComponent<Player>().InvincibleEffect();
		this.gameObject.GetComponent<Player>().Move();
		// 공격 쿨타임
		if (Cooltime > 0f)
		{
			Cooltime -= Time.deltaTime;
		}

		// 공격
		if (Input.GetKey(KeyCode.Z) & Cooltime <= 0f)
		{
			Cooltime = 0.1f / Gamespeed;
			GameObject Left_Bullet = Instantiate(Weapon);
			GameObject Right_Bullet = Instantiate(Weapon);
			Left_Bullet.transform.position = Right_Bullet.transform.position = transform.position;
			Left_Bullet.transform.Translate(L_B_Pos);
			Right_Bullet.transform.Translate(R_B_Pos);
			Left_Bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BulletSpeed * Gamespeed);
			Right_Bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BulletSpeed * Gamespeed);
		}

		// 폭탄 사용
		if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0)
		{
			this.gameObject.layer = 6;
			Invincible = 5f;
			Bomb--;
			BombBulletNum = 100;
		}

		// 폭탄 발사 트리거
		if (BombCooltime > 0f)
		{
			BombCooltime -= Time.deltaTime;
		}
		else if (BombBulletNum > 0)
		{
			BombCooltime = 0.02f / Gamespeed ;
			BombBulletNum--;
			Vector2 pos = new Vector2(Mathf.Sin(BombBulletNum * Mathf.PI / 25) , Mathf.Cos(BombBulletNum * Mathf.PI / 25));
			GameObject VIBomb = Instantiate(BombWeapon);
			VIBomb.transform.position = transform.position;
			VIBomb.transform.Translate(pos);
			VIBomb.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BombSpeed * Gamespeed);
		}
	}
}
