using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject HitVFX;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit()
    {
        rb.linearVelocity = transform.right * 5f;
        Invoke("OnDespawn", 3f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHit(30f);
            Instantiate(HitVFX, transform.position,transform.rotation);
            OnDespawn();
        }
    }
}
