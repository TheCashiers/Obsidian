echo "================= Start Configuring Package Dependencies (Stage 1 of 3) ============="
dotnet restore
cd ./src/Obsidian/
yarn
echo "================= Start Building (Stage 2 of 3) ============="
dotnet build
echo "================= Start Testing (Stage 3 of 3) ============="
cd ../../test
for d in ./*/ ; do(cd "$d" && dotnet test); done
cd ../src/Obsidian
npm run test:cover:travis