using System;
using UnityEngine;

public class NoiseEvents : Singleton<NoiseEvents>
{
    public event EventHandler<OnNoiseMadeArgs> OnNoiseMade;
    private void Awake()
    {
        SingletonBuilder(this);
    }

    public class OnNoiseMadeArgs : EventArgs
    {
        public Transform noiseTrans;
    }

    public void MakeNoise(Transform thisNoise)
    {
        // Debug.Log("Button Registered");
        OnNoiseMade?.Invoke(this, new OnNoiseMadeArgs { noiseTrans = thisNoise });
    }
}
