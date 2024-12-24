using System;
using UnityEngine;

public class SavePos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().SavePos();
        }
    }
}
