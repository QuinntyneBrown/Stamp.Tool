dotnet tool uninstall -g Stamp.Tool
dotnet pack
dotnet tool install --global --add-source ./nupkg Stamp.Tool