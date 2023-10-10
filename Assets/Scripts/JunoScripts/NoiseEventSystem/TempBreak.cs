using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBreak : MonoBehaviour
{
	bool breakable = false;
	NoiseMaker noise;
	BreakObject particleBreak;

	private void Start()
	{
		noise = GetComponent<NoiseMaker>();
		particleBreak = GetComponent<BreakObject>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && breakable)
		{
			particleBreak.Break();
			noise.MakeNoise();
			// this.gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			breakable = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			breakable = true;
		}
	}
}
