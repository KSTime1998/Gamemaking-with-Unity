using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public byte Life = 2;
  public float Invincible = 0;

  void Hit()
  {
    Invincible = 3;
    this.gameObject.layer = 6;

    if (Life == 0)
    {
      //Game over
      Debug.Log("게임 오버");
    }
    else
    {
      Life -= 1;
      Debug.Log("피격!");
    }
  }

  public void OnCollisionEnter2D(Collision2D coll)
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
  }

  public void Twinkling(float I)
  {
    Color C = gameObject.GetComponent<SpriteRenderer>().color;
    C.a = Mathf.Pow(Mathf.Cos(I * 80/3 * Mathf.PI),2);
    gameObject.GetComponent<SpriteRenderer>().color = C;
  }

  void Update()
  {
    if (Invincible > 0)
    {
      Invincible -= Time.deltaTime;
      Twinkling(Invincible);
      if (Invincible <= 0)
      {
        Invincible = 0;
        Twinkling(0);
        this.gameObject.layer = 7;
      }
    }
  }

}