# Mod.Core

The core part of the modding. Modders can use this package to create their own mods.

> [!NOTE]
> This package is currently in preview. If you have any questions or feedback, please post them on the [Issues](https://github.com/planetarium/9c-unity-mod-ares/issues) page.

# Quick Start

## Installation

1. Install the [Unity](https://unity.com/) and [Git](https://git-scm.com/).
2. Clone the [NineChronicles](https://github.com/planetarium/NineChronicles) repository.
3. Checkout the `main` branch.
4. Open the `NineChronicles` project with Unity.
5. Add the mod packages to your NineChronicles project's `manifest.json` file as shown below.

   - Path: `/nekoyume/Packages/manifest.json`

   ```json
   {
      "dependencies": {
          "com.planetariumlabs.9c-unity-mod-core": "https://github.com/planetarium/9c-unity-mod-ares.git?path=/nekoyume/Assets/Mod/Core#main-mod"
      }
   }
   ```

## Usage

1. Implement the `IMod` interface.
2. Attach the `ModAttribute` to the class that implements the `IMod` interface.
3. Play the game either in the editor or in the build.
4. Press `Ctrl(Cmd)+Alt+Shift+Space` keys simultaneously to activate the mod.

Here is an example of the [Ares](../Ares) mod.
