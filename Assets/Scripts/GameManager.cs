using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables

        #region Singleton

            public static GameManager instance = null;

        #endregion
    
        #region DEBUG

            [HideInInspector] public SceneLoader.Scene currentScene;   
            [HideInInspector] public bool continueGame = false;
            [HideInInspector] public bool dataSaved = false;
            [HideInInspector] public bool dataLoaded = false;
            [HideInInspector] public bool levelTransition = false;
        
        #endregion

        #region Components

            private SceneLoader sceneLoader;
            private GameData gameData;
            private MainMenuData mainMenuData;

        #endregion

        #region Data
        
            [HideInInspector] public SceneLoader.Scene savedScene;
            [HideInInspector] public bool enableContinue;
        
        #endregion
    
    #endregion
    
    #region Built-in Methods
    
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            sceneLoader = SceneLoader.instance;
        }

        private void Start()
        {
            currentScene = SceneLoader.Scene.MainMenu;
            sceneLoader.GameSetup();
        }
    
    #endregion
    
    #region Custom Methods
    
        #region Save System

            public void SaveGame()
            {
                SaveGameData();
            }

            public void LoadGame()
            {
                if(!levelTransition)
                {
                    if(continueGame)
                    {
                        LoadGameData();

                        if(continueGame)
                        {
                            if(gameData != null)
                            {
                                StartCoroutine(LoadGameDataCoroutine());
                            }
                            else
                            {
                                dataLoaded = true;
                            }
                        }
                        else
                        {
                            dataLoaded = true;
                        }
                    }
                    else
                    {
                        DeleteGameData();
                        dataLoaded = true;
                    }
                }
                else
                {
                    dataLoaded = true;
                }
            }

            public void EnableContinue()
            {
                if(enableContinue)
                {
                    MainMenu.instance.continueButton.SetActive(true);
                }
            }

        #endregion
        
        #region Scene Management

            public void UnloadPreviousScene()
            {
                sceneLoader.UnloadScene(currentScene);
            }

            public void LoadMainMenu()
            {
                sceneLoader.Load(SceneLoader.Scene.MainMenu);
            }

            public void LoadGameWorld(bool newGame, SceneLoader.Scene scene)
            {
                continueGame = !newGame;
                sceneLoader.Load(scene);
            }

        #endregion

        #region Game Data
            
            public void SaveGameData()
            {
                if(!levelTransition)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    string path = Application.persistentDataPath + "/game.data";

                    FileStream fileStream = new FileStream(path, FileMode.Create);
                    gameData = new GameData(true);

                    binaryFormatter.Serialize(fileStream, gameData);
                    fileStream.Close();
                }
                else
                {
                    DeleteGameData();
                }

                dataSaved = true;
            }

            public void LoadGameData()
            {
                string path = Application.persistentDataPath + "/game.data";

                if(File.Exists(path))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    FileStream fileStream = new FileStream(path, FileMode.Open);

                    try
                    {
                        gameData = (GameData)binaryFormatter.Deserialize(fileStream);
                    }
                    catch
                    {
                        gameData = new GameData(false);
                        continueGame = false;
                    }

                    fileStream.Close();
                }
                else
                {
                    gameData = new GameData(false);
                    continueGame = false;
                }
            }

            public void DeleteGameData()
            {
                string path = Application.persistentDataPath + "/game.data";

                if(File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch { }
                }
            }

        #endregion

        #region Main Menu Data

            public void SaveMainMenuData()
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                string path = Application.persistentDataPath + "/menu.data";

                FileStream fileStream = new FileStream(path, FileMode.Create);
                mainMenuData = new MainMenuData(true);

                binaryFormatter.Serialize(fileStream, mainMenuData);
                fileStream.Close();

                dataSaved = true;
            }

            public void LoadMainMenuData()
            {
                string path = Application.persistentDataPath + "/menu.data";
                bool dataDeserialized = true;

                if(File.Exists(path))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    FileStream fileStream = new FileStream(path, FileMode.Open);

                    try
                    {
                        mainMenuData = (MainMenuData)binaryFormatter.Deserialize(fileStream);
                    }
                    catch
                    {
                        mainMenuData = new MainMenuData(false);
                        dataDeserialized = false;
                    }

                    fileStream.Close();
                }
                else
                {
                    mainMenuData = new MainMenuData(false);
                    dataDeserialized = false;
                }

                if(dataDeserialized)
                {
                    // LOADS MAIN MENU DATA
                    savedScene = mainMenuData.savedScene;
                    enableContinue = mainMenuData.enableContinue;
                    EnableContinue();
                }

                dataLoaded = true;
            }

            public void DeleteMenuData()
            {
                string path = Application.persistentDataPath + "/menu.data";

                if(File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch { }
                }
            }

        #endregion

    #endregion

    #region Coroutines

        private IEnumerator LoadGameDataCoroutine()
        {
            // LOADS GAME DATA

            yield return null;

            dataLoaded = true;
        }

    #endregion
}