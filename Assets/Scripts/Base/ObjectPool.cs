using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectPool<T> where T : Component
{
    [SerializeField] T prefab;
    private Transform parent;
    [SerializeField] int size;
    private Queue<T> pool;
    public void Initialize(Transform parent)
    {
        pool = new Queue<T>();
        this.parent = parent;
        for (int i = 0; i < size; i++)
        {
            pool.Enqueue(Copy());
        }
    }
    private T Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);
        copy.gameObject.SetActive(false);
        return copy;
    }
    private T GetObject()
    {
        T availableObject = null;
        if (pool.Count > 0 && !pool.Peek().gameObject.activeSelf)
            availableObject = pool.Dequeue();
        else
            availableObject = Copy();
        pool.Enqueue(availableObject);
        return availableObject;
    }
    public T PrepareObject()
    {

        T prepareObject = GetObject();
        prepareObject.gameObject.SetActive(true);
        return prepareObject;
    }
    public T PrepareObject(Vector3 pos)
    {

        T prepareObject = GetObject();
        prepareObject.gameObject.SetActive(true);
        prepareObject.transform.position = pos;
        return prepareObject;
    }
    public T PrepareObject(Vector3 pos, Quaternion rotation)
    {
        T prepareObject = GetObject();
        prepareObject.gameObject.SetActive(true);
        prepareObject.transform.position = pos;
        prepareObject.transform.rotation = rotation;
        return prepareObject;
    }
    public T PrepareObject(Vector3 pos, Quaternion rotation, Vector3 localScale)
    {
        T prepareObject = GetObject();
        prepareObject.gameObject.SetActive(true);
        prepareObject.transform.position = pos;
        prepareObject.transform.rotation = rotation;
        prepareObject.transform.localScale = localScale;
        return prepareObject;
    }
    public T PrepareObject(Vector3 pos, Vector3 localScale)
    {

        T prepareObject = GetObject();
        prepareObject.gameObject.SetActive(true);
        prepareObject.transform.position = pos;
        prepareObject.transform.localScale = localScale;
        return prepareObject;
    }
}
