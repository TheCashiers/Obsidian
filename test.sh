cd ./test
dotnet restore
echo waiting for mongodb
sleep 120
for d in ./*/ ; do (cd "$d" && dotnet test); done