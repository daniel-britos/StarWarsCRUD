# Arquitectura y Desarrollo

Este repositorio contiene la estructura y las configuraciones iniciales para el proyecto `StarWarsCRUD`, diseñado para servir como una base sólida para el desarrollo de aplicaciones con una arquitectura limpia y modular.

## 1. Estructura del Proyecto

La solución `StarWarsCrud` se organiza en varias capas, cada una con responsabilidades bien definidas, siguiendo los principios de la Arquitectura Limpia (Clean Architecture). A continuación, se detalla la estructura de directorios:

```
StarWarsCRUDSolution/
│
├── StarWarsCRUD.Api/                → Capa de presentación (controladores, endpoints)
│   ├── Controllers/                → Controladores de la API (HeroesController, AuthController)
│   ├── DTOs/                       → Objetos de transferencia de datos (para requests/responses)
│   ├── Profiles/                   → Configuraciones de AutoMapper para mapeo de objetos
│   ├── Middlewares/                → Middlewares personalizados para la API
│   ├── Program.cs                  → Punto de entrada y configuración de la aplicación
│   └── appsettings.json            → Configuraciones de la aplicación
│
├── StarWarsCRUD.Application/        → Capa de lógica de negocio (servicios, casos de uso)
│   ├── Interfaces/                 → Definiciones de interfaces para servicios y repositorios
│   ├── Services/                   → Implementaciones de la lógica de negocio y casos de uso
│   ├── Validators/                 → Validaciones de datos (e.g., con FluentValidation)
│   └── Dtos/                       → DTOs internos (si se separan de los de la API)
│
├── StarWarsCRUD.Domain/             → Capa de dominio (entidades puras y lógica del dominio)
│   ├── Entities/                   → Entidades de negocio (Hero, Movie, Power, User)
│   ├── Enums/                      → Enumeraciones del dominio
│   └── Common/                     → Objetos de valor (ValueObjects) o entidades base
│
├── StarWarsCRUD.Infrastructure/     → Acceso a datos, EF Core, repositorios, identidad, etc.
│   ├── Data/                       → Contexto de base de datos (SuperheroesDbContext) y configuraciones
│   │   ├── StarWarsDbContext.cs
│   │   └── Configurations/         → Configuraciones de entidades con Fluent API
│   ├── Repositories/               → Implementaciones de los repositorios de datos
│   ├── Identity/                   → Configuración de usuarios, roles y autenticación
│   ├── Services/                   → Implementaciones de servicios externos (email, logs, etc.)
│   └── DependencyInjection.cs      → Extensión para la inyección de dependencias de infraestructura
│
├── StarWarsCRUD.Tests/              → Pruebas unitarias y de integración
│   ├── ApplicationTests/           → Pruebas para la capa de aplicación
│   ├── InfrastructureTests/        → Pruebas para la capa de infraestructura
│   └── ApiTests/                   → Pruebas para la capa de API
│
└── README.md                       → Este archivo de documentación
```

## 2. Relación entre Capas

La interacción entre las capas sigue un flujo unidireccional, donde las capas externas dependen de las internas, pero no al revés. Esto asegura la independencia del dominio y la facilidad de mantenimiento y prueba.

- **API**: Depende de `Application` e `Infrastructure`.

  - `Application` para utilizar las interfaces de servicios en los controladores.
  - `Infrastructure` para configurar la inyección de dependencias en `Program.cs`.

- **Infrastructure**: Depende de `Application`.

  - Necesita implementar las interfaces de repositorios y servicios definidas en `Application`.

- **Application**: Depende de `Domain`.

  - Utiliza las entidades y la lógica de negocio definidas en la capa de `Domain`.

- **Domain**: No tiene dependencias de ningún otro proyecto. Es el núcleo independiente de la aplicación.

## 3. Primeras Configuraciones

Para comenzar a trabajar con la inyección de dependencias, es necesario agregar las siguientes configuraciones en el archivo `Program.cs` de la capa `StarWarsCRUD.Api`.

En el área de servicios, antes de la línea `var app = builder.Build();`, se debe añadir:

```csharp
builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();
```

Además, asegúrate de incluir los siguientes `using` en la parte superior del archivo `Program.cs`:

```csharp
using StarWarsCRUD.Application.Interfaces;
using StarWarsCRUD.Infrastructure.Repositories;
```

**Nota sobre `AddScoped`:** El método `AddScoped` garantiza que una nueva instancia de `IPersonajeRepository` y `PersonajeRepository` se cree por cada petición HTTP. Esto es ideal para operaciones que interactúan con bases de datos, ya que asegura que cada petición trabaje con su propia instancia, evitando conflictos y manteniendo la operación aislada y ordenada.

## 4. Consideraciones Importantes

**Archivos de Dependencias:** Es crucial **no eliminar** los archivos de dependencias (por ejemplo, `.csproj` o archivos de configuración de paquetes). Estos archivos son fundamentales para gestionar las referencias entre proyectos dentro de la solución, así como las dependencias de paquetes externos (como NuGet). Su eliminación podría romper la compilación y el funcionamiento del proyecto.
