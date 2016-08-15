cd ./test
dotnet restore
for d in ./*/ ; do (cd "$d" && dotnet test); done