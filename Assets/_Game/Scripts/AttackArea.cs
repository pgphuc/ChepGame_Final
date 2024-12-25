using System;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            collision.GetComponent<Character>().OnHit(30f);
        }
    }
}
