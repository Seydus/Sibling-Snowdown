using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField] private float snowballSpeed;
    [SerializeField] private float snowballLife;
    [SerializeField] private LayerMask obstacleMask;
    public string playerToHit { get; set; }
    private float snowballLifeTimer;

    public Vector2 Direction { get; set; }

    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        snowballLifeTimer = snowballLife;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Life();
    }

    public void Move()
    {
        myBody.velocity = Direction * snowballSpeed * Time.deltaTime;
    }

    public void Life()
    {
        if (snowballLifeTimer <= 0)
        {
            gameObject.SetActive(false);
            snowballLifeTimer = snowballLife;
        }
        else
        {
            snowballLifeTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacles" ||
            collision.tag == playerToHit)
        {
            gameObject.SetActive(false);
        }
    }
}
