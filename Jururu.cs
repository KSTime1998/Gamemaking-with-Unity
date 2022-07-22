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
		for (byte i = 0 ; i < 100 ; i++)
		{
			JururuBullet[i] = Instantiate(Weapon);
			JururuBullet[i].transform.parent = transform;
			JururuBullet[i].SetActive(false);
		}
    JururuBomb = new GameObject[10];
    for (byte i = 0 ; i < 10 ; i++)
    {
		JururuBomb[i] = Instantiate(BombWeapon);
		JururuBomb[i].SetActive(false);
		JururuBomb[i].transform.parent = transform;
		JururuBomb[i].transform.position = new Vector2 (0f,0f);
    }

    // 캐릭터 스테이터스
    Movespeed = 9f;
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

		else if (Input.GetKey(KeyCode.Z))
		{
      Cooltime = 1;
			Fire(new Vector2(0.5f,0.2f));
			Fire(new Vector2(-0.5f,0.2f));
			if (Power > 49)
			{
				Fire(new Vector2(1f,0f));
				Fire(new Vector2(-1f,0f));
				if (Power > 99)
				{
					Fire(new Vector2(2f,-0.6f));
					Fire(new Vector2(-2f,-0.6f));
					if (Power > 149)
					{
						Fire(new Vector2(3f,-0.6f));
						Fire(new Vector2(-3f,-0.6f));
						if (Power == 200)
						{
							Fire(new Vector2(4f,-0.6f));
							Fire(new Vector2(-4f,-0.6f));
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
			Invincible = 5f;
			Bomb--;
		}

    if (BombCooltime < 150)
    {
    }
  }
}