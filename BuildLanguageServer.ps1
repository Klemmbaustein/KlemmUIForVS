param (
	[string] $buildConfig = "Debug"
)

$serverPath = "KlemmUILanguageServer"
$cmakeOutDir = "out/build/script"
$serverExecutable = "src/Resources/KlemmUILanguageServer.exe"

cd ${serverPath}
cmake -S . -B ${cmakeOutDir}
cmake --build ${cmakeOutDir} -j 12
cd ..

if (test-path ${serverPath}/${cmakeOutDir}/KlemmUILanguageServer.exe)
{
	cp ${serverPath}/${cmakeOutDir}/KlemmUILanguageServer.exe ${serverExecutable}
}
else
{
	cp ${serverPath}/${cmakeOutDir}/${buildConfig}/KlemmUILanguageServer.exe ${serverExecutable}
}