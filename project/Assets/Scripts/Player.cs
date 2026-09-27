using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int SCORE = 0;
    public static float xBorderLimit, yBorderLimit;

    public float thrustForce = 5f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;

    private Rigidbody _rigidbody;
    Vector2 thrustDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
        yBorderLimit = Camera.main.orthographicSize + 1;
    }

    private void FixedUpdate()
    {
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.fixedDeltaTime;
        float thrust = Input.GetAxis("Thrust") * thrustForce;

        thrustDirection = transform.right;
        transform.Rotate(Vector3.forward, -rotation);

        _rigidbody.AddForce(thrustDirection * thrust);
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;

        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;

        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            
            GameObject bullet = Pooling.Instance.GetBullet();
            bullet.transform.position = gun.transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}
