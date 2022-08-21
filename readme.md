# BaGet-Reloaded :baguette_bread:  <!-- omit in toc -->

A lightweight [NuGet](https://docs.microsoft.com/en-us/nuget/what-is-nuget) server used for Hosting Reloaded II mods based on [BaGet](https://github.com/loic-sharma/BaGet).

![Image](https://i.imgur.com/7n43mp1.png)

## About This Fork

This is a fork of BaGet, specifically intended to be used with Reloaded-II mods.

- [About This Fork](#about-this-fork)
  - [Changes](#changes)
  - [Setup Instructions](#setup-instructions)
    - [Installing .NET Core SDK](#installing-net-core-sdk)
    - [Create the bootup script.](#create-the-bootup-script)
    - [Create a systemd service.](#create-a-systemd-service)
    - [Enable and start systemd service.](#enable-and-start-systemd-service)
- [Maintenance](#maintenance)
  - [Updating BaGet-Reloaded](#updating-baget-reloaded)
  - [Freeing Up Disk](#freeing-up-disk)
- [Original Readme](#original-readme)
  - [Getting Started](#getting-started)
  - [Features](#features)
  - [Develop](#develop)

### Changes

- Themed.  
- Supports HTTPS.  
- Supports Response Compression.  
- Upstream with latest .NET Runtime.  
- Returns readme & changelog in search.  
- Custom auth. Associates each package with key at first upload.  

### Setup Instructions
Based on Ubuntu Server 22.04.  

#### Installing .NET Core SDK

```
sudo snap install dotnet-sdk --channel=6.0/stable --classic
snap alias dotnet-sdk.dotnet dotnet
```

#### Create the bootup script.
Script to start the application.  

`> nano /opt/start-baget-on-boot.sh`

```sh
#!/bin/sh

# Can be set in config too; see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.1#endpoint-configuration
export ASPNETCORE_URLS=http://*:5000;https://*:443

DATE=`date '+%Y-%m-%d %H:%M:%S'`
echo "BaGet Service Started at ${DATE}" | systemd-cat -p info

cd /opt/reloaded-ii/
dotnet /opt/reloaded-ii/BaGet.dll
```

#### Create a systemd service.

This will run the server at startup.  

`> sudo nano /lib/systemd/system/reloaded-ii.service`

```ini
[Unit]
Description=Reloaded II BaGet Server.

[Service]
Type=simple
ExecStart=/bin/sh /opt/start-baget-on-boot.sh
Restart=always

[Install]
WantedBy=multi-user.target
```

#### Enable and start systemd service.

```
sudo systemctl enable reloaded-ii.service
sudo systemctl start reloaded-ii.service
```

If you want to update your version, stop the service. swap out the files and resume.  

## Maintenance

### Updating BaGet-Reloaded

First stop the active server process:
```
sudo systemctl stop reloaded-ii.service
```

Then, using SFTP (or other method), overwrite published version in `/opt/reloaded-ii`.

Lastly, restart the service.

```
sudo systemctl start reloaded-ii.service
```

### Freeing Up Disk
To free up disk space, might be a good idea (on Ubuntu Server) to clean up old Snaps after updates.

```
chmod +x remove-old-snaps
sudo ./remove-old-snaps
```

```sh
#!/bin/sh
# Removes old revisions of snaps
# CLOSE ALL SNAPS BEFORE RUNNING THIS
set -eu
LANG=en_US.UTF-8

snap list --all | awk '/disabled/{print $1, $3}' |
while read snapname revision; do
    snap remove "$snapname" --revision="$revision"
done
```


Otherwise if you intend on obtaining a SSL cert through other means, set `UseLettuceEncrypt` to `false` and uncomment the `Certificates` section in the same config file. 

Use [Certificate Sources on MSDN](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-5.0#certificate-sources) as reference.

## Original Readme

### Getting Started

1. Install [.NET Core SDK](https://www.microsoft.com/net/download)
2. Download and extract [BaGet's latest release](https://github.com/loic-sharma/BaGet/releases)
3. Start the service with `dotnet BaGet.dll`
4. Browse `http://localhost:5000/` in your browser

For more information, please refer to [our documentation](https://loic-sharma.github.io/BaGet/).

### Features

* Cross-platform
* [Dockerized](https://loic-sharma.github.io/BaGet/#running-baget-on-docker)
* [Cloud ready](https://loic-sharma.github.io/BaGet/cloud/azure/)
* [Supports read-through caching](https://loic-sharma.github.io/BaGet/configuration/#enabling-read-through-caching)
* Can index the entirety of nuget.org. See [this documentation](https://loic-sharma.github.io/BaGet/import/nugetorg/#mirroring)
* Coming soon: Supports [private feeds](https://loic-sharma.github.io/BaGet/private-feeds/)
* And more!

Stay tuned, more features are planned!

### Develop

1. Install [.NET Core SDK](https://www.microsoft.com/net/download) and [Node.js](https://nodejs.org/)
2. Run `git clone https://github.com/loic-sharma/BaGet.git`
3. Navigate to `.\BaGet\src\BaGet`
4. Navigate to `..\BaGet`
5. Start the service with `dotnet run`
6. Open the URL `http://localhost:5000/v3/index.json` in your browser
