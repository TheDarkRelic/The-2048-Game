using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource slideSource;
    [SerializeField] AudioSource tileBreakSource;

    public void PlaySlideSound()
    {
        if (slideSource == null) { return; }
        slideSource.Play();
    }

    public void PlayTileBreakSound()
    {
        if (tileBreakSource == null) { return; }
        tileBreakSource.Play();
    }
}
