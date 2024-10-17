using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum BulletType { fire, ice, light }

public class ObjectPooling : MonoBehaviour
{
    //public static ObjectPooling instance;
    //public List<GameObject> pooledObjects;
    //public GameObject objectToPool;
    //public int amountToPool;

    //private void Awake()
    //{
    //    instance = this;
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    pooledObjects = new List<GameObject>();
    //    GameObject newObject;

    //    for(int i =0; i<amountToPool; i++)
    //    {
    //        newObject = Instantiate(objectToPool);
    //        newObject.transform.SetParent(gameObject.transform);
    //        newObject.SetActive(false);
    //        pooledObjects.Add(newObject);
    //    }
    //}

    //public GameObject GetPooledObject()
    //{
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            return pooledObjects[i];
    //        }
    //    }

    //    return null;
    //}

    public static ObjectPooling instance;

    // Dictionary để lưu các pool của từng loại đạn
    public Dictionary<string, List<GameObject>> bulletPools;
    private Dictionary<string, GameObject> bulletPrefabs;

    public GameObject[] objectToPool;

    void Awake()
    {
        instance = this;

        // Khởi tạo các Dictionary
        bulletPools = new Dictionary<string, List<GameObject>>();
        bulletPrefabs = new Dictionary<string, GameObject> ();

        // Thêm các prefab của từng loại đạn
        // Bạn có thể thêm từng loại đạn ở đây, ví dụ như fireBullet, iceBullet, v.v.
        //AddBulletType("FireBullet", objectToPool[0]);
        //AddBulletType("IceBullet", objectToPool[1]);
    }

    // Phương thức để thêm loại đạn mới
    public void AddBulletType(string bulletType, GameObject bulletPrefab, int poolSize)
    {
        bulletPrefabs.Add(bulletType, bulletPrefab);
        List<GameObject> newPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(false);
            newPool.Add(obj);
        }

        bulletPools.Add(bulletType, newPool);
    }

    // Phương thức lấy đạn từ pool
    public GameObject GetBullet(string bulletType)
    {
        if (bulletPools.ContainsKey(bulletType))
        {
            List<GameObject> pool = bulletPools[bulletType];
            foreach (GameObject bullet in pool)
            {
                if (!bullet.activeInHierarchy)
                {
                    return bullet;
                }
            }
        }
        return null;
    }

    // Phương thức để tạo thêm đạn nếu pool hết
    public void ExpandPool(string bulletType)
    {
        if (bulletPrefabs.ContainsKey(bulletType))
        {
            List<GameObject> pool = bulletPools[bulletType];
            GameObject newBullet = Instantiate(bulletPrefabs[bulletType]);
            newBullet.transform.SetParent(gameObject.transform);
            newBullet.SetActive(false);
            pool.Add(newBullet);
        }
    }
}
