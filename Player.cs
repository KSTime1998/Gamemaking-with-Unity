using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public ObjectManager objectmanager;
  public GameManager gamemanager;
  public static Player Instance;

  // 스테이터스 관련 변수
  public float Movespeed;
  public float SlowMovespeed;
  public byte StartBomb;
  public float Invincible;

  public int SubShotType;
  public byte Life = 0;
  public byte Bomb = 0;
  public byte Power = 0;
  public int Donation = 0;
  public sbyte Barrior = 0;

  // 조준탄 관련 함수
  public Vector2 PlayerPos;
  public float Distance;
  public Vector2 EnemyPos;

  // 시너지 관련 함수 (총 21개)
  public bool Chanburger = false;
  public byte Main_Vocals = 0;

  // 이동 함수
  public void Move()
  {
		float moveX = 0f;
		float moveY = 0f;
		  if (Input.GetKey(KeyCode.UpArrow))
        moveY += 1f;
		  if (Input.GetKey(KeyCode.DownArrow))
			  moveY -= 1f;
		  if (Input.GetKey(KeyCode.RightArrow))
			  moveX += 1f;
		  if (Input.GetKey(KeyCode.LeftArrow))
			  moveX -= 1f;

    Vector2 speedvec = new Vector2(moveX,moveY).normalized;
		  if (Input.GetKey(KeyCode.LeftShift))
		{ GetComponent<Rigidbody2D>().velocity = speedvec * SlowMovespeed ; }
		else
		{ GetComponent<Rigidbody2D>().velocity = speedvec * Movespeed ; }
	}

  // 피격 시 스테이터스 변경 함수(라이프 감소 및 무적시간 부여)
  void Hit()
  {
    this.gameObject.layer = 6;

    if (Barrior > 0)
    {
      Invincible = 1;
      if (Barrior == 2)
      { Barrior = -2; }
      else
      { Barrior = -1; }
    }

    else
    { Invincible = 3;
      if (Life == 0)
      {
        //Game over
      }
      else
      { Life--;
        Bomb = StartBomb; }
    }
  }

  // 오브젝트 접촉 함수(플레이어이므로 적 탄막, 적, 아이템에만 접촉 가능)
  public void OnTriggerEnter2D(Collider2D coll)
  {
    if (coll.gameObject.CompareTag("EnemyBullet"))
    {
      if (this.gameObject.layer == 7)
      { Hit(); }
      ObjectManager.Instance.Off(coll.gameObject);
    }

    else if ((coll.gameObject.CompareTag("Enemy") | coll.gameObject.CompareTag("EnemyBoss")) & this.gameObject.layer == 7)
    { Hit(); }

    else if (coll.gameObject.CompareTag("Item"))
    {
      Item TYPE = coll.gameObject.GetComponent<Item>();
      switch (TYPE.type)
      {
        case "Life":
          if (Life < 10)
          { Life++; }
          else
          { Donation += 1000000;}
          break;
        case "Money":
          Donation += 10000;
          break;
        case "Coin":
          Donation += 100;
          break;
        case "Power":
          if (Power < 200 + Main_Vocals) {
            Power++;
            if (Power == 50)
            {}
            else if (Power == 100)
            {}
            else if (Power == 150)
            {}
            else if (Power == 200)
            {}
          }
          else
          { Donation += 10; }
          break;
        case "Bomb":
          if (Bomb < 10)
          { Bomb++; }
          else
          { Donation += 100000; }
          break;
      }

      ObjectManager.Instance.Off(coll.gameObject);
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
      Invincible -= Time.deltaTime;
      Twinkling(Invincible);
      if (Invincible <= 0f)
      {
        Invincible = 0f;
        Twinkling(0);
        this.gameObject.layer = 7;
      }
    }
  }

  void Awake()
  {
    Player.Instance = this;
  }

  // 조준 함수
  void Judge(GameObject[] Type, int length)
  {
    for (byte i = 0 ; i < length ; i++)
    {
      var target = Type[i];
      if (target.activeSelf)
      {
        Vector2 Epos = target.transform.position; 
        float D = (Epos - PlayerPos).magnitude;
        if (D < Distance)
        {
          Distance = D;
          EnemyPos = target.transform.position;
        }
      }
    }
  }

  public Vector2 CloseEnemy()
  {
    PlayerPos = transform.position;
    float Distance = 100f;
    EnemyPos = Vector2.up * 20f;
    Judge(ObjectManager.Instance.Boss,10);
    if (Distance != 100f)
    { return EnemyPos; }
    else
    {
      Judge(ObjectManager.Instance.Ameba,100);
      Judge(ObjectManager.Instance.Mite,100);
      Judge(ObjectManager.Instance.ChickenPigeon,50);
      Judge(ObjectManager.Instance.WakParrot,50);
      Judge(ObjectManager.Instance.Chimpanchee,10);
      Judge(ObjectManager.Instance.Neugeuza,10);
    }
    return EnemyPos;
  }

}