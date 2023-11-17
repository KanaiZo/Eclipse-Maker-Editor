# Eclipse-Maker-Chart-Editor
A fork of [Moonscraper](https://github.com/FireFox2000000/Moonscraper-Chart-Editor) built for platforms including mobile, with tiny quality-of-life features.

> ⚠️ Eclipse Maker is not affiliated with Moonscraper Chart Editor, nor is it officially supported by FireFox2000000 or any of it's contributors. Report Eclipse Maker issues in the [Eclipse Maker Discord server](https://discord.com/invite/33gZuRejpq).

NOTE: I am a new programmer, and this is the project I will use to learn from.
This software is currently using leftover assets from Moonscraper not created by me. These will be replaced as development continues.

## Compiling from Source
Follow the instructions below for your desired platform to build and run from source.

### All Platforms
1. Download and install Unity 2022.3.13f1
2. Run Unity and open the project folder with it
3. Use the menu option Build Processes > Build Full Releases
  - Note that 7zip and Inno Setup are required to be installed to build distributables and installers respectively. 

### Runtime dependencies (Windows)
Required runtime dependencies are included with the build.

### Runtime dependencies (Linux)
The application requires the following dependencies to be installed:
- `ffmpeg sdl2 libx11-6 libgtk-3-0`
- `libbass` (included with the build)

A [`PKGBUILD` file for Arch Linux](aur/PKGBUILD) is included in the repository but is currently pointed at Moonscraper. This will be changed as soon as possible.

Other distribution packagers can use the `PKGBUILD` file for reference.

## License
- See [attribution.txt](https://github.com/FireFox2000000/Moonscraper-Chart-Editor/blob/master/Moonscraper%20Chart%20Editor/Assets/Documentation/attribution.txt) for third party libraries and resources included in this repository.
- See [LICENSE](LICENSE).
- The BASS audio library (a dependency of this application) is a commercial product. While it is free for non-commercial use, please ensure to obtain a valid licence if you plan on distributing any application using it commercially.
