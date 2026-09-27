using UnityEngine;

public class EnemySplit : MonoBehaviour
{
    public int fragmentCount = 2;
    public float dispersionForce = 5f;

    public void Explode()
    {
        SpawnFragments();
        Pooling.Instance.ReturnEnemy(gameObject);
    }

    private void SpawnFragments()
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            GameObject fragment = Pooling.Instance.GetFragment();
            fragment.transform.position = transform.position;
            fragment.transform.rotation = Quaternion.identity;

            Rigidbody _rigidbody = fragment.GetComponent<Rigidbody>();

            if (_rigidbody != null)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;

                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
                _rigidbody.AddForce(direction * dispersionForce, ForceMode.Impulse);
            }
        }
    }
}