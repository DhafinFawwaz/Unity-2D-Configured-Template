<h1 align="center">Unity 2D Configured Template V5.0.0</h1>

Unity 2D Configured Template V5.0.0 is a unity template with some probably usefull features that can
always be used for any unity game project. It consist of a template scene that has implemented all the features below. With a few modification within Unity Engine, it can also be used for 3D game. Note that this project uses Microsoft C# naming convention. This is because Unity's own naming convention itself isn't consistent. Therefore, it may be better to stick with one. This is mainly a template for game UI. Also note that the API Reference below hasn't covered everything. In this version, there's already boiler plate for state machine and you can easily create a new state by right click Create/C# StateMachine/PlayerGeneralState and you can also choose Core or States. Also, [AnimationUI](https://github.com/DhafinFawwaz/Unity-AnimationUI "AnimationUI") is also integrated. 


## ✨ Features

- Flexible transition
- Scene load with Loading Screen
- Save data with encryption
- Audio settings including with option like fading when Scene Load
- Custom Button
- Custom Slider
- Both Custom Button and Slider is inherits the Selectable class which makes the Unity UI navigation compatible 
- Implement everything modular. It mainly uses Observer
- Boilerplate for StateMachine
- Others



## 🔍 API Reference

### 🪟 Scene Transition

Get the reference by

```csharp
[SerializeField] SceneTransition _sceneTransition;
void GoHome()
{
    _sceneTransition.MyMethodName();
}
```

#### 🔗 Syntax

| Method                                  | Description                        |
|:--------                                | :------------------------------    |
|`StartSceneTransition(string sceneName)` |Start the transition animation and load the scene |
|`StartTransitionWithoutLoadingScene()`   |Start the transition animation without loading scene|
|`SetDelayAfterOut(float delay)`          |Set the delay after the transition out animation finish, before the OnAfterOut|   
|`SetDelayBeforeIn(float delay)`          |Set the delay before the transition in animation start, before the OnBefore|   
|`SetOutDuration(float duration)`         |Set duration of the transition out animation |
|`SetInDuration(float duration)`          |Set duration of the transition in animation |
|`AddListenerAfterOut(Action action)`     |Add another method that will get called after transition out finish|   
|`AddListenerBeforeIn(Action action)`     |Add another method that will get called before transition in started |   
|`RemoveAllListener()`                    |remove both OnAfterOut and OnBefore |
|`OutAnimation()`                         |override for transition out animation|
|`InAnimation()`                          |override for transition in animation|


#### 📖 Examples

Transition To home menu, fade the music, Enable the music back.
Make sure to attach the SceneTransition prefab in `Level/Prefab/PresetPrefabs/Transition`
```csharp
[SerializeField] SceneTransition _sceneTransition;
void GoHome()
{
    Audio.MusicFadeOut(0.5f);
    _sceneTransition.StartSceneTransition("Home");
        .AddListenerBeforeIn(() => {
            Audio.SetMusicSourceVolume(1);
    })
}
```

Transition To home menu, wait 0.5 seconds then transition in.
```csharp
[SerializeField] SceneTransition _sceneTransition;
[SerializeField] GameObject _home;
void GoHome()
{
    _sceneTransition.StartTransitionWithoutLoadingScene()
        .SetDelayAfterOut(0.5f)
        .AddListenerAfterOut(ActivateHome);
}
void ActivateHome()
{
    _home.gameObject.SetActive(true);
}
```

### ⏳ SceneLoader
Get the reference by

```csharp
SceneLoader.MyMethodName();
```

#### 🔗 Syntax

| Method                                         | Description                       |
|:--------                                       |:------------------------------    |
|`LoadSceneWithProgressBar(string sceneName, int index = 0)`  |Load scene by string and chose the SceneTransition|

In the project view. navigate to `Level/Prefabs/PresetPrefabs/Resources/SINGLETON` and see `SceneHandler.cs` attached to `SceneLoader` gameobject. Assign the SceneTransition prefab there. For example the SceneTransition is in `Level/Prefabs/PresetPrefabs/Transition/ScreenWipeTransition`

#### 📖 Examples

Load the Home Scene with transtion in index 0 with the progress bar loading screen.
```csharp
SceneLoader.LoadSceneWithProgressBar("Home", 0);
```

### 💾 SaveData
Get the reference by

```csharp
Save.MyMethodName();
```

