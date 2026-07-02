if (Test-Path -Path "./CIAdvanced/bin") {
    Remove-Item ./CIAdvanced/bin -recurse
}
dotnet publish -c Release -p:CreateCipx=true
