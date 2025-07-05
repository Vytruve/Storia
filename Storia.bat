echo.
echo =================================================
echo      Criando a Solucao Storia (.sln)
echo =================================================
echo.

 

dotnet new sln -n Storia

echo.
echo Solucao criada. Adicionando projetos...
echo.

dotnet build ""src\UI\Storia.UI.Maui\Storia.UI.Maui.csproj""
dotnet sln add "src/UI/Storia.UI.Maui/Storia.UI.Maui.csproj"
dotnet sln add "src/Infrastructure/Storia.Infrastructure/Storia.Infrastructure.csproj"
dotnet sln add "src/Core/Storia.Core/Storia.Core.csproj"
dotnet sln add "src/Application/Storia.Application/Storia.Application.csproj"
dotnet sln add "src/"



echo.
echo =================================================
echo      PROJETO STORIA COMPLETO!
echo =================================================
echo.
echo A estrutura completa do projeto foi criada.
echo Agora voce pode abrir o arquivo 'Storia.sln' no Visual Studio ou Rider.
echo.

pause