#### 🔗 Syntax

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`SaveData()`|Save the data of Save.data       |
|`LoadData()`|Load the save data into Save.data|

Modify `SaveData.cs` in `Assets\Code\PresetScripts\Save\SaveData.cs` however you like to fit the game. Change the value of `JSONEncryptedKey` in `Assets\Code\PresetScripts\Save\Save.cs` into any other random value with the same amount of digit. Note that this save system didn't use BinaryFormatter because of security risk according to Microsoft (Microsoft, 2022). The encryption uses Rijndael algorithm instead, see https://en.wikipedia.org/wiki/Advanced_Encryption_Standard.

#### 📖 Examples

Set the value of the highscore to 120 then save it.
```csharp
Save.Data.Highscore = 120;
Save.SaveData();
```
Set the value of the save data into newPlayerData then save it. Make sure to modify the `SaveData.cs`.
```csharp
Save newPlayerData = new Save();
newPlayerData.username = "chicken";
newPlayerData.exp = 50;
Save.Data = newPlayerData;
Save.SaveData();
```

### 🔊 Audio
Get the reference by

```csharp
Audio.MyMethodName();
```

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`PlayMusic(AudioClip audioClip)`|Play the audioClip music|
|`StopMusic()`|Stop the music and null it|
|`StopPlayMusic()`|Stop the music then play it again|
|`PlaySound(AudioClip audioClip)`|Play the audioClip sound|
|`PlaySound(AudioClip audioClip, float volume)`|Play the audioClip sound by volume|
|`PlaySound(int index)`|Play the audioClip sound by index|
|`PlaySoundWithTimeLimit(AudioClip clip, float timeLimit)`|Play the clip while also make sure it wont fire until timeLimit has elapsed before the previous call. Useful to optimize audio like when collecting hundreds of coins at the same time.|
|`MusicFadeOut(float duration)`|Fade out the music|
|`MusicFadeOutAndChangeTo(AudioClip _musicClip, bool isLooping, float duration, float delayBeforeChangeDuration)`|Fade out the music and change it to another music|
|`SetMusicSourceVolume(float t)`|Set the music volume normalized from 0 to 1. This is also whats changed by MusicFadeOutAndChangeTo and MusicFadeOut|
|`ToggleLoop(bool isLooping)`|Toggle whether the music should be looping or not|


Assign the list SFX you want in the prefab `Level/Prefabs/PresetPrefabs/Resources/SINGLETON`. Its the `AudioManager.cs` attached to the child of the SINGLETON prefab. You can also set the volume of the SFX.
Drop the `MusicLoader` prefab in `Assets\Level\Prefabs\PresetPrefabs` to the scene and replace the Music Clip in the inspector to any music you want. This will automatically replace the currently played music when entering this scene. If there's no music asigned to the MusicLoader, it will automatically stop the current playing music.

#### 📖 Examples

Play the epicImpactSFX.
```csharp
Audio.PlaySound(epicImpactSFX);
```

### 💻 Resolution
Get the reference by

```csharp
ResolutionManager.MyMethodName();
```

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`SetFullScreen(bool isFullScreen)`|Set whether the screen should be in fullscreen or not|

If you want to set the default resolution, Look at ResolutionManager.cs line 26 and modify the defaultWidth and defaultHeight. for example `int defaultWidth = Screen.currentResolution.width * 4/6;`

#### 📖 Examples

Set the screen into fullscreen.
```csharp
ResolutionManager.SetFullScreen(true);
```

### ⚙️ Settings
The settings for music volume, sound volume, resolution, and fullscreen has been made as a template. In the Unity Hierarchy, navigate into `Settings` and edit the prefab to however you like. The slider has been customized so that it will get animated when changing the slider value. There's also a customly made button that will be scaled when an event like hover occurs. If you want to add soundeffect to the button, just add the ButtonUI.PlaySound(AudioClip audioClip) one of the unity event. In `SettingsManager.cs` modify `OnMusicValueChanged(float newVal)`, `OnSoundValueChanged(float newVal)`, `SetFullScreen(bool isFullScreen)`, or `OnResolutionValueChanged(int resolutionIndex)` if you want to add something for example a soundeffect.

## 📝 License
[MIT](https://choosealicense.com/licenses/mit/)

## 📑 Reference
Microsoft, M. (2022, October 28). Deserialization risks in use of BinaryFormatter and related types. Microsoft Learn. Retrieved December 12, 2022, from https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide

