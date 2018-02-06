#!bin/sh

echo "========Obisidian Configuration Script for Ubuntu========"
apt-get -y install curl wget libunwind8-dev libicu-dev
echo "========Cloning Git Repository ========"
mkdir /home/Obsidian/
cd /home/Obsidian/
git clone https://github.com/ZA-PT/Obsidian.git
echo "======== Configuring environment ========"
echo ".Net Core SDK"
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /etc/apt/trusted.gpg.d/microsoft.gpg
sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
apt-get update
apt-get -y install dotnet-sdk-2.1.4
echo "Node.js"
DOWNLOADDIR="/home/nodejs/"
wget -P $DOWNLOADDIR https://nodejs.org/dist/v6.11.0/node-v6.11.0-linux-x64.tar.xz
cd $DOWNLOADDIR
tar -Jxv -f **.tar.xz
ln -s /home/nodejs/node-v6.11.0-linux-x64/bin/node /usr/local/bin/node
ln -s /home/nodejs/node-v6.11.0-linux-x64/bin/npm /usr/local/bin/npm
echo "Yarn"
curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | sudo apt-key add -
echo "deb https://dl.yarnpkg.com/debian/ stable main" | sudo tee /etc/apt/sources.list.d/yarn.list
sudo apt-get update && sudo apt-get install yarn
echo "MongoDB"
mkdir -p /data/db
apt-get -y install mongodb-server
netstat -nalp | grep "27017"
