using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // 이하 변수는 나중에 GameManager로 옮길 예정.
  public GameObject[] Enemys;
  public GameObject[] EnemyBosses;
  public float Gamespeed = 1f;

  // 스테이터스 관련 변수
  public float Movespeed;
  public float SlowMovespeed;
  public byte StartBomb;
  public float Invincible;

  public byte Life = 0;
  public byte Bomb = 0;
  public byte Power = 0;
  public int Donation = 0;
  public sbyte Barrior = 0;

  // 이동 함수
  public void Move()
  {
		float moveX = 0f;
		float moveY = 0f;
		  if (Input.GetKey(KeyCode.UpArrow))
		{
			moveY += 1f;
		}
		  if (Input.GetKey(KeyCode.DownArrow))
		{
			moveY -= 1f;
		}
		  if (Input.GetKey(KeyCode.RightArrow))
		{
			moveX += 1f;
		}
		  if (Input.GetKey(KeyCode.LeftArrow))
		{
			moveX -= 1f;
		}
    Vector2 speedvec = new Vector2(moveX,moveY);
		  if (Input.GetKey(KeyCode.LeftShift))
		{
			GetComponent<Rigidbody2D>().velocity = speedvec * SlowMovespeed ;
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = speedvec * Movespeed ;
		}
	}

  // 피격 시 스테이터스 변경 함수(라이프 감소 및 무적시간 부여)
  void Hit()
  {
    this.gameObject.layer = 6;

    if (Barrior > 0)
    {
      Invincible = 1;

      if (Barrior == 2)
      {
        Barrior = -2;
      }
      else
      {
        Barrior = -1;
      }
    }

    else
    {
      Invincible = 3;

      if (Life == 0)
      {
        //Game over
      }
      else
      {
        Life--;
        Bomb = StartBomb;
      }
    }
  }

  // 오브젝트 접촉 함수(플레이어이므로 적 탄막, 적, 아이템에만 접촉 가능)
  public void OnTriggerEnter2D(Collider2D coll)
  {
    if (coll.gameObject.CompareTag("EnemyBullet"))
    {
      if (this.gameObject.layer == 7)
      {
        Hit();
      }
      coll.gameObject.SetActive(false);
    }

    else if ((coll.gameObject.CompareTag("Enemy") | coll.gameObject.CompareTag("EnemyBoss")) & this.gameObject.layer == 7)
    {
      Hit();
    }

    else if (coll.gameObject.CompareTag("Item"))
    {
      Item TYPE = coll.gameObject.GetComponent<Item>();
      switch (TYPE.type)
      {
        case "Life":
          if (Life < 10)
          {
            Life++;
          }
          break;
        case "Paper":
          Donation += 10000;
          break;
        case "Coin":
          Donation += 100;
          break;
        case "Power":
          if (Power < 200)
          {
            Power++;
          }
          break;
        case "Bomb":
          if (Bomb < 10)
          {
            Bomb++;
          }
          break;
      }

      coll.gameObject.SetActive(false);
    }
  }


  // 무적 시 플레이어 불투명조 조절(깜빡임 효과) 함수
  public void Twinkling(float I)
  {
    Color C = gameObject.GetComponent<SpriteRenderer>().color;
    C.a = Mathf.Pow(Mathf.Cos(I * 20 * Mathf.PI),2);
    gameObject.GetComponent<SpriteRenderer>().color = C;
  }


  // 무적시간 함수
  public void InvincibleEffect()
  {
    if (Invincible > 0f)
    {
      Invincible -= Time.deltaTime * Gamespeed;
      Twinkling(Invincible);
      if (Invincible <= 0f)
      {
        Invincible = 0f;
        Twinkling(0);
        this.gameObject.layer = 7;
      }
    }
  }

  // 플레이어 조준탄 방향 지정 함수
  // 사용할 때 밑 줄 붙여넣기 하면 편함.
  // Vector2 Epos = this.gameObject.GetComponent<Player>().Aming(transform.position)
  public Vector2 Aming(Vector2 pos)
  {

    float distance = 1000;
    Vector2 E = new Vector2(0,10);
    Vector2 Epos;

    for (int i = 0 ; i < EnemyBosses.Length ; i++)
    {
      if (EnemyBosses[i].gameObject.activeSelf)
      {
        Epos = EnemyBosses[i].gameObject.transform.position;
        float d = Vector2.Distance(pos,Epos);
        if (distance > d)
        {
          distance = d;
          E = Epos;
        }
      }
    }

    if (distance != 1000)
    {
      return E;
    }

    else
    {
      for (int i = 0 ; i < Enemys.Length ; i++)
      {
        if (Enemys[i].gameObject.activeSelf)
        {
          Epos = Enemys[i].gameObject.transform.position;
          float d = Vector2.Distance(pos,Epos);
          if (distance > d)
          {
            distance = d;
            E = Epos;
          }
        }
      }
      return E;
    }

  }


  // 유도탄 함수
}