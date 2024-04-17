using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class WoodClipPool : Singleton<WoodClipPool>
    {
        [SerializeField] ParticleSystem prefab;
        [SerializeField] int size;
        private Queue<ParticleSystem> pool;
        // Start is called before the first frame update
        void Start()
        {
            pool = new Queue<ParticleSystem>();
            for (int i = 0; i < size; i++)
            {
                pool.Enqueue(Copy());
            }
        }
        private ParticleSystem Copy()
        {
            var copy = GameObject.Instantiate(prefab, transform);
            copy.gameObject.SetActive(false);
            return copy;
        }
        private ParticleSystem GetObject()
        {
            ParticleSystem availableObject = null;
            if (pool.Count > 0 && pool.Peek().particleCount == 0)
                availableObject = pool.Dequeue();
            else
                availableObject = Copy();
            pool.Enqueue(availableObject);
            return availableObject;
        }
        public ParticleSystem PrepareObject(Vector2 pos)
        {

            ParticleSystem prepareObject = GetObject();
            prepareObject.transform.position = pos;
            prepareObject.gameObject.SetActive(true);
            prepareObject.Play();
            return prepareObject;
        }
    }
}