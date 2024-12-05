# Soulwall
SoulWall is an app that combines a digital mural with a physical one to make it more interactive and engaging. We want to see if it helps users remember things better in a cognitively demanding space, such as museum. Our intended users  are adults aged 18-25.


# Installation
## Prerequisites
1. [Enable developer mode for the Quest](https://developers.meta.com/horizon/documentation/native/android/mobile-device-setup/)

2. [Enable unknown sources](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-rift-s/unknown-sources/)

3. ADB (Optional)

4. Unity Hub and Unity Editor 6 (Optional)
   - with Android support
## Android Debug Bridge
1. Download the [apk](https://github.com/SealTheTiel/SoulWall/releases).

2. Move the apk to the location of your ADB.
   - Skip if ADB is already installed and is part of the path environment variables.

4. Open a terminal in the same directory of the apk.

5. Run `adb devices` to verifiy if the MetaQuest is connected.
   - When adb is in the same directory, and you are using Windows Powershell, dont forget the `./` prefix
     - `./adb devices`

6. Run `adb install -r <filename.apk>`

7. Check headset for the app

## Build and Run using the Unity Editor
1. Clone the repository to a folder

2. Run Unity Hub and add open a project from disk.

3. Navigate to the cloned repository.

4. Open it using Unity 6

5. Connect your Metaquest to your computer using a USB cable

6. Build and Run

# Getting started
1. The primary trigger of your controller spawns the mural in front of you.
   - Pressing it again allows you to reposition the mural placement

2. You can use your fingers to click on the start button
   - This will show an animation transitioning to the next part

3. There are 4 zones or exhibits
   - Facing your head at a zone focuses it
     - Focused zone will glow and its artworks willalso glow and be animated

4. Tapping on animated artworks will show a modal of the artworks with its info and full unedited images
   - Modals can be moved around and/or closed.
   - Multiple modals can be active at the same time, provided that their artworks are unique.

# Basic Controls
1. Primary Controller Trigger
   - Spawns the initial eagle (if not yet spawned)
   - Moves the anchor of the eagle

2. Click
   - Fingers can virtually push buttons

3. Drag
   - Pinching or grabbing the modals will move them around

# Troubleshooting
1. Crash on build and run.
   - Relaunch the app, this is just the device failing to launch the app when it is installed via Unity Build and Run.
   - Wear your headset before it gets installed on the headset.
  
2. No hands are tracked.
   - Make sure hand tracking is enabled in the device settings.
   - If enabled already, enable the option to switch from controller to hand tracking by tapping the controllers together.
     - This way, you can force the quest to track your hands.

3. The buttons are not clicking.
   - Move nearer so you can reach it.
   - Alternatively, you can press the trigger to move the mural in front of you.

4. The modal is transparent.
   - The modal is currently flipped, it again to see contents.
   - You can tap on the animated artwork its from and it will reset.

# Contact
Team Members
- [SealTheTiel](https://github.com/SealTheTiel)
- [ikkai-r](https://github.com/ikkai-r)
- [BanzaiNamco](https://github.com/BanzaiNamco)
- [Tiny-Banana](https://github.com/Tiny-Banana)
