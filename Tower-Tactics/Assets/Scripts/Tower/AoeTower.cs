using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeTower : MonoBehaviour
{
    public int damamge = 5;
    private void OnParticleCollision(GameObject other) {
        // Debug.Log("here");
        if (other.tag == "enemy"){
            Debug.Log("here");
        }
    }
}
