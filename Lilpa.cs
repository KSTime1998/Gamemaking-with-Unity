using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilpa : Player
{
  public GameObject Weapon;
  public GameObject BombWeapon;
  GameObject[] LilpaBullet;
  GameObject LilpaBomb;

	// 탄,폭탄 관련 변수
  byte Cooltime = 10;
  byte BulletIndex = 0;
  byte BombCooltime = 250;

	void Start()
	{
		transform.position = new Vector2(0f,-5f);

		// 캐릭터 스테이터스
		Movespeed = 12f;
		SlowMovespeed = 7f;
		StartBomb = 2;
		Life = 2;
		Bomb = StartBomb;
		Power = 0;

		// 오브젝트 풀링
		LilpaBullet = new GameObject[5];
		for (byte i = 0 ; i < 5 ; i++)
		{
			LilpaBullet[i] = Instantiate(Weapon);
			LilpaBullet[i].transform.parent = transform;
			LilpaBullet[i].SetActive(false);
		}
		LilpaBomb = Instantiate(BombWeapon);
    LilpaBomb.transform.parent = transform;
		LilpaBomb.SetActive(false);
    LilpaBomb.transform.position = new Vector2 (0f,10f);
	}

	void FixedUpdate()
	{
		this.gameObject.GetComponent<Player>().InvincibleEffect();
		this.gameObject.GetComponent<Player>().Move();

		if (Cooltime != 10)
		{
			Cooltime++;
		}

    else if (Input.GetKey(KeyCode.Z) & BombCooltime == 250)
    {
      Cooltime = 0;
      if (BulletIndex == 5)
      { BulletIndex = 0; }
      LilpaBullet[BulletIndex].transform.localPosition = new Vector3(0f,0f,0f);
      LilpaBullet[BulletIndex].GetComponent<PlayerBullet>().Damage = 16 * (1 + Power/100);
      LilpaBullet[BulletIndex].SetActive(true);
      LilpaBullet[BulletIndex].GetComponent<Rigidbody2D>().velocity = new Vector3(0f,40f,0f);
      BulletIndex++;
    }

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

    // for (byte i = 0 ; i < 5 ; i++)
    // {
    //   if (LilpaBullet[i].transform.position.y > 20)
    //   { LilpaBullet[i].SetActive(false); }
    // }

		// if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0 & Barrior == 0)
		// {
    //   this.gameObject.GetComponent<CircleCollider2D>().radius = 0.25f;
    //   this.gameObject.layer = 6;
    //   Invincible = 0.2f;
    //   Bomb--;
    //   Barrior = 2;
    // }

    // if (Barrior < 0)
    // {
    //   LilpaBomb.transform.localPosition = new Vector3(0,0,0);
    //   LilpaBomb.SetActive(true);
    //   BombCooltime = 0;
    //   if (Barrior == -2)
    //   { Barrior = 1; }
    //   else
    //   {
    //     Barrior = 0;
    //     this.gameObject.GetComponent<CircleCollider2D>().radius = 0.2f;
    //   }
    // }

    // if (BombCooltime <= 20)
    // {
    //   if (BombCooltime == 20)
    //   {
    //     LilpaBomb.gameObject.GetComponent<CircleCollider2D>().radius = 3f;
    //     LilpaBomb.SetActive(false);
    //   }
    //   BombCooltime++;
    //   LilpaBomb.transform.position += new Vector3(0f,0.001f,0f);
    //   LilpaBomb.gameObject.GetComponent<CircleCollider2D>().radius *= 1.1f;
    // }

		// 폭탄 사용
		if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0)
		{
			BombCooltime = 0;
			LilpaBomb.transform.position = new Vector2(0f,0.5f);
			LilpaBomb.GetComponent<PlayerBullet>().Damage = 32 * (1 + Power/50);
			LilpaBomb.SetActive(true);
			this.gameObject.layer = 6;
			Invincible = 5f;
			Bomb--;
		}

		// 폭탄 데미지 발생 로직
		if (BombCooltime < 250)
		{
			if (BombCooltime % 5 == 0)
			{
				LilpaBomb.gameObject.GetComponent<Collider2D>().enabled = true;
			}
			else if (BombCooltime % 5 == 1)
			{
				LilpaBomb.gameObject.GetComponent<Collider2D>().enabled = false;
			}

			BombCooltime++;
			LilpaBomb.transform.localPosition = new Vector3(0f,10f,0f);
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-2f);
			if (BombCooltime == 250)
			{
				LilpaBomb.gameObject.SetActive(false);
			}

		}

  }
}
