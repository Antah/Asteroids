using UnityEngine;

public class Ship : MonoBehaviour
{
    public Transform bulletsParent;
    public Transform bulletStart;
    public GameObject bullet;
    public GameObject renderZone;
    private float thrust = 50f;
    private float rotationSpeed = 300f;
    private float MaxSpeed = 40f;
    private Rigidbody2D rb;

    private void Start()
    {
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        bullet.SetActive(false);
    }

    private void FixedUpdate()
    {
        ControlRocket();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            Shoot();
        }
    }

    private void ControlRocket()
    {
        transform.Rotate(0, 0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rb.AddForce(transform.up * thrust * Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), Mathf.Clamp(rb.velocity.y, -MaxSpeed, MaxSpeed));
    }


    public void ResetRocket()
    {
        while (bulletsParent.childCount > 0) {
            var child = bulletsParent.GetChild(0);
            Destroy(child.gameObject);
            child.SetParent(null);
        }
        rb.simulated = true;
        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0, 180f, 0);
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    void Shoot()
    {
        if (!GameManager.Instance.gameInProgress)
            return;
        GameObject BulletClone = Instantiate(bullet, new Vector2(bulletStart.position.x, bulletStart.position.y), transform.rotation);
        BulletClone.transform.parent = bulletsParent;
        BulletClone.SetActive(true);
        BulletClone.GetComponent<Bullet>().KillOldBullet();
        BulletClone.GetComponent<Rigidbody2D>().AddForce(transform.up * 3000);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.GetComponent<AsteroidData>() != null)
        {
            rb.simulated = false;
            foreach (Transform child in bulletsParent)
            {
                child.GetComponent<Bullet>().GetComponent<Rigidbody2D>().simulated = false;
            }
            GameManager.Instance.GameOver();
        }
    }
}
