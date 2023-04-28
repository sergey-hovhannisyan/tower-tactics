using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("enemy")){
            Destroy(gameObject);
        }
    }
}
