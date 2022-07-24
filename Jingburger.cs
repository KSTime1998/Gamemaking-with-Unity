using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jingburger : Player
{
  public GameObject Weapon;
  GameObject[] JingburgerBullet;

	// 탄,폭탄 관련 변수
  byte Cooltime = 10;
  int BulletIndex = 0;
  byte BombCooltime = 200;

	void Fire(Vector2 pos, float D, float dir, float R)
	{
		JingburgerBullet[BulletIndex].transform.localPosition = pos;
    JingburgerBullet[BulletIndex].GetComponent<SpriteRenderer>().enabled = true;
    JingburgerBullet[BulletIndex].GetComponent<CircleCollider2D>().radius = R;
		JingburgerBullet[BulletIndex].GetComponent<PlayerBullet>().Damage = D * (1 + Power/50);
    JingburgerBullet[BulletIndex].GetComponent<JingburgerBulletEffect>().Radius = R * (2 + Power/200);
    JingburgerBullet[BulletIndex].SetActive(true);
		JingburgerBullet[BulletIndex].GetComponent<Rigidbody2D>().AddForce(new Vector2(dir,1f).normalized * 1000);
		BulletIndex++;
		if (BulletIndex == 300)
		{
			BulletIndex = 0;
		}
	}

	void Start()
	{
		transform.position = new Vector2(0f,-5f);

		// 캐릭터 스테이터스
		Movespeed = 9f;
		SlowMovespeed = 5.5f;
		StartBomb = 2;
		Life = 2;
		Bomb = StartBomb;
		Power = 0;

		// 오브젝트 풀링
		JingburgerBullet = new GameObject[300];
		for (int i = 0 ; i < 300 ; i++)
		{
			JingburgerBullet[i] = Instantiate(Weapon);
			JingburgerBullet[i].transform.parent = transform;
			JingburgerBullet[i].SetActive(false);
		}
	}

	void FixedUpdate()
	{
		this.gameObject.GetComponent<Player>().InvincibleEffect();
		this.gameObject.GetComponent<Player>().Move();

		// 공격 쿨타임
		if (Cooltime != 10)
		{
			Cooltime++;
		}

		// 공격
		else if (Input.GetKey(KeyCode.Z))
		{
      if (BombCooltime == 200)
      {
			  Cooltime = 1;
			  Fire(new Vector2(-0.5f,0.1f),10f,0f,0.5f);
			  Fire(new Vector2(0.5f,0.1f),10f,0f,0.5f);
      }
      else
      {
        Cooltime = 6;
			  Fire(new Vector2(-0.5f,0.1f),2.5f,0f,1f);
			  Fire(new Vector2(0.5f,0.1f),2.5f,0,1f);
			  Fire(new Vector2(-0.5f,-0.1f),2.5f,-0.1f,1f);
			  Fire(new Vector2(0.5f,-0.1f),2.5f,0.1f,1f);
        Fire(new Vector2(-0.25f,0f),2.5f,-0.2f,1f);
        Fire(new Vector2(0.25f,0f),2.5f,0.2f,1f);
      }
		}

		if (Input.GetKey(KeyCode.X) & this.gameObject.layer == 7 & this.gameObject.GetComponent<Player>().Bomb > 0)
		{
      BombCooltime = 0;
		  Movespeed = 13f;
		  SlowMovespeed = 8f;
			this.gameObject.layer = 6;
			Invincible = 4f;
			Bomb--;
		}

    if (BombCooltime != 200)
    {
      BombCooltime++;
      if (BombCooltime == 200)
      {
        Movespeed = 9f;
        SlowMovespeed = 5.5f;
      }
    }

	}
}
