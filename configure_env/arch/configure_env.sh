#!bin/sh
echo "========Obisidian Configuration Script for Arch Linux========"
echo "======== Installing tools ========"
pacman -Syy git dotnet-sdk nodejs npm yarn mongodb
echo "======== Cloning Git Repository ========"
git clone https://github.com/ZA-PT/Obsidian.git
