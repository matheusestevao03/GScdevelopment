# Evidências de Execução — Lunar Colony Simulator

Este arquivo concentra o roteiro reprodutível para gerar as evidências
exigidas pela rubrica (`Obrigatório: evidências de execução; sem evidências (-10)`).

## 1. Build

```powershell
cd C:\Users\mathe\Downloads\LCS
dotnet build
```

Saída esperada (resumida):

```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

Tire um print desta saída e cole abaixo:

> **Insira aqui o print do `dotnet build`**.

## 2. Execução do cenário Demo

```powershell
dotnet run
```

Quando solicitado:

```
> 1         (cenário Demo)
> 14        (14 dias)
> 6         (tick de 6h)
```

Trecho representativo da saída (gerado pelo `ConsoleLogger`):

```
╔════════════════════════════════════════════════════════╗
║         LUNAR COLONY SIMULATOR — Core IA v1.0          ║
║      FIAP Global Solution — Digital Twin Lunar         ║
╚════════════════════════════════════════════════════════╝

Escolha o cenário:
  [1] Colônia Demo (Mare Tranquillitatis, 6 colonos)
  [2] Colônia Customizada (você define os parâmetros)
> 1
Quantos dias terrestres simular? (padrão 14) > 14
Duração de cada tick (horas, padrão 6) > 6

[12:00:00] INFO Iniciando simulação da colônia 'Tranquility Base' em (0.67°, 23.47°).
[12:00:00] INFO População: 6 | Módulos: 6 | Satélites: 1
[12:00:01] INFO Tick   7 | Idade da colônia: 1.8d | Recursos: [E:2210.0kWh | A:1156.0L | O2:386.2kg | F:91200kcal]
[12:00:01] WARN [12:00:01] WARNING — TEMP-01: Leitura anômala de temperatura: 31.7°C
[12:00:02] INFO Tick  14 | Idade da colônia: 3.5d | Recursos: [E:2020.0kWh | A:1112.0L | O2:372.3kg | F:86400kcal]
...
[12:00:08] INFO Score de Sobrevivência: 73.2/100
                >> Colônia estável. Pode-se planejar expansão.
[12:00:08] INFO Simulação finalizada em 7.84s (tempo real).
```

> **Insira aqui o print do `dotnet run`** mostrando o banner, o avanço dos ticks,
> pelo menos um alerta amarelo de sensor anômalo e o score final.

## 3. Arquivo de histórico

Após a execução, abra `simulation_history.log` (gerado na raiz do projeto).
Cada linha é prefixada por timestamp ISO 8601 em UTC — atende ao critério
"manipulação precisa de DateTime para histórico de dados":

```
[2025-06-09T15:00:00.1234567Z] INFO Iniciando simulação da colônia 'Tranquility Base'...
[2025-06-09T15:00:00.5678910Z] WARN [15:00:00] WARNING — O2-01: Leitura anômala de oxigênio: 17.6%
[2025-06-09T15:00:01.9876543Z] INFO Tick  14 | Idade da colônia: 3.5d | Recursos: ...
```

> **Insira aqui o print do conteúdo de `simulation_history.log`**.

## 4. Cenário de colapso (tratamento de exceção em ação)

Repita com parâmetros agressivos para forçar `ResourceDepletionException`:

```
> 2                 (custom)
Nome da colônia: Stress Test
Latitude:        0
Longitude:       0
Colonos:         20
Painéis:         1
Estufas:         0
Dias:            30
Tick:            6
```

Saída esperada (trecho final):

```
[hh:mm:ss] ERRO Simulação interrompida por colapso de recurso. :: ResourceDepletionException - Recurso 'Alimento' esgotado. Restante: -2400.00.
[hh:mm:ss] INFO Score de Sobrevivência: 11.4/100
                >> Colônia em risco. Reforce os pontos abaixo: ...
```

> **Insira aqui o print do cenário de colapso**, evidenciando que a exceção
> específica foi capturada e a colônia entrou em **modo seguro** em vez de
> derrubar o processo.
