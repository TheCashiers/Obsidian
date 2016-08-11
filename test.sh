cd ../../test

for D in `find . -type d`
do
    dotnet test
done