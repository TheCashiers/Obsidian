echo "=============== Obsidian Build Script for CentOS ============="
sudo yum install -y curl wget git
echo "=============== Cloning git repository =================="
git clone https://github.com/ZA-PT/Obsidian.git
cd ./Obsidian/build/

echo "=============== Configuring environment =================="
sudo cp ./mongodb-org-3.4.repo /etc/yum.repos.d/mongodb-org-3.4.repo
cd ..

sudo wget https://dl.yarnpkg.com/rpm/yarn.repo -O /etc/yum.repos.d/yarn.repo
curl --silent --location https://rpm.nodesource.com/setup_8.x | bash -
sudo yum install -y nodejs npm yarn mongodb-org

curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?linkid=848821
sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin
rm dotnet.tar.gz

echo "=============== Starting database service ===================="
sudo service mongod start

echo "=============== Starting build ====================="
chmod +x ./build.core.sh
./build.core.sh