using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlayerBullet"))
		{
			PlayerBullet values = coll.gameObject.GetComponent<PlayerBullet>();
            if (!values.isLazer)
            {
                coll.gameObject.SetActive(false);
            }
        }
        else
        {
            coll.gameObject.SetActive(false);
        }
    }
}
