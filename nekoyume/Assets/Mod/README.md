# Ares

This is a mod project used by the Unity client for Nine Chronicles.

# Features

This mod currently includes one feature.

## Calculating Arena Battle Odds

This feature works based on the data used by the Arena Board, which means you can use it when you're viewing the Arena Board.
When you are viewing the Arena Board, as shown in this image, press Ctrl(Cmd)+Alt+Shift+Space simultaneously to activate the mode.

> Go to Arena Board screen.

<img width="1920" alt="image" src="https://github.com/planetarium/9c-unity-mod-ares/assets/6128868/07828884-182c-43bc-8661-4dba88fc81f8">

> Click the `Ctrl(Cmd)+Alt+Shift+Space` combination.

<img width="1920" alt="image" src="https://github.com/planetarium/9c-unity-mod-ares/assets/6128868/f7175390-ebb7-4de1-bb18-1da5bd71fbbc">

- Previous Button: Close the mod.
- Calculate Button for each Avatar: Calculate odds asynchronously.
- Choice Button for each Avatar: Takes you to a screen that prepares you for an Arena battle with the target avatar.
- <, > Buttons: Page navigation buttons.

> Click `Calculate` buttons

<img width="1920" alt="image" src="https://github.com/planetarium/9c-unity-mod-ares/assets/6128868/32428b6f-d73e-4bc0-aa2c-ccaf25b66912">

> Waiting...

<img width="1920" alt="image" src="https://github.com/planetarium/9c-unity-mod-ares/assets/6128868/1ce89f6d-a603-4efa-91b7-256c5f22146c">

- I got you!

> Click `Choice` button and enjoy!

# How to apply to the NineChronicles Unity project

This mode can be added as a Unity package.
Add the mod packages to your NineChronicles project's manifest.json file as shown below.

- Path: `/nekoyume/Packages/manifest.json`

```json
{
  "dependencies": {
    "com.planetariumlabs.9c-unity-mod-ares": "https://github.com/planetarium/9c-unity-mod-ares.git?path=/nekoyume/Assets/MOD/Ares#develop-mod",
    "com.planetariumlabs.9c-unity-mod-core": "https://github.com/planetarium/9c-unity-mod-ares.git?path=/nekoyume/Assets/MOD/Core#develop-mod"
  }
}
```
