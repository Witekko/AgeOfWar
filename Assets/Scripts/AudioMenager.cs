using UnityEngine;

public class AudioMenager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("--------Audio Source--------")]
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource SFXSource;

    [Header("--------Audio Source--------")]
    public AudioClip background;

    public AudioClip click;
    public AudioClip death;
    public AudioClip sword;
    public AudioClip mage;
    public AudioClip swoosh;
    public AudioClip hit;

    protected void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}