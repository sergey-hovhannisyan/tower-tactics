using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float totalHealth = 20;
    public float currentHealth;
    private Bullet bullet;
    private float frameRate;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet")){
            bullet = other.gameObject.GetComponent<Bullet>();
            currentHealth-= bullet.damage;
            checkIfDead();
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("DamageOverTime")){
            frameRate = (1.0f / Time.deltaTime);
            Debug.Log(frameRate);
            Debug.Log(other.GetComponent<DamageOverTime>().dps / frameRate);
            currentHealth -= (other.GetComponent<DamageOverTime>().dps / frameRate);
            checkIfDead();
        }
    }

    void checkIfDead(){
        if(currentHealth <= 0){
            gameObject.GetComponent<CharacterMovements>().isDead = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
