using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu{
	public class UISettingsManager : MonoBehaviour {

		public enum Platform {Desktop};
		public Platform platform;

		[Header("GAME SETTINGS")]
		public GameObject tooltipstext;

		// sliders
		public GameObject musicSlider;
		public GameObject sensitivityXSlider;
		public GameObject sensitivityYSlider;

		private float sliderValueXSensitivity = 0.0f;
		private float sliderValueYSensitivity = 0.0f;
		private float sliderValueSmoothing = 0.0f;


        public void  Start (){
			musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
			sensitivityXSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("XSensitivity");
			sensitivityYSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("YSensitivity");

			// check tool tip value
			if(PlayerPrefs.GetInt("ToolTips")==0){
				tooltipstext.GetComponent<TMP_Text>().text = "desligado";
			}
			else{
				tooltipstext.GetComponent<TMP_Text>().text = "ligado";
			}
		}

		public void Update() { 
            sliderValueXSensitivity = sensitivityXSlider.GetComponent<Slider>().value;
			sliderValueYSensitivity = sensitivityYSlider.GetComponent<Slider>().value;
		}
        public void MusicSlider (){
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
		}

		public void SensitivityXSlider (){
			PlayerPrefs.SetFloat("XSensitivity", sliderValueXSensitivity);
		}

		public void SensitivityYSlider (){
			PlayerPrefs.SetFloat("YSensitivity", sliderValueYSensitivity);
		}

		public void SensitivitySmoothing (){
			PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
			Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
		}

		public void ToolTips (){
			if(PlayerPrefs.GetInt("ToolTips")==0){
				PlayerPrefs.SetInt("ToolTips",1);
				tooltipstext.GetComponent<TMP_Text>().text = "ligado";
			}
			else if(PlayerPrefs.GetInt("ToolTips")==1){
				PlayerPrefs.SetInt("ToolTips",0);
				tooltipstext.GetComponent<TMP_Text>().text = "desligado";
			}
		}
	}
}