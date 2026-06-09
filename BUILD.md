# Como construir e rodar

## Pré-requisito: instalar o .NET 8 SDK

Este computador possui o **runtime** do .NET 8, mas **não o SDK** — sem o SDK,
o comando `dotnet build` retorna:

```
No .NET SDKs were found.
Download a .NET SDK: https://aka.ms/dotnet/download
```

Baixe o instalador em <https://dotnet.microsoft.com/download/dotnet/8.0>
escolhendo **".NET 8 SDK — Windows x64"**. Após instalar, reabra o PowerShell.

## Build e execução

```powershell
cd C:\Users\mathe\Downloads\LCS
dotnet build
dotnet run
```

## Verificação rápida do SDK

```powershell
dotnet --list-sdks
# deve mostrar algo como: 8.0.xxx [C:\Program Files\dotnet\sdk]
```

## Coletando as evidências

1. Tire **print** do `dotnet build` mostrando 0 erros.
2. Tire **print** do `dotnet run` com o cenário **Demo** (digite `1`, `14`, `6`).
3. Abra o arquivo `simulation_history.log` gerado na raiz e tire **print** dele.
4. (Opcional, mas recomendado) Rode novamente com cenário **2** e parâmetros
   agressivos (20 colonos, 1 painel, 0 estufas) para evidenciar o tratamento
   de exceção `ResourceDepletionException`.
5. Cole os prints dentro de `docs/EvidenciasDeExecucao.md` nos placeholders.
