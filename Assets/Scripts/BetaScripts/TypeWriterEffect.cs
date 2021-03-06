using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour {

	public float delay = 0.2f;
	public string fullText;
	private string currentText = "";
	//public AudioClip blip;
	//AudioSource audioSauce;

	// Use this for initialization
	void Start () 
	{
		//audioSauce = GetComponent<AudioSource>();
		StartCoroutine(ShowText());
	}
	
	IEnumerator ShowText()
	{
		yield return new WaitForSeconds(9f);
		for (int i = 0; i < fullText.Length; i++)
		{
			currentText = fullText.Substring(0,i);
			this.GetComponent<Text>().text = currentText;
			//audioSauce.PlayOneShot(blip, 0.5f);
			yield return new WaitForSeconds(delay);
		}
	}
}
