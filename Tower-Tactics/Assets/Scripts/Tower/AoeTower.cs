using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeTower : MonoBehaviour
{
    public int dps = 5;
    private void OnParticleCollision(GameObject other) {
        if (other.tag == "enemy"){
            Debug.Log("here");
        }
    }
}
