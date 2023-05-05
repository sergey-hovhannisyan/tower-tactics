using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float loopDelay = 1f;

    private float timer;
    private bool isPlaying;

    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (particleSystem != null)
        {
            particleSystem.Stop();
            timer = 0f;
            isPlaying = false;
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;

            if (timer >= loopDelay + particleSystem.main.duration)
            {
                timer = 0f;
                particleSystem.Play();
                audioSource.PlayOneShot(audioClip);
            }
        }
    }

    public void TurnOnParticleSystem(){
        if (!isPlaying){
        isPlaying = true;
        timer = 0f;
        particleSystem.Play();
        audioSource.PlayOneShot(audioClip);
        }
    }

    public void TurnOffParticleSystem(){
        isPlaying = false;
        particleSystem.Stop();
    }
}
