using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gosegu : Player
{
	public GameObject Weapon;
	public GameObject BombWeapon;
	GameObject[] GoseguBullet;
	GameObject GoseguBomb;

	// 탄,폭탄 관련 변수
	byte Cooltime = 3;
	byte BulletIndex = 0;
	byte BombCooltime = 20;

	// 초기 위치와 방향을 넣으면 탄환을 액티브하고 발사하는 함수
	void Fire(Vector2 pos, Vector2 dir)
	{
		GoseguBullet[BulletIndex].transform.localPosition = pos;
		GoseguBullet[BulletIndex].SetActive(true);
		GoseguBullet[BulletIndex].GetComponent<PlayerBullet>().Damage = 2 * (1 + Power/100);
		GoseguBullet[BulletIndex].GetComponent<Rigidbody2D>().AddForce(dir.normalized * 1500);
		BulletIndex++;
		if (BulletIndex == 150)
		{
			BulletIndex = 0;
		}
	}

	void Start()
	{
		transform.position = new Vector2(0f,-5f);

		// 캐릭터 스테이터스
		Movespeed = 11f;
		SlowMovespeed = 6.5f;
		StartBomb = 2;
		Life = 2;
		Bomb = StartBomb;
		Power = 0;

		// 오브젝트 풀링
		GoseguBullet = new GameObject[150];
		for (byte i = 0 ; i < 150 ; i++)
		{
			GoseguBullet[i] = Instantiate(Weapon);
			GoseguBullet[i].transform.parent = transform;
			GoseguBullet[i].SetActive(false);
		}
		GoseguBomb = Instantiate(BombWeapon);
		GoseguBomb.SetActive(false);
		GoseguBomb.transform.parent = transform;

	}


	void FixedUpdate()
	{
		this.gameObject.GetComponent<Player>().InvincibleEffect();
		this.gameObject.GetComponent<Player>().Move();

		// 머리핀 던지기 공격
		if (Cooltime != 3)
		{
			Cooltime++;
		}

		else if (Input.GetKey(KeyCode.Z))
		{
			Cooltime = 1;
			Fire(new Vector2(0.15f,0.1f),new Vector2(0f,1f));
			Fire(new Vector2(-0.15f,0.1f),new Vector2(0f,1f));
			if (Power > 49)
			{
				Fire(new Vector2(0.15f,0.05f),new Vector2(0.05f,1f));
				Fire(new Vector2(-0.15f,0.05f),new Vector2(-0.05f,1f));
				if (Power > 99)
				{
					Fire(new Vector2(0.5f,0.05f),new Vector2(0f,1f));
					Fire(new Vector2(-0.5f,0.05f),new Vector2(-0f,1f));
					if (Power > 149)
					{
						Fire(new Vector2(0.3f,0f),new Vector2(0.1f,1f));
						Fire(new Vector2(-0.3f,0f),new Vector2(-0.1f,1f));
						if (Power == 200)
						{
							Fire(new Vector2(0.4f,0f),new Vector2(0.15f,1f));
							Fire(new Vector2(-0.4f,0f),new Vector2(-0.15f,1f));
						}
					}
				}
			}
		}


		// // 폭탄 사용
		// if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0)
		// {
		// 	BombCooltime = 0;
		// 	GoseguBomb.transform.position = new Vector2(0f,0.5f);
		// 	GoseguBomb.GetComponent<PlayerBullet>().Damage = 32 * (1 + Power/50);
		// 	GoseguBomb.SetActive(true);
		// 	this.gameObject.layer = 6;
		// 	Invincible = 5f;
		// 	Bomb--;
		// }

		// // 폭탄 데미지 발생 로직
		// if (BombCooltime < 250)
		// {
		// 	if (BombCooltime % 5 == 0)
		// 	{
		// 		GoseguBomb.gameObject.GetComponent<Collider2D>().enabled = true;
		// 	}
		// 	else if (BombCooltime % 5 == 1)
		// 	{
		// 		GoseguBomb.gameObject.GetComponent<Collider2D>().enabled = false;
		// 	}

		// 	BombCooltime++;
		// 	GoseguBomb.transform.localPosition = new Vector3(0f,10f,0f);
		// 	this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-2f);
		// 	if (BombCooltime == 250)
		// 	{
		// 		GoseguBomb.gameObject.SetActive(false);
		// 	}

		// }

    // else if (Input.GetKey(KeyCode.Z))
    // {
    //   Cooltime = 0;
    //   if (BulletIndex == 5)
    //   { BulletIndex = 0; }
    //   LilpaBullet[BulletIndex].transform.localPosition = new Vector3(0f,0f,0f);
    //   LilpaBullet[BulletIndex].GetComponent<PlayerBullet>().Damage = 16 * (1 + Power/100);
    //   LilpaBullet[BulletIndex].SetActive(true);
    //   LilpaBullet[BulletIndex].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2000);
    //   BulletIndex++;
    // }

		if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0 & Barrior == 0)
		{
      this.gameObject.GetComponent<CircleCollider2D>().radius = 0.25f;
      this.gameObject.layer = 6;
      Invincible = 0.2f;
      Bomb--;
      Barrior = 2;
    }

    if (Barrior < 0)
    {
      GoseguBomb.transform.localPosition = new Vector3(0,0,0);
      GoseguBomb.SetActive(true);
      BombCooltime = 0;
      if (Barrior == -2)
      { Barrior = 1; }
      else
      {
        Barrior = 0;
        this.gameObject.GetComponent<CircleCollider2D>().radius = 0.2f;
      }
    }

    if (BombCooltime <= 20)
    {
      if (BombCooltime == 20)
      {
        GoseguBomb.gameObject.GetComponent<CircleCollider2D>().radius = 3f;
        GoseguBomb.SetActive(false);
      }
      BombCooltime++;
      GoseguBomb.transform.position += new Vector3(0f,0.001f,0f);
      GoseguBomb.gameObject.GetComponent<CircleCollider2D>().radius *= 1.1f;
    }

	}
}