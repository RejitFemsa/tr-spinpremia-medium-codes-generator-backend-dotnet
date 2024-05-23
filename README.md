# foundation-dotnet-template
Template para estructura de proyectos .Net
```
$/
  artifacts/
  build/
  docs/
  lib/
  packages/
  samples/
  src/
  tests/
  .editorconfig
  .gitignore
  .gitattributes
  build.cmd
  build.sh
  LICENSE
  NuGet.Config
  README.md
  {solution}.sln
```
---
- `src` - Proyectos principales (el codigo del producto)
- `tests` - proyectos con pruebas unitarias
- `docs` - Documentacion, archivos markdown, archivos de ayuda, etc.
- `samples` (opcional) - Proyectos de ejemplo
- `lib` - Objetos que no pueden existir **NUNCA** en un paqete nuget
- `artifacts` - Los artefactos generados por el build se colocan aqui, ejecutar build.cmd/build.sh debe generar los artefactos en esta carpeta (nupkgs, dlls, pdbs, etc.)
- `packages` - Paquetes nuGet
- `build` - Scripts de personalizacion del proceso de build (custom msbuild files/psake/fake/albacore/etc)
- `build.cmd` - Bootstrap para la construccion en windows
- `build.sh` - Bootstrap para la construccion *nix
- `global.json` - Solo aplica para ASP.NET vNext
