dotnet build
dotnet test

rm -rf dist
mkdir dist

cd VipsApi
dotnet publish -r linux-x64 -c Release -o ../dist
cd ..


docker build -t vipsapi .

docker run --rm -it -d -p 5010:5010 vipsapi

http "http://localhost:5010/api/v1/image?width=100&url=https%3A%2F%2Fvia.placeholder.com%2F300"
