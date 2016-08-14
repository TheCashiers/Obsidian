cd ./test
dotnet restore
echo waiting for mongodb
sleep 15
for d in ./*/ ; do (cd "$d" && dotnet test); done