using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource backgroundMusic;

    void Start()
    {
        // Set the slider to match the current volume
        volumeSlider.value = backgroundMusic.volume;

        // Add a listener to adjust the volume when the slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        backgroundMusic.volume = value;
    }
}
