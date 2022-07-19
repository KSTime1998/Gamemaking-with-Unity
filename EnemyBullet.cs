using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 14)
        {
            this.gameObject.SetActive(false);
        }
    }
}
