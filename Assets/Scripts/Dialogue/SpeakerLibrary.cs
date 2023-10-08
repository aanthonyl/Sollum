/*
    Script Added by Aurora Russell
	09/30/2023
	// SPEAKER LIBRARY MAKES SCRIPTABLE OBJECT OPTION //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpeakerLibrary : ScriptableObject
{
    // TO BE USED AS AN INPUT IN THE DialogueManager.cs SCRIPT

    public List<SpriteInfo> speakerSpriteList = new List<SpriteInfo>();

    [System.Serializable]
    public class SpriteInfo
    {
        public string name;
        public Sprite sprite;
    }
}
