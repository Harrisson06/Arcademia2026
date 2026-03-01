using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private int damage = 10;

    private Transform player;
    private bool isStunned = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null ) return;
        if (!isStunned) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
        transform.right = player.position - transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Vitals>().TakeHealth(damage);
            StartCoroutine(StunEnemy());
        }
    }

    private IEnumerator StunEnemy()
    {
        isStunned = true;
        yield return new WaitForSeconds(0.5f);
        isStunned = false;
    }
}