# Lunar Colony Simulator — Console Edition (FIAP Global Solution)

> Capítulo de C# / .NET da entrega Global Solution.
> Console application em .NET 8 que reproduz, em modo CLI, o núcleo do
> **Lunar Colony Simulator (LCS)** — um Gêmeo Digital lunar operado por IA.

---

## 1. Motivação do projeto

Planejar uma base lunar envolve milhares de variáveis: energia, água, alimento,
oxigênio, manutenção, redundância de sensores e relés orbitais. O LCS nasce como
**simulador educacional** e evolui para um **gêmeo digital corporativo (ESG)**
que ingere telemetria real de clientes para recomendar a configuração mais
eficiente.

Esta entrega em C# é o **núcleo de domínio** do LCS:

- modela a colônia, seus módulos, sensores, satélites e colonos;
- calcula consumo e produção de recursos a cada *tick* simulado;
- aplica um **Core IA** heurístico que devolve `survival score` e recomendações;
- registra histórico com `DateTime` preciso (in-memory + arquivo de log).

A arquitetura completa do produto (React + WebGL → Python/FastAPI → PostgreSQL)
está descrita no documento de arquitetura entregue em PDF; este projeto é o
**equivalente didático em C#** dessa lógica de simulação.

## 2. Como o projeto se integra na solução geral

```
Frontend Web (React)              ───┐
Sensores IoT do cliente (telemetria) ─┤   ┌──> Core IA (este projeto, em C#)
PostgreSQL (histórico)            ───┘    └──> Recomendações ESG / Educacionais
```

O `SimulationEngine` desta entrega corresponde, no produto final, ao serviço
Python/FastAPI que roda na nuvem. As **interfaces** (`ISimulationLogger`,
`ISurvivalCalculator`, `IAlertService`) garantem que cada peça pode ser
substituída por uma implementação real (gRPC, banco, dashboard) sem alterar a
lógica de domínio — exatamente o desacoplamento exigido para um gêmeo digital
que precisa rodar tanto offline (modo aula) quanto online (modo ESG).

## 3. Estrutura de pastas

```
LCS/
├── LunarColonySimulator.csproj
├── Program.cs                       # ponto de entrada
├── README.md
├── docs/
│   ├── DiagramaDeFluxo.md            # diagrama Mermaid
│   └── EvidenciasDeExecucao.md       # evidências do `dotnet run`
├── Common/
│   ├── Constants/LunarConstants.cs   # classe STATIC com constantes
│   └── Exceptions/                   # hierarquia de exceções específicas
├── Domain/
│   ├── Entities/                     # POO: classes públicas, abstratas, sealed
│   │   ├── Module.cs, EnergyModule.cs (abstratas)
│   │   ├── SolarPanel.cs, NuclearReactor.cs, Greenhouse.cs, ...
│   │   ├── Sensor.cs, TemperatureSensor.cs, OxygenSensor.cs, ...
│   │   ├── Satellite.cs, Colonist.cs, Alert.cs
│   │   ├── Colony.cs (partial)       # estado
│   │   └── Colony.Simulation.cs (partial)   # lógica de tick
│   └── ValueObjects/                 # STRUCTS imutáveis
│       ├── Coordinates.cs, ResourceLevel.cs, SensorReading.cs
├── Application/
│   ├── Interfaces/                   # contratos para DI
│   └── Services/                     # ConsoleLogger, FileLogger, AlertService,
│                                     # AISurvivalCalculator, SimulationEngine,
│                                     # DependencyContainer
└── UI/
    └── ConsoleMenu.cs                # leitura de cenário e construção da colônia
```
 

## 4. Como executar

Pré-requisito: **.NET 8 SDK** instalado (`dotnet --version` deve responder 8.x).

```powershell
cd C:\Users\mathe\Downloads\LCS
dotnet build
dotnet run
```

Durante a execução o programa pergunta:

1. Cenário (`1` Demo / `2` Customizado);
2. Quantidade de dias terrestres a simular (padrão 14);
3. Duração de cada tick em horas (padrão 6).

Os logs vão para o console **e** para o arquivo `simulation_history.log`,
gerado na pasta de execução — essa é parte das evidências.

## 5. Autores
Joao Victor Oliveira dos Santos - RM557948
Matheus Alcântara Estevão - RM558193
Nicolle Pellegrino Jelinski - RM558610
Pedro Pereira dos Santos - RM552047
Eric Segawa Montagner- RM558224

