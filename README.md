# Sistema CRUD de Gestión de Pólizas de Seguros

Este repositorio contiene un sistema completo para la gestión de pólizas de seguros, dividido en dos partes: una API backend desarrollada en ASP.NET Core y una aplicación frontend desarrollada con React.

## Estructura del Proyecto

El proyecto está organizado en dos componentes principales:

- **PolizasCRUD.API**: Backend de la aplicación desarrollado con ASP.NET Core 7.0
- **polizas-crud-app**: Frontend de la aplicación desarrollado con React y Vite

## Requisitos Previos

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js](https://nodejs.org/) (versión 18 o superior)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express, LocalDB o instancia completa)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)

## Configuración e Instalación

### 1. Configuración de la Base de Datos

1. Abra SQL Server Management Studio o su herramienta preferida para gestión de SQL Server
2. Cree una nueva base de datos llamada `PolizasCRUD`
3. Opcionalmente, puede restaurar desde el archivo de respaldo (`PolizasCRUD.bak`) si prefiere no ejecutar las migraciones

### 2. Configuración del Backend (PolizasCRUD.API)

1. Navegue al directorio del proyecto API:
   ```bash
   cd PolizasCRUD.API
   ```

2. Modifique la cadena de conexión en `appsettings.json` para que coincida con su instancia SQL Server:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=PolizasCRUD;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

3. Aplicar migraciones para crear el esquema de base de datos:
   
   Usando la consola del administrador de paquetes en Visual Studio:
   ```
   Update-Database
   ```
   
   O usando la CLI de .NET:
   ```bash
   dotnet ef database update
   ```

4. Ejecute la API:
   ```bash
   dotnet run
   ```

   La API estará disponible en:
   - https://localhost:7213
   - http://localhost:5213
   - Swagger UI: https://localhost:7213/swagger

### 3. Configuración del Frontend (polizas-crud-app)

1. Navegue al directorio del frontend:
   ```bash
   cd polizas-crud-app
   ```

2. Modifique la URL de la API en `src/utils/constants.js` para que apunte a su backend:
   ```javascript
   // API Base URL
   export const API_URL = 'https://localhost:7213/api'; // Cambiar a la URL de su backend
   ```

3. Instale las dependencias:
   ```bash
   npm install
   ```

4. Inicie la aplicación en modo desarrollo:
   ```bash
   npm run dev
   ```

   La aplicación estará disponible en:
   - http://localhost:5173

## Datos de Prueba

La aplicación viene preconfigurada con datos iniciales para pruebas:

### Usuario Administrador
- **Usuario**: admin
- **Contraseña**: Admin123


### Otros usuarios (En archivo bak de restauracion)
- **Usuario**: testuser
- **Contraseña**: testuser

### Catálogos
- Tipos de póliza (Vida, Auto, Hogar, Salud, Viaje)
- Estados de póliza (Activa, Cancelada, Vencida, En Trámite)
- Coberturas (Responsabilidad Civil, Robo, Incendio, etc.)

### Cliente de Prueba
- **Cédula**: 123456789
- **Nombre**: Juan Pérez García

Si desea cargar datos adicionales, puede ejecutar el script disponible en `script.sql` o utilizar los inserts proporcionados en `Info BD e INSERTS.txt`.

## Diagrama de la Base de Datos

```
┌───────────────┐         ┌───────────────┐
│    Usuario    │         │    Cliente    │
├───────────────┤         ├───────────────┤
│ Id (PK)       │         │ CedulaAsegurado (PK) │
│ Username      │         │ Nombre        │
│ PasswordHash  │         │ PrimerApellido │
│ PasswordSalt  │         │ SegundoApellido │
│ Email         │         │ TipoPersona   │
│               │         │ FechaNacimiento │
│                         └───────┬───────┘
└───────────────┘                 │
                                  │
       ┌───────────────┐          │          ┌───────────────┐
       │  TipoPoliza   │          │          │  EstadoPoliza │
       ├───────────────┤          │          ├───────────────┤
       │ Id (PK)       │          │          │ Id (PK)       │
       │ Nombre        │◄─────┐   │    ┌─────┤ Nombre        │
       └───────────────┘      │   │    │     └───────────────┘
                              │   │    │
                              │   │    │
┌───────────────┐      ┌─────┴───┴────┴───┐     ┌───────────────┐
│   Cobertura   │      │      Poliza      │     │PolizaCobertura│
├───────────────┤      ├───────────────────┤    ├───────────────┤
│ Id (PK)       │      │ NumeroPoliza (PK) │    │ Id (PK)       │
│ Nombre        │◄─────┤ TipoPolizaId (FK) │    │ NumeroPoliza (FK)│
│ Descripcion   │      │ CedulaAsegurado (FK)│◄──┤ CoberturaId (FK)│
└──────┬────────┘      │ MontoAsegurado    │    └───────────────┘
       │               │ FechaVencimiento  │
       │               │ FechaEmision      │
       │               │ EstadoPolizaId (FK)│
       │               │ Prima             │
       └───────────────┤ Periodo           │
                       │ FechaInclusion    │
                       │ Aseguradora       │
                       └───────────────────┘
```

## Endpoints API

### Autenticación
- `POST /api/Auth/register`: Registrar un nuevo usuario
- `POST /api/Auth/login`: Iniciar sesión 
- `POST /api/Auth/logout`: Cerrar sesión


### Pólizas
- `GET /api/Polizas`: Obtener todas las pólizas
- `GET /api/Polizas/{numeroPoliza}`: Obtener una póliza por número
- `POST /api/Polizas/search`: Búsqueda avanzada de pólizas
- `POST /api/Polizas`: Crear una nueva póliza
- `PUT /api/Polizas/{numeroPoliza}`: Actualizar una póliza
- `DELETE /api/Polizas/{numeroPoliza}`: Eliminar una póliza
- `GET /api/Polizas/tipos`: Obtener tipos de póliza
- `GET /api/Polizas/estados`: Obtener estados de póliza
- `GET /api/Polizas/coberturas`: Obtener coberturas disponibles

## Funcionalidades del Frontend

- Login para acceso seguro al sistema
- CRUD completo de pólizas:
  - Creación de nuevas pólizas con validación de campos
  - Listado y búsqueda avanzada de pólizas
  - Edición de pólizas existentes
  - Eliminación de pólizas
- Validaciones de formato en formularios
- Diseño responsive

## Solución de Problemas

### CORS
Si experimenta problemas de CORS, verifique que la configuración en `Program.cs` del backend incluya la URL correcta del frontend (por defecto http://localhost:5173).

### Migraciones
Si tiene problemas al aplicar las migraciones, puede utilizar los siguientes comandos en la consola del Administrador de Paquetes:

```
Add-Migration [MigrationName]   # Crear nueva migración
Update-Database                 # Aplicar migraciones
Remove-Migration                # Eliminar la última migración
Get-Migration                   # Listar todas las migraciones
Script-Migration                # Generar script SQL
Update-Database -Migration 0    # Revertir todas las migraciones
```

## Mejores Prácticas y Seguridad

- Las contraseñas se almacenan con hash y salt
- Las validaciones se implementan tanto en el backend como en el frontend
- La API está documentada con Swagger
