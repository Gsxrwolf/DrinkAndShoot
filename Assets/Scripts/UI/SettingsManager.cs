using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderUI;

    private void Start()
    {
        sliderMaster.value = GameManager.Instance.saveFile.volumeMaster;
        sliderMusic.value = GameManager.Instance.saveFile.volumeMusic;
        sliderSFX.value = GameManager.Instance.saveFile.volumeSFX;
        sliderUI.value = GameManager.Instance.saveFile.volumeUI;

    }
    public void OnVolumeChange(string _name)
    {
        Slider slider = null;
        switch (_name)
        {
            case "Master":
                {
                    slider = sliderMaster;
                    break;
                }
            case "Music":
                {
                    slider = sliderMusic;
                    break;
                }
            case "SFX":
                {
                    slider = sliderSFX;
                    break;
                }
            case "UI":
                {
                    slider = sliderUI;
                    break;
                }
        }
        float value = slider.value;
        if (slider.value == slider.minValue)
        {
            value = -80;
        }
        AudioManager.Instance.SetVolume(_name, value);
    }
}
