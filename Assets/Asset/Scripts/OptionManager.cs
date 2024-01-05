using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionManager : MonoBehaviour
{
	[Header("Volme")]
	public Slider volumeSlider;
	public TMP_Text volumeValue;
	public AudioSource musicSource;
	public Button applyButton;

	private const string volumePrefKey = "musicVolume";

	[Header("Quality")]
	public TMP_Dropdown qualityDropdown;

	private const string qualityPrefKey = "qualityLevel";


	void Awake()
	{
		// Load saved volume value
		float savedVolume = PlayerPrefs.GetFloat(volumePrefKey, 0.5f); // Default to 0.5 if not saved
		volumeSlider.value = savedVolume;
		musicSource.volume = savedVolume;

		// Update volume text initially
		UpdateVolumeValue();
		
		// Load saved quality level
		int savedQuality = PlayerPrefs.GetInt(qualityPrefKey, 1); // Default to medium (1)
		qualityDropdown.value = savedQuality;
		QualitySettings.SetQualityLevel(savedQuality);
	}


	void Update()
	{
		// Update volume on slider change
		if (volumeSlider.value != musicSource.volume)
		{
			musicSource.volume = volumeSlider.value;
			UpdateVolumeValue();
		}
	}


	public void OnApplyButtonClick()
	{
		PlayerPrefs.SetFloat(volumePrefKey, volumeSlider.value); // Save volume on button click
		
		int selectedQuality = (int)qualityDropdown.value;
		QualitySettings.SetQualityLevel(selectedQuality);
		PlayerPrefs.SetInt(qualityPrefKey, selectedQuality); // Save quality on button click
	}


	void UpdateVolumeValue()
	{
		int volumePercent = (int)(volumeSlider.value * 100);
		volumeValue.text = volumePercent + "%";
	}
	
	
}
