using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    private Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject uiObject = GameObject.FindGameObjectWithTag("UI");
        scoreText = uiObject.GetComponent<Text>();
    }

    void OnEnable()
    {
        Invoke(nameof(Deactivate), maxLifeTime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
{
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore(1);

            EnemySplit splitScript = collision.gameObject.GetComponent<EnemySplit>();
            splitScript.Explode();
            Deactivate(); // Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Fragment"))
        {
            IncreaseScore(3);

            Pooling.Instance.ReturnFragment(collision.gameObject);
            Deactivate(); // Destroy(gameObject);
        }
    }

    private void IncreaseScore(int score)
    {
        Player.SCORE += score;
        scoreText.text = "Points: " + Player.SCORE;
    }

    private void Deactivate()
    {
        Pooling.Instance.ReturnBullet(gameObject);
    }
}
