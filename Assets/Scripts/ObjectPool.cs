using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool _Instance;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize = 5;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        _pool.Enqueue(obj);
    }
}
