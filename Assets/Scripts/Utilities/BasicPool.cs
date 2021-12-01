using UnityEngine;

public class BasicPool<T1, T2> : MonoBehaviour where T1 : BasicPool<T1, T2> where T2 : class
{
    [SerializeField] private GameObject obj = null;
    [SerializeField] private int instancesNumber = 30;

    private static T1 instance = null;
    public static T1 Instance { get => instance; set => instance = value; }
    public int PoolIndex { get; private set; } = 0;

    [SerializeField] T2[] items = null;

    public T2 currentItem
    {
        get
        {
            PoolIndex = PoolIndex >= items.Length ? 0 : PoolIndex;
            PoolIndex++;            
            return items[PoolIndex - 1];
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this as T1;
        else
            Destroy(this as T1);

        InitPool();
    }

    void InitPool()
    {
        items = new T2[instancesNumber];
        for(int i = 0; i < instancesNumber; i++)
        {
            GameObject go = Instantiate(obj, Vector3.zero, Quaternion.identity);
            items[i] = go.GetComponent<T2>();
            go.SetActive(false);
        }
    }
}
