# Obsidian Canary branch
[![Build Status](https://travis-ci.org/ZA-PT/Obsidian.svg?branch=canary)](https://travis-ci.org/ZA-PT/Obsidian)
[![Coverage Status](https://coveralls.io/repos/github/ZA-PT/Obsidian/badge.svg?branch=canary)](https://coveralls.io/github/ZA-PT/Obsidian?branch=canary)
[![CodeFactor](https://www.codefactor.io/repository/github/za-pt/obsidian/badge)](https://www.codefactor.io/repository/github/za-pt/obsidian)

# Setting up build and test environment
## Windows
Install these folowing tools:
- .NET Core SDK
- Node.js with NPM
- Yarn
- MongoDB

And run the build script for PowerShell
``` powershell
./build.ps1
```
or for Command Prompt
``` winbatch
build.cmd
```

## Linux
### For CentOS
Run this command to clone the source code to `./Obsidian` directory and install required packages
```bash
curl -o- https://raw.githubusercontent.com/ZA-PT/Obsidian/canary/build/build.centos.sh | bash
```
And run this script to build
```bash
./build.sh
```
### Ubuntu and other linux distributions
Coming soon

## macOS
Coming soon