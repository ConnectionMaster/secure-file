rd /s /q build
dotnet publish secure-file -f net6.0 -o build -c Release
dotnet publish secure-file -f net462 -o build -c Release