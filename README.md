
<!-- ![Logo](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/th5xamgrr6se0x5ro4g6.png) -->
<!-- 444x91 px -->
<!-- <hr> -->
<h1 align="center">Unity 2D Configured Template V1.0.0</h1>

Unity 2D Configured Template V1.0.0 is a unity template with some probably usefull features that can
always be used for any unity game project. With a few modification within Unity Engine, it can also be used for 3D game. This is mainly a template for game UI.


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
Singleton.Instance.transition.MyMethodName();
```

#### 🔗 Syntax

| Method                            | Description                        |
|:--------                          | :------------------------------    |
|`Out()`                            |Start the transition out animation  |
|`In()`                             |Start the transition in animation   |
|`SetOutDefault()`                  |Set the transition out parameters to the default values|
|`SetInDefault()`                   |Set the transition in parameters to the default values|
|`OutDefault()`                     |call `SetOutDefault()` then `Out()`|
|`InDefault()`                      |call `SetInDefault()` then `In()`|
|`SetDelayAfterOut(float t)`        |Set the delay after the transition animation ends, before the added/set function by t|
|`SetDuration(float t)`             |Set the duration of the transition animation by t|
|`SetMusicFade(bool b)`             |Set whether the music will fade during the transition out|
|`SetOutStart(delegate func)`       |Set the method that will get called when transition out started    |
|`SetOutAnimation(delegate func)`   |Set the method that will get called during the transition out      |
|`SetOutEnd(delegate func)`         |Set the method that will get called when transition out ended      |
|`AddOutStart(delegate func)`       |Add another method that will get called when transition out started|
|`AddOutAnimation(delegate func)`   |Add another method that will get called during the transition out  |
|`AddOutEnd(delegate func)`         |Add another method that will get called when transition out ended  |
|`SetInStart(delegate func)`        |Set the method that will get called when transition in started     |
|`SetInAnimation(delegate func)`    |Set the method that will get called during the transition in       |
|`SetInEnd(delegate func)`          |Set the method that will get called when transition in ended       |
|`AddInStart(delegate func)`        |Add another method that will get called when transition in started |
|`AddInAnimation(delegate func)`    |Add another method that will get called during the transition in   |
|`AddInEnd(delegate func)`          |Add another method that will get called when transition in ended   |


In the Unity Hierarchy, navigate to `Singleton/Transition`. Inside `TransitionAnimation.cs`, change the animation the way you want there with `OutAnimation(float t) and by editing its child`. Make sure the animation can be Played by inputing the parameter `float t` from 0 to 1. You can also modify `OutStart()`, `OutEnd()`, `InStart()`, and `InEnd()`.

#### 📖 Examples

Transition out without fading the music, call `ActivateMainMenuCanvas()`, wait 1 seconds then transition in.
```csharp
Singleton.Instance.transition.Out()
    .AddOutEnd(Singleton.Instance.transition.InDefault)
    .SetMusicFade(false)
    .SetDelayAfterOut(1)
    .AddOutEnd(ActivateMainMenuCanvas);
```

### ⏳ SceneLoader
Get the reference by

```csharp
Singleton.Instance.scene.MyMethodName();
```

#### 🔗 Syntax

| Method                                    | Description                       |
|:--------                                  |:------------------------------    |
|`LoadScene(string sceneName)`              |Load scene by string               |
|`AddOnLoadingEnd(delegate func)`           |Add a function to call when the loading ended, usefull for transition in|
|`LoadSceneWithTransition(string sceneName)`|Transition out, then loading screen, transition in|

In the Unity Hierarchy, navigate to `Singleton/Loading`. You can change the loading animation by editing `Loading.cs` and its child's child.

#### 📖 Examples

Transition out in 0.7 seconds without fading the music, start loading screen, load scene, then transition in.
```csharp
Singleton.Instance.scene.LoadSceneWithTransition(sceneName);
Singleton.Instance.transition.SetMusicFade(true)
    .SetDuration(0.7f);
```

### 📑 SaveData
Get the reference by

```csharp
Singleton.Instance.save.MyMethodName();
```

#### 🔗 Syntax

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`SaveData()`|Save the data of Singleton.Instance.save.data       |
|`LoadData()`|Load the save data into Singleton.Instance.save.data|

Modify `SaveData.cs` in `Assets\Code\PresetScripts` however you like to fit the game. Remove the path depending on which platform you want to build in `Encryption.cs` line 16 and 17. Change the value of `JSON_ENCRYPTED_KEY` into any other random value with the same amount of digit.

#### 📖 Examples

Set the value of the highscore to 120 then save it.
```csharp
Singleton.Instance.save.data.highscore = 120;
Singleton.Instance.save.SaveData();
```
Set the value of the save data into newPlayerData then save it. Make sure to modify the `SaveData.cs`.
```csharp
Save newPlayerData = new Save();
newPlayerData.username = "chicken";
newPlayerData.exp = 50;
Singleton.Instance.save.data = newPlayerData;
Singleton.Instance.save.SaveData();
```

### 🔊 Audio
Get the reference by

```csharp
Singleton.Instance.audio.MyMethodName();
```

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`PlayMusic(AudioClip audioClip)`|Play the audioClip music|
|`StopMusic()`|Stop the music and null it|
|`StopPlayMusic()`|Stop the music then play it again|
|`PlaySound(AudioClip audioClip)`|Play the audioClip soundeffect|
|`PlaySound(AudioClip audioClip, float volume)`|Play the audioClip soundeffect by volume|


Drop the `MusicLoader` prefab in `Assets\Level\Prefabs\PresetPrefabs` to the scene and replace the Music Clip in the inspector to any music you want. This will automatically replace the currently played music when entering this scene.

#### 📖 Examples

Play the epicImpactSFX.
```csharp
Singleton.Instance.audio.PlaySound(epicImpactSFX);
```

### 💻 Resolution
Get the reference by

```csharp
Singleton.Instance.resolution.MyMethodName();
```

| Method     | Description                                        |
|:--------   |:------------------------------                     |
|`SetFullScreen(bool isFullScreen)`|Set whether the screen should be in fullscreen or not|
#### 📖 Examples

Set the screen into fullscreen.
```csharp
Singleton.Instance.resolution.SetFullScreen(true);
```

### ⚙️ Settings
The settings for music volume, sound volume, resolution, and fullscreen has been made as a template. In the Unity Hierarchy, navigate into `Settings` and edit the prefab to however you like. The slider has been customized so that it will get animated when changing the slider value. There's also a customly made button that will be scaled when an event like hover occurs. If you want to add soundeffect to the button, just add the ButtonUI.PlaySound(AudioClip audioClip) one of the unity event. In `SettingsManager.cs` modify `OnMusicValueChanged(float newVal)`, `OnSoundValueChanged(float newVal)`, `SetFullScreen(bool isFullScreen)`, or `OnResolutionValueChanged(int resolutionIndex)` if you want to add something for example a soundeffect.

## 📝 License
[MIT](https://choosealicense.com/licenses/mit/)