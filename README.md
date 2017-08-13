# Obsidian Canary branch
[![Build Status](https://travis-ci.org/ZA-PT/Obsidian.svg?branch=canary)](https://travis-ci.org/ZA-PT/Obsidian)
[![Coverage Status](https://coveralls.io/repos/github/ZA-PT/Obsidian/badge.svg?branch=canary)](https://coveralls.io/github/ZA-PT/Obsidian?branch=canary)
[![CodeFactor](https://www.codefactor.io/repository/github/za-pt/obsidian/badge)](https://www.codefactor.io/repository/github/za-pt/obsidian)

# Setting up build and test environment
## Windows
Install these following tools:
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

## *NIX systems
Run these following command to clone the source code to `./Obsidian` directory and install required packages

CentOS
```bash
curl -o- https://raw.githubusercontent.com/ZA-PT/Obsidian/canary/configure_env/centos/configure_env.sh | bash
```

Ubuntu
```bash
curl -o- https://raw.githubusercontent.com/ZA-PT/Obsidian/canary/configure_env/ubuntu/configure_env.sh | bash
```

And run this script to build
```bash
./build.sh
```
Support for macOS and other linux distributions are coming soon
