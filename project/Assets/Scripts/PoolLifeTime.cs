using UnityEngine;

public class PoolLifeTime : MonoBehaviour
{
    public float lifeTime = 4f;
    public bool isFragment = false;

    void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    private void ReturnToPool()
    {
        if (isFragment)
        {
            Pooling.Instance.ReturnFragment(gameObject);
        }
        else
        {
            Pooling.Instance.ReturnEnemy(gameObject);
        }
    }
}