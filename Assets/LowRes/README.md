# Low-resolution assets (kept out of builds)

These are the `low_*` variants that used to live under `Assets/Resources`. The original game loaded them
when `KFFLODManager.IsLowEndDevice()` returned true. That function now always returns false, so nothing selects
them, but anything under a `Resources` folder is packed into every build. They were moved here, outside
`Resources`, so they no longer ship. Files were moved with their `.meta` files, so GUIDs (and references from
other assets) are unchanged.

Layout: `ResourcesLowRes/<same subfolder as before>/low_*`.

## Using them again (for example in a web build)

Move the contents of `ResourcesLowRes` back into `Assets/Resources`, keeping the subfolder structure
(for example `ResourcesLowRes/summons/low_X.prefab` goes back to `Resources/summons/low_X.prefab`), and make
`IsLowEndDevice` return true for the platform. Use the Unity editor or `git mv` so the `.meta` files travel
with the assets.

## Still included in builds

A few `low_*` assets are referenced directly by scenes (for example `AdventureTime.unity` uses
`low_CardArtAtlas`, `low_CardFrameAtlas` and `low_CharacterIconAtlas` for some sprites and several
`low_FX_*` prefabs). They are still packed because a scene references them, wherever they live.
