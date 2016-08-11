cd ./test

for d in */ ; do
    cd $d
    dotnet test
    cd ..
done