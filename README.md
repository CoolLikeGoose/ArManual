# AR Manual App

Mobile AR application for displaying interactive AR manuals with marker-based tracking.

This project was developed as part of a bachelor's thesis.

## Architecture and structure

The core application logic is located in `Assets/Scripts`. The project is organized into logical folders:

- `Core/` — Orchestration of the application lifecycle and global runtime state. Contains high-level controllers.
- `Detection/` — Marker and QR detection code.
- `Tracking/` — Management of markers and interaction points during the app lifecycle.
- `ManualSession/` — Scenario and session management.
- `Models/` — Plain data models used across the app (manual, scenario, interaction point, trackpoint, etc.).
- `UI/` — UI controllers, view logic and bindings between state and interface.
- `Network/` — Abstraction for data loading.
- `DebugTools/` — Developer tools and visualizations.
- `Tests/` — Test data and helpers.

Additional important folders in the project:

- `Plugins/` — Native plugins (ArUco native plugin is placed here and selected by Unity depending on platform/architecture).
- `Prefabs/`, `Materials/`, `Textures/` — Visual and prefab assets used by the app.

## Installation and setup

Requirements
- Unity Hub with installed Unity 6.2
- Platform packages (ARCore/ARKit) support on targeted device
- Android SDK / Xcode toolchain for device builds

Quick setup

1. Open the project folder in Unity Hub.
2. Let the Editor import packages and compile.

Using the backend API

- By default the loader uses `FakeManualDataSource` for offline testing.
- To use the REST backend, open main scene and set `useFakeData = false` on `AppManager/APILoader` object, then ensure `APIManualDataSource` in `APILoader` class is constructed with your backend base URL.

## Tech stack and notable libraries

- Unity 6.2
- AR Foundation
- [ZXing](https://github.com/zxing/zxing) (QR code decoding)

Other parts of the thesis project:
- [ArUcoDetector](https://github.com/CoolLikeGoose/ArUcoDetector) (Detection and PnP solving as native plugin)
- [ArManualBackend](https://github.com/CoolLikeGoose/ArManualBackend) (REST API backend for manuals data)