using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Type;
        public GameObject Prefab;
        public int Size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update

    public static ObjectPooler Instance;
    private void Awake()
    {
        //Simple singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    void Start()
    {
        

    }

    public void NewPool(int size, string type, GameObject prefab)
    {
        Pool pool = new Pool
        {
            Type = type,
            Prefab = prefab,
            Size = size
        };

        pools.Add(pool);//solo se veria desde el inspector
        
        poolDictionary.Add(pool.Type, PrewarmPool(pool));
        Debug.Log("pool created " + pool.Type);


    }

    public Queue<GameObject> PrewarmPool(Pool pool)//dar un return tipo queue<>?
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < pool.Size; i++)
        {
            GameObject obj = Instantiate(pool.Prefab);
            obj.transform.position = new Vector3(0, 0, 0);
            obj.SetActive(false);

            obj.transform.SetParent(this.transform);//orden en la jerarquia

            objectPool.Enqueue(obj);//adds objets to the que
        }
        return objectPool;
    }

    public GameObject SpawnFromPool(string type, Vector3 pos)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.Log("Invalid Type " + type);
            return null;
        }
        GameObject objectToSpawn = poolDictionary[type].Dequeue();//saca el primer elemento de la lista

        objectToSpawn.transform.position = pos;
        objectToSpawn.SetActive(true);

        poolDictionary[type].Enqueue(objectToSpawn);//pone el elemento como el ultimo en la lista

        return objectToSpawn;
    }
}
