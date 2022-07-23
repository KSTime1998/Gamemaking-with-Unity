using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INE : Player
{
	public GameObject Weapon;
	public GameObject BombWeapon;
	public GameObject BombEffect;
	GameObject[] INEBullet;
	GameObject[] INEBombEffect;
	GameObject INEBomb;
	// Enemys = GameObject.FindGameObjectsWithTag("Enemy");
	// EnemyBosses = GameObject.FindGameObjectsWithTag("EnemyBoss");
	//public Vector2 PrevieousPos;
	//public Vector2 Translation;

	// 탄,폭탄 관련 변수
	byte Cooltime = 10;
	byte BulletIndex = 0;
	byte BombCooltime = 100;


	void Start()
	{
		transform.position = new Vector2(0f,-5f);

		// 캐릭터 스테이터스
		Movespeed = 8f;
		SlowMovespeed = 5f;
		StartBomb = 3;
		Life = 2;
		Bomb = StartBomb;
		Power = 0;

		// 오브젝트 풀링
		INEBullet = new GameObject[20];
		for (byte i = 0 ; i < 20 ; i++)
		{
			INEBullet[i] = Instantiate(Weapon);
			INEBullet[i].transform.parent = transform;
			INEBullet[i].SetActive(false);
		}
		INEBomb = Instantiate(BombWeapon);
		INEBomb.SetActive(false);
		INEBomb.transform.parent = transform;
		INEBomb.transform.position = new Vector2 (0f,10f);
		INEBombEffect = new GameObject[2];
		for (byte i = 0 ; i < 2 ; i++)
		{
			INEBombEffect[i] = Instantiate(BombEffect);
			INEBombEffect[i].transform.parent = transform;
			INEBombEffect[i].SetActive(false);
		}
	}


	void FixedUpdate()
	{
		this.gameObject.GetComponent<Player>().InvincibleEffect();
		this.gameObject.GetComponent<Player>().Move();

		// 레이저 공격 (폐기했으나 혹시 몰라서 남겨둠)
		// // 레이저 스프라이트 On/Off
		// if (Input.GetKey(KeyCode.Z))
		// {
		// 	INEBullet[0].transform.localPosition = new Vector2(-0.2f,17f);
		// 	INEBullet[1].transform.localPosition = new Vector2(0.2f,17f);
		// 	if (!OnHit)
		// 	{
		// 	OnHit = true;
		// 	INEBullet[0].gameObject.GetComponent<SpriteRenderer>().enabled = true;
		// 	INEBullet[1].gameObject.GetComponent<SpriteRenderer>().enabled = true;
		// 	}
		// }
		// else if (OnHit)
		// {
		// 	OnHit = false;
		// 	INEBullet[0].gameObject.GetComponent<SpriteRenderer>().enabled = false;
		// 	INEBullet[1].gameObject.GetComponent<SpriteRenderer>().enabled = false;
		// }

		// // 0.2초마다 공격 판정 생성
		// if (Cooltime == 2)
		// {
		// 	INEBullet[0].gameObject.GetComponent<Collider2D>().enabled = false;
		// 	INEBullet[1].gameObject.GetComponent<Collider2D>().enabled = false;
		// }
		// if (Cooltime < 10)
		// {
		// 	Cooltime++;
		// }
		// else if (Cooltime == 10 & OnHit)
		// {
		// 	Cooltime = 1;
		// 	INEBullet[0].transform.Translate(0f,0f,0.001f);
		// 	INEBullet[1].transform.Translate(0f,0f,0.001f);
		// 	INEBullet[0].gameObject.GetComponent<Collider2D>().enabled = true;
		// 	INEBullet[1].gameObject.GetComponent<Collider2D>().enabled = true;
		// 	INEBullet[0].transform.Translate(0f,0f,-0.001f);
		// 	INEBullet[1].transform.Translate(0f,0f,-0.001f);
		// }

		// 도끼 던지기 공격
		if (Cooltime != 10)
		{
			Cooltime++;
		}

		else if (Input.GetKey(KeyCode.Z))
		{
			Cooltime = 1;
			BulletIndex++;
			if (BulletIndex == 20)
			{
				BulletIndex = 0;
			}
			INEBullet[BulletIndex].transform.localPosition = new Vector2(0f,0.2f);
			INEBullet[BulletIndex].SetActive(true);
			INEBullet[BulletIndex].GetComponent<PlayerBullet>().Damage = 30 * (1 + Power/100);
			INEBullet[BulletIndex].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 750);
			INEBullet[BulletIndex].GetComponent<Rigidbody2D>().AddTorque(750);
		}


		// 폭탄 사용
		if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0)
		{
			BombCooltime = 0 ;
			this.gameObject.layer = 6;
			Invincible = 2f;
			Bomb--;
			INEBomb.gameObject.GetComponent<Collider2D>().enabled = false;
			INEBomb.GetComponent<PlayerBullet>().Damage = 30 * (1 + Power/50);
			INEBomb.SetActive(true);
			INEBombEffect[0].transform.localPosition = new Vector2(0.1f,20f);
			INEBombEffect[0].transform.rotation = Quaternion.Euler (new Vector3(0,0,30));
			INEBombEffect[0].SetActive(true);
			INEBombEffect[1].transform.localPosition = new Vector2(-0.1f,20f);
			INEBombEffect[1].transform.rotation = Quaternion.Euler (new Vector3(0,0,30));
			INEBombEffect[1].SetActive(true);
		}

		// 폭탄 이펙트, 데미지 발생 로직
		if (BombCooltime < 100)
		{
			INEBomb.transform.position -= new Vector3(0f,0.001f,0f);
			if (BombCooltime == 50)
			{
				INEBombEffect[0].SetActive(false);
				INEBombEffect[1].SetActive(false);
			}

			if (BombCooltime < 50)
			{
				INEBombEffect[0].transform.localPosition = new Vector2(0.15f,0f);
				INEBombEffect[1].transform.localPosition = new Vector2(-0.15f,0f);
				float degree = Mathf.Atan((20-BombCooltime)/10f) * Mathf.Rad2Deg / 1.5f;
				INEBombEffect[0].transform.rotation = Quaternion.Euler (new Vector3(0,0,degree));
				INEBombEffect[1].transform.rotation = Quaternion.Euler (new Vector3(0,0,degree));
			}

			else
			{
				if (BombCooltime % 5 == 0)
				{
					INEBomb.gameObject.GetComponent<Collider2D>().enabled = true;
				}
				else if (BombCooltime % 5 == 4)
				{
					INEBomb.gameObject.GetComponent<Collider2D>().enabled = false;
				}
			}

			BombCooltime++;
			INEBomb.transform.position -= new Vector3(0f,0.001f,0f);
			if (BombCooltime == 100)
			{
				INEBomb.gameObject.GetComponent<Collider2D>().enabled = true;
				INEBomb.gameObject.SetActive(false);
			}

		}

	}
}