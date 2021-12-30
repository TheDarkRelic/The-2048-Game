using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource slideSource;

    public void PlaySlideSound()
    {
        if (slideSource == null) { return; }
        slideSource.Play();
    }
}
