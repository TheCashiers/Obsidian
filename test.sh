cd ./test
dotnet restore
echo waiting for mongodb
sleep 45
for d in ./*/ ; do (cd "$d" && dotnet test); done