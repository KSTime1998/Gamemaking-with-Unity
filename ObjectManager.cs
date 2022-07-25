using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

  // 프리팹
  public GameObject Item_Power_Prefab;
  public GameObject Item_Paper_Prefab;
  public GameObject Item_Coin_Prefab;
  public GameObject Item_Life_Prefab;
  public GameObject Item_Bomb_Prefab;

  public GameObject Ameba_Prefab;
  public GameObject Mite_Prefab;
  public GameObject ChickenPigeon_Prefab;
  public GameObject WakParrot_Prefab;
  public GameObject Chimpanchee_Prefab;
  public GameObject Neugeuza_Prefab;
  public GameObject Boss_Prefab;

  public GameObject Point_Prefab;
  public GameObject SmallSph_Prefab;
  public GameObject Bullet_Prefab;
  public GameObject MediumSph_Prefab;
  public GameObject LargeSph_Prefab;
  public GameObject Star_Prefab;

  // 실질적인 오브젝트 배열(큐에 저장)
  //// GameObejct[] ~ ;
  public Queue Item_Power = new Queue();
  public Queue Item_Paper = new Queue();
  public Queue Item_Coin = new Queue();
  public Queue Item_Life = new Queue();
  public Queue Item_Bomb = new Queue();

  public Queue Ameba = new Queue();
  public Queue Mite = new Queue();
  public Queue ChickenPigeon = new Queue();
  public Queue WakParrot = new Queue();
  public Queue Chimpanchee = new Queue();
  public Queue Neugeuza = new Queue();
  public Queue Boss = new Queue();

  public Queue Point = new Queue();
  public Queue SamllSph = new Queue();
  public Queue Bullet = new Queue();
  public Queue MediumSph = new Queue();
  public Queue LargeSph = new Queue();
  public Queue Star = new Queue();

  void ObjectPooling(GameObject type)
  {
    GameObject newObj = Instantiate(type);
    newObj.SetActive(false);
    Item_Power.Enqueue(newObj);
  }

  // 함수 간략화를 위한 타겟 변수
  GameObject[] target;


  // 오브젝트 풀링
  void Start()
  {
    //// ~ = new GameObject[~];
    for (int i = 0 ; i < 500 ; i++)
    {
      if (i < 10)
      {
        ObjectPooling(Item_Life_Prefab);
        ObjectPooling(Item_Bomb_Prefab);

        ObjectPooling(Neugeuza_Prefab);
      }

      if (i < 20)
      {
        ObjectPooling(Boss_Prefab);
      }

      if (i < 100)
      {
        ObjectPooling(Item_Power_Prefab);
        ObjectPooling(Item_Paper_Prefab);
        ObjectPooling(Item_Coin_Prefab);

        ObjectPooling(Ameba_Prefab);
        ObjectPooling(Mite_Prefab);
        ObjectPooling(ChickenPigeon_Prefab);
        ObjectPooling(WakParrot_Prefab);
        ObjectPooling(Chimpanchee_Prefab);
      }

      ObjectPooling(Point_Prefab);
      ObjectPooling(SmallSph_Prefab);
      ObjectPooling(Bullet_Prefab);
      ObjectPooling(MediumSph_Prefab);
      ObjectPooling(LargeSph_Prefab);
      ObjectPooling(Star_Prefab);
    }
  }



  // Enqueue
  // private Bullet CreateNewObject()
  // {
  //   var NEW = Instantiate(poolingObjectPrefab).GetComponent<Bullet>();
  //   NEW.gameObject.SetActive(false);
  //   NEW.transform.SetParent(transform);
  //   return NEW;
  // }

  // Dequeue



  // 아이템 생성 함수
  // public void Make_Item(Vector2 pos, string type)
  // {
  //   target = null;
  //   switch (type)
  //   {
  //     case "Power":
  //       target = Item_Power;
  //       break;
  //     case "Paper":
  //       target = Item_Paper;
  //       break;
  //     case "Coin":
  //       target = Item_Coin;
  //       break;
  //     case "Life":
  //       target = Item_Life;
  //       break;
  //     case "Bomb":
  //       target = Item_Bomb;
  //       break;
  //   }
    // target.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,0f,0f);
    // target.transform.position = pos;
    // target.SetActive(true);
    // target.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 250f);
  // }

  public void Spawn()
  {
  }

  public void EnemyFire()
  {
  }

}
