#!bin/sh

echo "========Obisidian Configuration Script for Ubuntu========"
apt-get install curl wget libunwind8-dev libicu-dev
echo "========Cloning Git Repository ========"
mkdir /home/Obsidian/
cd /home/Obsidian/
git clone https://github.com/ZA-PT/Obsidian.git
echo "======== Configuring environment ========"
echo ".Net Core SDK"
sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ trusty main" > /etc/apt/sources.list.d/dotnetdev.list'
 apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893
apt-get update
apt-get install dotnet-dev-1.0.4
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
apt-get install mongodb-server
netstat -nalp | grep "27017"
