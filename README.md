# Card Wars Revival

A revival of *Adventure Time: Card Wars*, the mobile card battler. The goal is to bring the game back with **full
functionality** and **better performance** than the original, while keeping the **vibes exactly the same**: the
art, the music, the humor and the feel of a card game played at a table in the Tree Fort.

## A big thank you to shishkabob27

This project exists because of [**shishkabob27**](https://github.com/shishkabob27) and their
[CardWars](https://github.com/shishkabob27/CardWars) project. They did the hard part: bringing a discontinued mobile
game back to life on PC, getting it playable again, putting out Windows builds, and standing up replacement
server and login pieces so people could actually play it. Revival builds directly on that work, and the history of
this repository includes all of theirs. Thank you for the work and for keeping this game alive.

## What this project is

Revival is a continuation and modernization of that port. The aims are:

- **Full functionality**: every part of the original game working again, including the pieces that used to depend on
  servers and third-party services that no longer exist.
- **Performance improvements**: smaller builds, lower memory use and faster loading than the original.
- **The same vibes**: no redesigns. Anything that changes should make the game feel more like the original, not less.
- **Every platform**: macOS, Windows, Linux, iOS and Android (and the web, later).

## Status

This is early work and under active development. Highlights so far:

- Moved the project from Unity 2017 to **Unity 6** (`6000.6.7f1`).
- All five platforms build. **macOS (Apple silicon)** and **iOS (device)** have been run and checked by hand;
  Windows, Linux and Android build but still need verification on real hardware.
- **Offline mode**: the original game servers are gone, so the game runs entirely locally for now.
- **Smaller builds**: textures are now compressed (the macOS build went from 2.7 GB to 1.0 GB) and unused low-resolution
  variants no longer ship (down to about 880 MB).
- Mobile fixes: landscape lock, safe-area handling for notches and the Dynamic Island, and a fix for slightly
  transparent cards in the hand.

What is still missing or unverified is tracked in the [issue list](../../issues). Notable open items include
Android plugin cleanup, online play and backend, verifying the Windows and Linux builds, and the web version.

## Getting the code and building

Requirements: **Unity 6000.6.7f1** with the build support modules for the platforms you want (macOS, Windows,
Linux, iOS, Android; WebGL is optional). Open the project folder in Unity Hub.

Settings live in `Assets/StreamingAssets/server_settings.json`:

| Key | Meaning |
|---|---|
| `offline_mode` | `true` answers all server requests locally (no backend needed). |
| `skip_tutorial` | `true` turns on the debug flag that skips the tutorial. Useful for testing. |

Command-line builds use the `PlatformBuild` editor script, for example:

```
Unity -batchmode -quit -projectPath . -buildTarget StandaloneOSX \
  -executeMethod PlatformBuild.Build -buildPath Build/Mac/CardWars.app
```

Add `-development` for a development build. For the iOS Simulator add `-simulator`; the iOS project is then
built with Xcode (the simulator needs an arm64 Xcode build on Apple silicon).

Some assets are kept out of builds on purpose. See `Assets/LowRes/README.md`.

## Contributing

Issues and pull requests are welcome. Please check the open issues first; many of them list concrete next steps.
When you add anything that changes how the game looks or plays, the test is simple: does it still feel like Card Wars?

## Credits

- **shishkabob27**: the PC port this project is built on. See above.
- Original game: *Adventure Time: Card Wars*, developed by **Cartoon Network Games** and **Kung Fu Factory**.
- *Adventure Time* was created by Pendleton Ward.

## Legal

This is an unofficial, non-commercial fan project. It is not affiliated with, endorsed by or sponsored by Cartoon
Network, Warner Bros. Discovery or the original developers. *Adventure Time*, *Card Wars* and all related characters,
names, art, audio and other content are the property of their respective owners. This repository is provided for
preservation and educational purposes. If a rights holder has a concern, please open an issue and it will be addressed.
