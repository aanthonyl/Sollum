using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]

public class ResetGame : Editor
{
    #region Built-in Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameManager gameManager = (GameManager)target;

            EditorGUILayout.Space();

            if(GUILayout.Button("Reset Game"))
            {
                gameManager.DeleteGameData();
                gameManager.DeleteMenuData();
            }
        }

    #endregion
}