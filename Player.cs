using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // 게임 관련 변수
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
    Vector2 speedvec = new Vector2(moveX,moveY) * 50f * Time.deltaTime * Gamespeed;
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
    Invincible = 3;
    this.gameObject.layer = 6;

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

    else if (coll.gameObject.CompareTag("Enemy") | coll.gameObject.CompareTag("EnemyBoss"))
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

}