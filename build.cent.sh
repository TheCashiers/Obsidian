sudo cp ./mongodb-org-3.4.repo /etc/yum.repos.d/mongodb-org-3.4.repo
sudo yum install -y mongodb-org wget curl libunwind libicu

# install dotnet
curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?linkid=848821
sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin

# install yarn and nodejs
sudo wget https://dl.yarnpkg.com/rpm/yarn.repo -O /etc/yum.repos.d/yarn.repo
sudo curl --silent --location https://rpm.nodesource.com/setup_6.x | bash -
sudo yum install nodejs yarn

sudo service mongod start

chmod +x ./build.core.sh
./build.core.sh