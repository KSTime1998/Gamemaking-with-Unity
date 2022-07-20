using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIichan : Player
{
	public GameObject Weapon;
	public GameObject BombWeapon;
	GameObject[] VIichanBullet;
	GameObject[] VIichanBomb;
	// Enemys = GameObject.FindGameObjectsWithTag("Enemy");
	// EnemyBosses = GameObject.FindGameObjectsWithTag("EnemyBoss");
	//public Vector2 PrevieousPos;
	//public Vector2 Translation;
	float Cooltime = 0f;
	short BombBulletNum;
	float degree;

	// 기본 공격 생성 위치
	Vector2 L_B_Pos = new Vector2(-0.2f,0.1f);
	Vector2 R_B_Pos = new Vector2(0.2f,0.1f);

	// 탄 스피드 | 폭탄 스피드
	public short BulletSpeed = 10;
	public short BombSpeed = 100;

	void Start()
	{
		//this.gameObject.SetActive(false);
		//transform.position = new Vector2(0f , -5f);
		//PrevieousPos = transform.position;

		// 이하 캐릭터별 스테이터스
		Movespeed = 7f;
		SlowMovespeed = 4f;
		StartBomb = 3;
		Life = 2;
		Bomb = StartBomb;

		// 오브젝트 풀링
		VIichanBullet = new GameObject[100];
		VIichanBomb = new GameObject[100];
		for (byte i = 0; i < 100 ; i++)
		{
			VIichanBullet[i] = Instantiate(Weapon);
			VIichanBullet[i].SetActive(false);
			VIichanBomb[i] = Instantiate(BombWeapon);
			VIichanBomb[i].transform.parent = transform;
			VIichanBomb[i].SetActive(false);
		}

	}


	void FixedUpdate()
	{
		//Vector2 CurrentPos = transform.position;
		//Translation = CurrentPos - PrevieousPos;
		//PrevieousPos = transform.position;
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
			BombBulletNum = 200;
		}

		// 폭탄 발사 트리거
		// BombBulletNum은 200에서 시작하여 FixedUpdate 1회마다 1씩 줄어듬(0.02초마다 1 감소)
		// 200 ~ 101틱까지 총 100개의 오브젝트 활성화. 활성화 직후 위치와 방향 지정
		// 이후 100이 될 때까지 200 ~ BombBulletNum까지의 오브젝트들(101 ~ B는 아직 비활성화 상태라 건드리면 안됨)의 위치를 갱신

		// 절반이 활성화된 시점(BombBulletNum == 150)이 되면, 처음 활성화된 개체부터 방향을 재지정하고 발사 직전까지 갱신해줌.
		// 방향 갱신은 발사 직전까지 위치 갱신과 함께 이루어짐.
		// 방향 갱신은 Player Class에서 구현한 함수를 이용함. 

		// BombBulletNum이 100 이하가 되면 200일 때 활성화한 개체부터 발사를 시작함. 그러므로 가장 마지막에 활성화된 개체부터
		// 발사 직전인 개체까지만 위치 갱신을 진행해줌. 발사한 개체는 건드리지 않음.
		if (BombBulletNum > 0)
		{
			if (BombBulletNum > 100)
			{
				int index = BombBulletNum - 101;
				VIichanBomb[index].GetComponent<PlayerBullet>().Damage = 0;
				VIichanBomb[index].SetActive(true);
				VIichanBomb[index].transform.localPosition = new Vector2(Mathf.Sin(BombBulletNum * Mathf.PI / 50) , Mathf.Cos(BombBulletNum * Mathf.PI / 50));
				VIichanBomb[index].transform.rotation = Quaternion.Euler (new Vector3(0,0,-BombBulletNum * Mathf.PI / 50 * Mathf.Rad2Deg ));
				for (int i = 200 ; i > BombBulletNum ; i--)
				{
					VIichanBomb[i - 101].transform.localPosition = new Vector2 (Mathf.Sin(i* Mathf.PI / 50) , Mathf.Cos(i * Mathf.PI / 50));
				}
			}

			if (BombBulletNum < 151 & BombBulletNum > 100)
			{
				Vector2 Epos = this.gameObject.GetComponent<Player>().Aming(transform.position);
				for (int i = BombBulletNum - 51 ; i < 100 ; i++)
				{
					Vector2 Bpos = VIichanBomb[i].transform.position;
					Vector2 direction_vector = (Epos - Bpos).normalized;
					degree = -Mathf.Atan(direction_vector.x / direction_vector.y) * Mathf.Rad2Deg;
					VIichanBomb[i].transform.rotation = Quaternion.Euler (new Vector3(0,0,degree));
				}
			}

			if (BombBulletNum <= 100)
			{
				for (int i = 0 ; i < BombBulletNum - 1 ; i++)
				{
					VIichanBomb[i].transform.localPosition = new Vector2 (Mathf.Sin((i + 101) * Mathf.PI / 50) , Mathf.Cos((i + 101) * Mathf.PI / 50));
				}
				VIichanBomb[BombBulletNum - 1].SetActive(false);
				VIichanBomb[BombBulletNum - 1].GetComponent<PlayerBullet>().Damage = 100;
				VIichanBomb[BombBulletNum - 1].SetActive(true);

				Vector2 Epos = this.gameObject.GetComponent<Player>().Aming(transform.position);
				for (int i = Mathf.Max(0, BombBulletNum - 50); i < BombBulletNum ; i++)
				{
					Vector2 Bpos = VIichanBomb[i].transform.position;
					Vector2 direction_vector = (Epos - Bpos).normalized;
					degree = -Mathf.Atan(direction_vector.x / direction_vector.y) * Mathf.Rad2Deg;
					VIichanBomb[i].transform.rotation = Quaternion.Euler (new Vector3(0,0,degree));
					if(i == BombBulletNum - 1)
					{
						VIichanBomb[BombBulletNum - 1].GetComponent<Rigidbody2D>().AddForce(direction_vector * BombSpeed * Gamespeed);
					}
				}
			}

			BombBulletNum--;
		}

	}
}