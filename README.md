# Discord Status Updater for ESO (using Rich Presence)

*Updated to Update 30, or ESOUI version 100035*.

This little program is made using the .Net 4.6.1 framework and sends Rich Presence data to your local Discord client, making your status say more than just "Playing **Elder Scrolls Online**", including info about your current location, your level, your current party, and more! While running, your presence should look like this:

<p align="center">
  <img src="https://i.imgur.com/iAYoWnK.png">
</p>

## Installation

### Normal Method

This program currently only supports Windows. Visit the [Releases](https://github.com/Medallyon/eso-discord-rich-presence-client/releases) page to view the latest release and install it. Make sure you install it into the correct folder (which should be `My Documents/Elder Scrolls Online/AddOns/`).

### Compiling Yourself

#### Prerequisites

+ [.Net 4.6.1 Framework](https://support.microsoft.com/en-gb/help/3102436/the-net-framework-4-6-1-offline-installer-for-windows)
+ [Visual Studio 2015+](https://visualstudio.microsoft.com/downloads/)
  + [NLua](https://www.nuget.org/packages/NLua) (Install via NuGet Manager in VS)
+ [discord-rpc](https://github.com/discordapp/discord-rpc/releases) (`win32-dynamic`)
  + Include this with the built executable

#### Instructions

1. Install the latest version of Microsoft Visual Studio
2. Import this VS solution
3. `Build` your solution
4. [Grab the dynamic binaries](https://github.com/discordapp/discord-rpc/releases) for the Discord RPC API and include them in your compiled directory
6. Use to your liking!

## Usage

1. [Install](#installation) this addon (use [Minion](https://minion.mmoui.com/) for ease of use)
2. Start Discord
3. Start ESO
4. Start the Addon Client, found in the Client folder that comes with the Addon
5. (Optional) Configure the Addon by typing `/drp` into the in-game chat
6. (Optional) Create a shortcut on your desktop for the Addon Client

## Troubleshooting

+ **The files immediately disappear when I unzip the addon to the Addons folder**
  + This is most likely because of an anti-virus program (Windows Defender, Malwarebytes, Avast, etc.). Create an exception for the unzipped folder along with the Client to stop this issue.
+ **I get an Error: `The target directory is not empty.`**
  + Remove the Addon folder `DiscordRichPresence` and try installing the addon again.
  + If the error persists, move the Client folder to your desktop, remove the Addon folder, and then try running the Client

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## Credits

+ [Medallyon](https://github.com/medallyon) - Main codebase & maintenance
+ [Whisperity](https://github.com/whisperity) - Some additional low-level stuff
