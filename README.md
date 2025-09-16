# 🧑‍💻 COLAPP - Clean Architecture Template

Este proyecto implementa una arquitectura limpia (**Clean Architecture**) en .NET 8 con Angular en el frontend, diseñada para ser modular, escalable y fácil de mantener.  

Incluye integración con **MediatR**, **CQRS**, **Dapper**, **Stored Procedures**, **Validaciones automáticas** con FluentValidation y **manejo global de errores**.  

---

## 🚀 Tecnologías principales

- **.NET 8** + C#
- **SQL Server** (con Stored Procedures)
- **Dapper** (Micro ORM)
- **MediatR** (CQRS + pipeline behaviors)
- **FluentValidation** (validaciones automáticas)
- **Swagger/OpenAPI** (documentación interactiva)
- **Scrutor** (inyección automática de dependencias con decoradores)

---

## 📂 Estructura del proyecto

```
src/
├── COLAPP.Api                  → Capa de presentación (Controllers, Swagger, Middlewares)
├── COLAPP.Application          → Casos de uso, Commands, Queries, Handlers, Behaviors, Validators
│   ├── Common/Behaviors        → ValidationBehavior, TransactionalBehavior
│   ├── Patients/Commands       → Create, Update, Delete
│   ├── Patients/Queries        → GetAll, GetById
│   └── Validators              → Validadores FluentValidation
├── COLAPP.Domain               → Entidades del dominio (ej: Patient)
├── COLAPP.Infrastructure       → Persistencia (DapperContext, Repositories, UnitOfWork)
├── COLAPP.Shared               → Atributos y helpers comunes (Scoped, Transient, Singleton)
```

---

## 🗂️ Base de datos

### Tabla principal: `Patients`

```sql
CREATE TABLE Patients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DocumentType NVARCHAR(50) NOT NULL,
    DocumentNumber NVARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    BirthDate DATE NOT NULL,
    Email NVARCHAR(150) NULL,
    Gender NVARCHAR(20) NOT NULL,
    Address NVARCHAR(200) NULL,
    PhoneNumber NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 NULL
);
```

### Stored Procedures

- `sp_GetAllPatients`  
- `sp_GetPatientById`  
- `sp_CreatePatient`  
- `sp_UpdatePatient`  
- `sp_DeletePatient`

*(ubicados en la carpeta `/database` para referencia)*

---

## ⚙️ Inyección de dependencias

Se utiliza **Scrutor** para registrar servicios automáticamente con atributos:

```csharp
[Scoped]   // Se registra como Scoped
[Transient] // Se registra como Transient
[Singleton] // Se registra como Singleton
```

Ejemplo en `DependencyInjection.cs`:

```csharp
services.Scan(scan => scan
    .FromAssemblies(typeof(Application.DependencyInjection).Assembly)
    .AddClasses(c => c.WithAttribute<ScopedAttribute>())
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);
```

---

## 🛠️ Pipeline Behaviors

### 🔹 ValidationBehavior
Valida automáticamente todos los **Commands/Queries** con FluentValidation antes de llegar al Handler.  
Si falla, devuelve un `400 Bad Request` con los errores de validación.

### 🔹 TransactionalBehavior
Maneja transacciones de base de datos para operaciones que implementan `ITransactionalRequest`.  
- `BeginTransaction → Handler → SaveChanges → Commit / Rollback`.

---

## 🧩 Validaciones

Ejemplo `CreatePatientCommandValidator`:

```csharp
RuleFor(x => x.Name)
    .NotEmpty().WithMessage("El nombre es obligatorio.")
    .MaximumLength(100);

RuleFor(x => x.BirthDate)
    .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe estar en el pasado");
```

---

## 🌐 Manejo global de errores

Middleware en `Program.cs`:

```csharp
app.UseExceptionHandler(cfg =>
{
    cfg.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationEx)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new {
                Title = "Se encontraron errores de validación",
                Errors = validationEx.Errors.Select(e => new {
                    e.PropertyName,
                    e.ErrorMessage
                })
            });
        }
        else
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new {
                Title = "Error interno en el servidor",
                Message = exception?.Message
            });
        }
    });
});
```

---

## 📖 Ejemplo de request (Swagger)

### ✅ Correcto
```json
{
  "documentType": "CC",
  "documentNumber": "12345678",
  "name": "Eduardo Luna",
  "birthDate": "1995-05-20T00:00:00Z",
  "email": "eduardo@test.com",
  "gender": "M",
  "address": "Calle 123",
  "phoneNumber": "3001234567"
}
```

### ❌ Inválido
```json
{
  "documentType": "NIT",
  "documentNumber": "90505525",
  "name": "",
  "birthDate": "2025-09-16T22:26:33.037Z",
  "email": "bad-email",
  "gender": "",
  "address": "Cra 10",
  "phoneNumber": "321"
}
```

👉 Respuesta (400 Bad Request):

```json
{
  "title": "Se encontraron errores de validación",
  "status": 400,
  "errors": [
    { "field": "Name", "error": "El nombre es obligatorio." },
    { "field": "BirthDate", "error": "La fecha de nacimiento debe estar en el pasado" },
    { "field": "Email", "error": "El correo electrónico no es válido." },
    { "field": "Gender", "error": "El género es obligatorio." }
  ]
}
```

---

## 🧪 Próximos pasos

- Agregar **Unit Tests** para Handlers y Validators (`xUnit + Moq + FluentAssertions`).  
- Agregar **Integration Tests** con base de datos en memoria.  
- Extender repositorios y entidades según reglas de negocio.  
- Conectar frontend **Angular** y consumir los endpoints expuestos.

---

✍️ **Autor**: Eduardo Luna Silva  
📅 **Versión inicial**: Septiembre 2025  
