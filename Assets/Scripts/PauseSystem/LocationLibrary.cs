/*
    Script Added by Aurora Russell
	10/08/2023
	// LOCATION LIBRARY MAKES SCRIPTABLE OBJECT OPTION //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LocationLibrary : ScriptableObject
{
    // TO BE USED AS AN INPUT IN THE PauseManager.cs SCRIPT

    public List<SpriteInfo> locationSpriteList = new List<SpriteInfo>();

    [System.Serializable]
    public class SpriteInfo
    {
        public string name;
        public Sprite sprite;
    }
}
