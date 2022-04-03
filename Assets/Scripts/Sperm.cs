using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sperm : MonoBehaviour
{
    public ParticleSystem impact;
    public GameObject egg;
    public float speed;
    public GameManager gm;
    public Rigidbody2D rb;
    private Vector2 dir;
    private int bounceAmount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = egg.transform.position - transform.position;
        dir.Normalize();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (bounceAmount > 0)
            {
                impact.Play();
                dir = Vector2.Reflect(dir, collision.contacts[0].normal);
                dir.Normalize();
                speed = Random.Range(2, 3.5f);
                bounceAmount--;
            }
            else
            {
                impact.Play();
                dir = egg.transform.position - transform.position;
                dir.Normalize();
                speed = Random.Range(2, 3.5f);
            }
            
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
            dir.Normalize();
            speed = Random.Range(2, 3.5f);
            bounceAmount = Random.Range(0, 3);
        }
        else if (collision.gameObject.CompareTag("Sperm"))
        {
            impact.Play();
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
            dir.Normalize();
            speed = 3.5f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            gm.StartCoroutine(gm.GameOver());
        }
    }
    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);
        rb.velocity = dir * speed;
    }
}
