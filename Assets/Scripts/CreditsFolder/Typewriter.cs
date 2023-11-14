using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typewriter : MonoBehaviour
{

	Text _text;
	string writer;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

	[HideInInspector]
	public CreditManager creditsManager;

	void Start()
	{
		creditsManager = GameObject.Find("CreditsManager").GetComponent<CreditManager>();

		_text = GetComponent<Text>()!;

		if (_text != null)
		{
			writer = _text.text;
			_text.text = "";

			StartCoroutine("TypeWriterText");
		}
	}

	IEnumerator TypeWriterText()
	{
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);


		foreach (char c in writer)
		{
			if (_text.text.Length > 0)
			{
				_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
			}
			_text.text += c;
			_text.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
		}

		/*
		if (production)
		{
			creditsManager.ProductionNext(creditsNumber);
		}
		else if (design)
		{
			creditsManager.DesignNext(creditsNumber);
		}
		else if (art)
		{
			creditsManager.ArtNext(creditsNumber);
		}
		else if (engineering)
		{
			creditsManager.EngineeringNext(creditsNumber);
		}
		*/
	}
}