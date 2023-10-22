using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBreak : MonoBehaviour
{
	public bool breakable = false;
	NoiseMaker noise;
	BreakObject particleBreak;

	private void Start()
	{
		noise = GetComponent<NoiseMaker>();
		particleBreak = GetComponent<BreakObject>();
	}

	public void Break()
	{
		if (breakable)
		{
			noise.MakeNoise();
			particleBreak.Break();
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
