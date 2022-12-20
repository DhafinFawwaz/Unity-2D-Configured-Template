<h1 align="center">Unity 2D Configured Template V3.0.0</h1>

Unity 2D Configured Template V3.0.0 is a unity template with some probably usefull features that can
always be used for any unity game project. It consist of a template scene that has implemented all the features below. With a few modification within Unity Engine, it can also be used for 3D game. Note that this project uses Microsoft C# naming convention. This is because Unity's own naming convention itself isn't consistent. Therefore, it may be better to stick with one. This is mainly a template for game UI. Also note that the API Reference below hasn't covered everything. In this version, [AnimationUI](https://github.com/DhafinFawwaz/Unity-AnimationUI "AnimationUI") is also integrated. Now the LoadScene sequence have option to loadscene with the loading bar.


## ✨ Features

- Flexible transition
- Scene load with Loading Screen
- Save data with encryption
- Audio settings including with option like fading when Scene Load
- Custom button
- Custom Slider
- Others



## 🔍 API Reference

### 🪟 Transition

Get the reference by

```csharp
Singleton.Instance.Transition.MyMethodName();
```

#### 🔗 Syntax

| Method                            | Description                        |
|:--------                          | :------------------------------    |
|`Out()`                            |Start the transition out animation  |
|`SetOutDefault()`                  |Set the transition out parameters to the default values|
|`SetDelayBeforeOut(float t)`       |Set the delay before the transition out animation starts, before the added/set function by t|
|`SetDelayAfterOut(float t)`        |Set the delay after the transition out animation ends, before the added/set function by t|
|`SetOutStart(delegate func)`       |Set the method that will get called when transition out started    |
|`SetOutEnd(delegate func)`         |Set the method that will get called when transition out ended      |
|`AddOutStart(delegate func)`       |Add another method that will get called when transition out started|
|`AddOutEnd(delegate func)`         |Add another method that will get called when transition out ended  |
|`In()`                             |Start the transition in animation   |
|`SetInDefault()`                   |Set the transition in parameters to the default values|
|`SetDelayBeforeIn(float t)`        |Set the delay before the transition in animation start, before the added/set function by t|
|`SetDelayAfterIn(float t)`        |Set the delay after the transition In animation ends, before the added/set function by t|
|`SetInStart(delegate func)`        |Set the method that will get called when transition in started     |
|`SetInEnd(delegate func)`          |Set the method that will get called when transition in ended       |
|`AddInStart(delegate func)`        |Add another method that will get called when transition in started |
|`AddInEnd(delegate func)`          |Add another method that will get called when transition in ended   |
|`SetMusicFade(bool b)`             |Set whether the music will fade during the transition out|


In the Unity Hierarchy, navigate to `Singleton/Transition`. Inside `TransitionAnimation.cs`, change the animation the way you want there with `OutAnimation() and by editing its child`.

#### 📖 Examples

Transition out without fading the music, call `ActivateMainMenuCanvas()`, wait 1 seconds then transition in.
```csharp
Singleton.Instance.Transition.Out()
    .AddOutEnd(Singleton.Instance.Transition.In)
    .SetMusicFade(false)
    .SetDelayAfterOut(1)
    .AddOutEnd(ActivateMainMenuCanvas);
```

### ⏳ SceneLoader
Get the reference by

```csharp
Singleton.Instance.Scene.MyMethodName();
```

#### 🔗 Syntax

| Method                                    | Description                       |
|:--------                                  |:------------------------------    |
|`LoadScene(string sceneName)`              |Load scene by string               |
|`AddOnLoadingEnd(delegate func)`           |Add a function to call when the loading ended, usefull for transition in|
|`LoadSceneWithTransition(string sceneName)`|Transition out, loading screen, then transition in|

In the Unity Hierarchy, navigate to `Singleton/Loading`. You can change the loading animation by editing `Loading.cs` and its child's child.

#### 📖 Examples

Transition out in 0.7 seconds with fading music, start loading screen, load scene, then transition in.
```csharp
Singleton.Instance.Scene.LoadSceneWithTransition(sceneName);
Singleton.Instance.Transition.SetMusicFade(true)
    .SetDuration(0.7f);
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

Modify `SaveData.cs` in `Assets\Code\PresetScripts` however you like to fit the game. Remove the path depending on which platform you want to build in `Encryption.cs` line 16 and 17. Change the value of `JSON_ENCRYPTED_KEY` into any other random value with the same amount of digit. Note that this save system didn't use BinaryFormatter because of security risk according to Microsoft (Microsoft, 2022). The encryption uses Rijndael algorithm instead, see https://en.wikipedia.org/wiki/Advanced_Encryption_Standard.

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
Singleton.Instance.Audio.MyMethodName();
```

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`PlayMusic(AudioClip audioClip)`|Play the audioClip music|
|`StopMusic()`|Stop the music and null it|
|`StopPlayMusic()`|Stop the music then play it again|
|`PlaySound(AudioClip audioClip)`|Play the audioClip sound|
|`PlaySound(AudioClip audioClip, float volume)`|Play the audioClip sound by volume|
|`PlaySound(int index)`|Play the audioClip sound by index|


Drop the `MusicLoader` prefab in `Assets\Level\Prefabs\PresetPrefabs` to the scene and replace the Music Clip in the inspector to any music you want. This will automatically replace the currently played music when entering this scene. If there's no music asigned to the MusicLoader, it will automatically stop the current playing music.

#### 📖 Examples

Play the epicImpactSFX.
```csharp
Singleton.Instance.Audio.PlaySound(epicImpactSFX);
```

### 💻 Resolution
Get the reference by

```csharp
Singleton.Instance.Resolution.MyMethodName();
```

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`SetFullScreen(bool isFullScreen)`|Set whether the screen should be in fullscreen or not|

If you want to set the default resolution, Look at ResolutionManager.cs and modify the _defaultWidth and _defaultHeight. for example `int _defaultWidth = Screen.currentResolution.width * 4/6;`

#### 📖 Examples

Set the screen into fullscreen.
```csharp
Singleton.Instance.Resolution.SetFullScreen(true);
```

### ⚙️ Settings
The settings for music volume, sound volume, resolution, and fullscreen has been made as a template. In the Unity Hierarchy, navigate into `Settings` and edit the prefab to however you like. The slider has been customized so that it will get animated when changing the slider value. There's also a customly made button that will be scaled when an event like hover occurs. If you want to add soundeffect to the button, just add the ButtonUI.PlaySound(AudioClip audioClip) one of the unity event. In `SettingsManager.cs` modify `OnMusicValueChanged(float newVal)`, `OnSoundValueChanged(float newVal)`, `SetFullScreen(bool isFullScreen)`, or `OnResolutionValueChanged(int resolutionIndex)` if you want to add something for example a soundeffect.

## 📝 License
[MIT](https://choosealicense.com/licenses/mit/)

## 📑 Reference
Microsoft, M. (2022, October 28). Deserialization risks in use of BinaryFormatter and related types. Microsoft Learn. Retrieved December 12, 2022, from https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide

