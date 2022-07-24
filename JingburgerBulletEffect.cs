using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JingburgerBulletEffect : MonoBehaviour
{
  public float Lifetime = 11;
  public float Radius;

  void Start()
  {
    Lifetime = 11f;
  }

  public void OnTriggerEnter2D(Collider2D coll)
  {
    if (coll.gameObject.CompareTag("Enemy"))
    {
      Lifetime = 10f;
      this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
      this.gameObject.GetComponent<CircleCollider2D>().radius = Radius;
      this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,1f,0f);
    }
  }

  void FixedUpdate()
  {
    if (Lifetime != 11)
    {
      Lifetime--;
    }

    if (Lifetime == 0)
    {
      Lifetime = 11f;
      this.gameObject.SetActive(false);
    }
  }
}
