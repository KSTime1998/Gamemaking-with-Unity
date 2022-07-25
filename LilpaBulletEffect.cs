using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilpaBulletEffect : MonoBehaviour
{
  public int LifeTime = 50;

  void FixedUpdate()
  {
    if (this.gameObject.activeSelf)
    {
      LifeTime--;
    }
    if (LifeTime == 0)
    {
      LifeTime = 50;
      this.gameObject.SetActive(false);
    }
  }
}
