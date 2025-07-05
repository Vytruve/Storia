echo.
echo =================================================
echo      Criando a Solucao Storia (.sln)
echo =================================================
echo.

cd Storia

dotnet new sln -n Storia

echo.
echo Solucao criada. Adicionando projetos...
echo.

dotnet build Storia\src\UI\Storia.UI.Maui\Storia.UI.Maui.csproj
dotnet sln add "Storia/src/UI/Storia.UI.Maui/Storia.UI.Maui.csproj"
dotnet sln add "Storia/src/Infrastructure/Storia.Infrastructure/Storia.Infrastructure.csproj"
dotnet sln add "Storia/src/Core/Storia.Core/Storia.Core.csproj"
dotnet sln add "Storia/src/Application/Storia.Application/Storia.Application.csproj"


echo.
echo =================================================
echo      PROJETO STORIA COMPLETO!
echo =================================================
echo.
echo A estrutura completa do projeto foi criada.
echo Agora voce pode abrir o arquivo 'Storia.sln' no Visual Studio ou Rider.
echo.

pause