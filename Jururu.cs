using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jururu : Player
{
  public GameObject Weapon;
  public GameObject BombWeapon;
  GameObject[] JururuBullet;
  GameObject[] JururuBomb;

  // 탄,폭탄 관련 변수
  byte Cooltime = 10;
  byte BulletIndex = 0;
  byte BombCooltime = 150;

  // 초기 위치를 넣으면 탄환을 액티브하고 발사하는 함수
  void Fire(Vector2 pos)
  {
  	JururuBullet[BulletIndex].transform.localPosition = pos;
  	JururuBullet[BulletIndex].SetActive(true);
  	JururuBullet[BulletIndex].GetComponent<PlayerBullet>().Damage = 10 * (1 + Power/100);
  	JururuBullet[BulletIndex].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
  	BulletIndex++;
  	if (BulletIndex == 100)
  	{
  		BulletIndex = 0;
  	}
  }

  void Start()
  {
    transform.position = new Vector2(0f,-5f);

		// 오브젝트 풀링
		JururuBullet = new GameObject[100];
		JururuBomb = new GameObject[10];
		for (byte i = 0 ; i < 100 ; i++)
		{
			JururuBullet[i] = Instantiate(Weapon);
			JururuBullet[i].transform.parent = transform;
			JururuBullet[i].SetActive(false);
		}
		for (byte i = 0 ; i < 10 ; i++)
		{
			JururuBomb[i] = Instantiate(BombWeapon);
			JururuBomb[i].transform.parent = transform;
			JururuBomb[i].SetActive(false);
		}

    // 캐릭터 스테이터스
    Movespeed = 10f;
    SlowMovespeed = 6f;
    StartBomb = 2;
    Life = 2;
    Bomb = StartBomb;
    Power = 0;
  }


  void FixedUpdate()
  {
  	this.gameObject.GetComponent<Player>().InvincibleEffect();
  	this.gameObject.GetComponent<Player>().Move();

    // 공격
    if (Cooltime != 10)
		{
			Cooltime++;
		}

		else if (Input.GetKey(KeyCode.Z) & BombCooltime == 150)
		{
      Cooltime = 1;
			Fire(new Vector2(0.25f,0.2f));
			Fire(new Vector2(-0.25f,0.2f));
			if (Power > 49)
			{
				Fire(new Vector2(0.75f,0.15f));
				Fire(new Vector2(-0.75f,0.15f));
				if (Power > 99)
				{
					Fire(new Vector2(1.25f,0.1f));
					Fire(new Vector2(-1.25f,0.1f));
					if (Power > 149)
					{
						Fire(new Vector2(1.75f,0.05f));
						Fire(new Vector2(-1.75f,0.05f));
						if (Power == 200)
						{
							Fire(new Vector2(2.25f,0f));
							Fire(new Vector2(-2.25f,0f));
						}
					}
				}
			}
    }

    // 폭탄 사용
		if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0)
		{
			BombCooltime = 0;
			this.gameObject.layer = 6;
			Invincible = 3f;
			Bomb--;
		}

    if (BombCooltime < 150)
    {
			if(BombCooltime >= 50)
			{
				if (BombCooltime % 10 == 0)
				{
					Vector2 Epos = this.gameObject.GetComponent<Player>().Aming(transform.position);
					if (Epos == new Vector2(0,20))
					{
						Epos = new Vector2(Random.Range(-9.0f,9.0f),Random.Range(-6.0f,14.0f));
					}
					else
					{
						Vector2 Rpos = new Vector2(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f));
						Epos += Rpos;
						if (Epos.x < -9)
						{ Epos.x = -9; }
						if (Epos.x > 9)
						{ Epos.x = 9; }
						if (Epos.y < -6)
						{ Epos.y = -6; }
						if (Epos.y > 14)
						{ Epos.y = 14; }
					}
					JururuBomb[(BombCooltime-50) / 10].transform.position = Epos;
					JururuBomb[(BombCooltime-50) / 10].GetComponent<PlayerBullet>().Damage = 60 * (1 + Power/50);
					JururuBomb[(BombCooltime-50) / 10].SetActive(true);
					JururuBomb[(BombCooltime-50) / 10].gameObject.GetComponent<Collider2D>().enabled = true;
				}
					else if (BombCooltime % 10 == 5)
				{
					JururuBomb[(BombCooltime-50) / 10].gameObject.GetComponent<Collider2D>().enabled = false;
					JururuBomb[(BombCooltime-50) / 10].SetActive(false);
				}
			}
			BombCooltime++;
    }
  }
}