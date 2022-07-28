using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
  public static ObjectManager Instance;

  // 프리팹
  public GameObject Item_Power_Prefab;
  public GameObject Item_Money_Prefab;
  public GameObject Item_Coin_Prefab;
  public GameObject Item_Life_Prefab;
  public GameObject Item_Bomb_Prefab;

  public GameObject Point_Prefab;
  public GameObject SmallSph_Prefab;
  public GameObject Bullet_Prefab;
  public GameObject MediumSph_Prefab;
  public GameObject LargeSph_Prefab;
  public GameObject Star_Prefab;

  public GameObject Ameba_Prefab;
  public GameObject Mite_Prefab;
  public GameObject ChickenPigeon_Prefab;
  public GameObject WakParrot_Prefab;
  public GameObject Chimpanchee_Prefab;
  public GameObject Neugeuza_Prefab;
  public GameObject Boss_Prefab;

  public GameObject Bidulgi_Prefab;
  public GameObject Poop_Prefab;
  public GameObject Bat_Prefab;
  public GameObject Fox_Prefab;
  public GameObject Bacteria_Prefab;
  public GameObject Waterdeer_Prefab;

  // 실질적인 오브젝트 배열(큐)
  public Queue<GameObject> Item_Power = new Queue<GameObject>();
  public Queue<GameObject> Item_Money = new Queue<GameObject>();
  public Queue<GameObject> Item_Coin = new Queue<GameObject>();
  public Queue<GameObject> Item_Life = new Queue<GameObject>();
  public Queue<GameObject> Item_Bomb = new Queue<GameObject>();

  public Queue<GameObject> Point = new Queue<GameObject>();
  public Queue<GameObject> SmallSph = new Queue<GameObject>();
  public Queue<GameObject> Bullet = new Queue<GameObject>();
  public Queue<GameObject> MediumSph = new Queue<GameObject>();
  public Queue<GameObject> LargeSph = new Queue<GameObject>();
  public Queue<GameObject> Star = new Queue<GameObject>();

  public GameObject[] Ameba = new GameObject[100];
  public GameObject[] Mite = new GameObject[100];
  public GameObject[] ChickenPigeon = new GameObject[50];
  public GameObject[] WakParrot = new GameObject[50];
  public GameObject[] Chimpanchee = new GameObject[10];
  public GameObject[] Neugeuza = new GameObject[10];
  public GameObject[] Boss = new GameObject[20];

  public GameObject[] Bidulgi = new GameObject[500];
  public GameObject[] Poop = new GameObject[100];
  public GameObject[] Bat = new GameObject[100];
  public GameObject Fox;
  public GameObject[] Bacteria = new GameObject[200];
  public GameObject[] Waterdeer = new GameObject[60];

  // 오브젝트 풀링을 위한 Enqueue 메소드
  void ObjectPooling(GameObject PrefabName, Queue<GameObject> QueueName)
  {
    GameObject newObj = Instantiate(PrefabName);
    QueueName.Enqueue(newObj);
    newObj.SetActive(false);
  }


  // 비활성화 메서드(Enqueue)
  public void Off(GameObject target)
  {
    target.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,0f,0f);
    target.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
    target.SetActive(false);

    if (target.CompareTag("Item"))
    {
      string TYPE = target.GetComponent<Item>().type;
      switch (TYPE)
      {
        case "Life":
          Item_Life.Enqueue(target);
          break;
        case "Money":
          Item_Money.Enqueue(target);
          break;
        case "Coin":
          Item_Coin.Enqueue(target);
          break;
        case "Power":
          Item_Power.Enqueue(target);
          break;
        case "Bomb":
          Item_Bomb.Enqueue(target);
          break;
      }
    }

    else if (target.CompareTag("EnemyBullet"))
    {
    }
  }

  // 비활성화된 Enemy 개체를 찾아 반환
  public GameObject MakeEnemy(string target)
  {
    GameObject[] type = Ameba;
    int length = 0;
    switch (target)
    {
    case "Ameba":
      type = Ameba;
      length = 100;
      break;
    case "Mite":
      type = Mite;
      length = 100;
      break;
    case "ChickenPigeon":
      type = ChickenPigeon;
      length = 50;
      break;
    case "WakParrot":
      type = WakParrot;
      length = 50;
      break;
    case "Chimpanchee":
      type = Chimpanchee;
      length = 10;
      break;
    case "Neugeuza":
      type = Neugeuza;
      length = 10;
      break;
    }
    for (int i = 0 ; i < length ; i++)
    {
      if (!type[i].activeSelf)
      { return type[i]; }
    }
    return null;
  }

  // 큐에서 아이템/적 탄환을 Dequeue하고 반환
  public GameObject MakeObj(string type)
  {
    switch (type)
    {
      case "Power":
        return Item_Power.Dequeue();
      case "Money":
        return Item_Money.Dequeue();
      case "Coin":
        return Item_Coin.Dequeue();
      case "Life":
        return Item_Life.Dequeue();
      case "Bomb":
        return Item_Bomb.Dequeue();

      case "Point":
        return Point.Dequeue();
      case "Bullet":
        return Bullet.Dequeue();
      case "SmallSph":
        return SmallSph.Dequeue();
      case "MediumSph":
        return MediumSph.Dequeue();
      case "LargeSph":
        return LargeSph.Dequeue();
      case "Star":
        return Star.Dequeue();
      default :
        return null;
    }
  }

  // 오브젝트 풀링
  void Start()
  {
    ObjectManager.Instance = this;

    for (int i = 0 ; i < 10 ; i++)
    {
      ObjectPooling(Item_Life_Prefab,Item_Life);
      ObjectPooling(Item_Bomb_Prefab,Item_Bomb);
      Chimpanchee[i] = Instantiate(Chimpanchee_Prefab);
      Chimpanchee[i].SetActive(false);
      Neugeuza[i] = Instantiate(Neugeuza_Prefab);
      Neugeuza[i].SetActive(false);
    }

    for (int i = 0 ; i < 50 ; i++)
    {
      ChickenPigeon[i] = Instantiate(ChickenPigeon_Prefab);
      ChickenPigeon[i].SetActive(false);
      WakParrot[i] = Instantiate(WakParrot_Prefab);
      WakParrot[i].SetActive(false);
    }

    for (int i = 0 ; i < 100 ; i++)
    {
      ObjectPooling(Item_Power_Prefab,Item_Power);
      ObjectPooling(Item_Money_Prefab,Item_Money);
      ObjectPooling(Item_Coin_Prefab,Item_Coin);
      Ameba[i] = Instantiate(Ameba_Prefab);
      Ameba[i].SetActive(false);
      Mite[i] = Instantiate(Mite_Prefab);
      Mite[i].SetActive(false);
    }
    for (int i = 0 ; i < 500 ; i++)
    {
      ObjectPooling(Point_Prefab,Point);
      ObjectPooling(SmallSph_Prefab,SmallSph);
      ObjectPooling(Bullet_Prefab,Bullet);
      ObjectPooling(MediumSph_Prefab,MediumSph);
      ObjectPooling(LargeSph_Prefab,LargeSph);
      ObjectPooling(Star_Prefab,Star);
    }
  }
}
