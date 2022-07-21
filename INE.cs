using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INE : Player
{
	public GameObject Weapon;
	public GameObject BombWeapon;
	GameObject[] INEBullet;
	GameObject INEBomb;
	// Enemys = GameObject.FindGameObjectsWithTag("Enemy");
	// EnemyBosses = GameObject.FindGameObjectsWithTag("EnemyBoss");
	//public Vector2 PrevieousPos;
	//public Vector2 Translation;

	// 탄,폭탄 관련 변수
	byte Cooltime = 10;
	bool OnHit = false;
	byte BombCooltime = 150;


	void Start()
	{
		transform.position = new Vector2(0f,-5f);

		// 캐릭터 스테이터스
		Movespeed = 5f;
		SlowMovespeed = 2f;
		StartBomb = 3;
		Life = 2;
		Bomb = StartBomb;
		Power = 0;

		// 오브젝트 풀링
		INEBullet = new GameObject[2];
		INEBullet[0] = Instantiate(Weapon);
		INEBullet[1] = Instantiate(Weapon);
		INEBullet[0].transform.parent = transform;
		INEBullet[1].transform.parent = transform;
		INEBullet[0].gameObject.GetComponent<SpriteRenderer>().enabled = false;
		INEBullet[1].gameObject.GetComponent<SpriteRenderer>().enabled = false;
		INEBullet[0].gameObject.GetComponent<Collider2D>().enabled = false;
		INEBullet[1].gameObject.GetComponent<Collider2D>().enabled = false;
		INEBullet[0].SetActive(true);
		INEBullet[1].SetActive(true);
		INEBomb = Instantiate(BombWeapon);
		INEBomb.SetActive(false);
		INEBomb.transform.parent = transform;
		INEBomb.transform.position = new Vector2 (0f,10f);
	}


	void FixedUpdate()
	{
		this.gameObject.GetComponent<Player>().InvincibleEffect();
		this.gameObject.GetComponent<Player>().Move();

		// 레이저 스프라이트 On/Off
		if (Input.GetKey(KeyCode.Z))
		{
			INEBullet[0].transform.localPosition = new Vector2(-0.2f,17f);
			INEBullet[1].transform.localPosition = new Vector2(0.2f,17f);
			if (!OnHit)
			{
			OnHit = true;
			INEBullet[0].gameObject.GetComponent<SpriteRenderer>().enabled = true;
			INEBullet[1].gameObject.GetComponent<SpriteRenderer>().enabled = true;
			}
		}
		else if (OnHit)
		{
			OnHit = false;
			INEBullet[0].gameObject.GetComponent<SpriteRenderer>().enabled = false;
			INEBullet[1].gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}

		// 0.2초마다 공격 판정 생성
		if (Cooltime == 2)
		{
			INEBullet[0].gameObject.GetComponent<Collider2D>().enabled = false;
			INEBullet[1].gameObject.GetComponent<Collider2D>().enabled = false;
		}
		if (Cooltime < 10)
		{
			Cooltime++;
		}
		else if (Cooltime == 10 & OnHit)
		{
			Cooltime = 1;
			INEBullet[0].transform.Translate(0f,0f,0.001f);
			INEBullet[1].transform.Translate(0f,0f,0.001f);
			INEBullet[0].gameObject.GetComponent<Collider2D>().enabled = true;
			INEBullet[1].gameObject.GetComponent<Collider2D>().enabled = true;
			INEBullet[0].transform.Translate(0f,0f,-0.001f);
			INEBullet[1].transform.Translate(0f,0f,-0.001f);
		}

		// 폭탄 사용
		if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0)
		{
			BombCooltime = 0 ;
			INEBomb.transform.position = new Vector2 (0f,10f);
			INEBomb.gameObject.SetActive(true);
			this.gameObject.layer = 6;
			Invincible = 3f;
			Bomb--;
		}

		// 폭탄 데미지 발생 트리거
		if (BombCooltime < 150)
		{
			if (BombCooltime % 5 == 0)
			{
				INEBomb.gameObject.GetComponent<Collider2D>().enabled = true;
			}
			else if (BombCooltime % 5 == 1)
			{
				INEBomb.gameObject.GetComponent<Collider2D>().enabled = false;
			}

			BombCooltime++;
			INEBomb.transform.Translate(0f,-0.001f,0f);
			if (BombCooltime == 150)
			{
				INEBomb.gameObject.SetActive(false);
			}

		}

	}
}