cd ./test
cat /var/log/mongodb/mongod.log
for d in ./*/ ; do (cd "$d" && dotnet test); done