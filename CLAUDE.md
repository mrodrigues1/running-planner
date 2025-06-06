# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Development Commands

**Build the solution:**
```bash
dotnet build
```

**Run tests:**
```bash
dotnet test
```

**Run a specific test:**
```bash
dotnet test --filter "TestMethodName"
```

**Run the console application:**
```bash
dotnet run --project RunningPlanner.Core
```

## Architecture Overview

This is a C# .NET 9.0 solution for generating personalized marathon training plans. The architecture follows a strategy pattern for workout generation and uses Jack Daniels' VDOT system for pace calculations.

### Core Components

**Models Layer (`RunningPlanner.Core/Models/`):**
- `TrainingPlan` - Main entity containing collection of `TrainingWeek` objects
- `TrainingWeek` - Represents weekly structure with workouts for each day
- `Workout` - Individual training session with steps, paces, and descriptions
- `VdotCalculator` - Implements Jack Daniels' VDOT pace calculation system
- `TrainingPaces` - Contains all training paces (easy, marathon, threshold, interval, repetition)

**Plan Generation (`RunningPlanner.Core/PlanGenerator/`):**
- `MarathonPlanGenerator` - Main generator class that orchestrates plan creation
- `MarathonPlanGeneratorParameters` - Input parameters for plan generation
- Uses phases: Base → Build → Peak → Taper → Race

**Workout Strategy Pattern (`RunningPlanner.Core/WorkoutStrategies/`):**
- `IWorkoutStrategy` - Interface for workout generation strategies
- `WorkoutGenerator` - Factory that maps workout types to strategies
- Individual strategies: `EasyRunStrategy`, `LongRunStrategy`, `TempoRunStrategy`, `IntervalsStrategy`, etc.

### Key Design Patterns

- **Strategy Pattern**: Used for workout generation where each workout type has its own strategy
- **Factory Pattern**: `WorkoutGenerator` acts as factory for creating workouts
- **Immutable Records**: Models use C# records for immutability
- **Builder Pattern**: `TrainingPlan.Create()` and `TrainingWeek.Create()` static factory methods

### Training Plan Logic

The plan generator calculates weekly mileages through phases, distributes runs across specified days, and applies the VDOT system for pace calculations. Long run distances are capped based on runner experience level, and step-back weeks are automatically inserted for recovery.