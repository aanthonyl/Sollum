using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBreak : MonoBehaviour
{
	NoiseMaker noise;
	BreakObject particleBreak;

	private void Start()
	{
		noise = GetComponent<NoiseMaker>();
		particleBreak = GetComponent<BreakObject>();
	}

	public void Break()
	{
		noise.MakeNoise();
		particleBreak.Break();
	}
}
