# API de Gestión de Pólizas

Esta API proporciona un backend completo para la gestión de pólizas de seguros, incluyendo autenticación, CRUD de clientes y pólizas, y búsqueda avanzada.

## Tecnologías utilizadas

- ASP.NET Core 7.0
- Entity Framework Core 7.0
- SQL Server
- JWT para autenticación
- Swagger para documentación

## Requisitos

- Visual Studio 2022
- SQL Server (Local o Express)
- .NET 7.0 SDK

## Configuración y ejecución

1. Clonar o descargar el repositorio

2. Configurar la cadena de conexión de la base de datos:
   - Abrir el archivo `appsettings.json`
   - Modificar la cadena de conexión `DefaultConnection` con su configuración de SQL Server

3. Abrir la solución en Visual Studio 2022

4. Abrir la Consola del Administrador de Paquetes y ejecutar las migraciones:
   ```
   Update-Database
   ```

5. Ejecutar la aplicación (F5)
   - La API se ejecutará en https://localhost:5001 o http://localhost:5000
   - Swagger UI estará disponible en https://localhost:5001/swagger o http://localhost:5000/swagger

## Estructura de la API

### Autenticación

- `POST /api/Auth/register`: Registrar un nuevo usuario
- `POST /api/Auth/login`: Iniciar sesión y obtener token
- `POST /api/Auth/refresh-token`: Renovar token de acceso
- `POST /api/Auth/logout`: Cerrar sesión

### Clientes

- `GET /api/Clientes`: Obtener todos los clientes
- `GET /api/Clientes/{cedula}`: Obtener un cliente por cédula
- `POST /api/Clientes`: Crear un nuevo cliente
- `PUT /api/Clientes/{cedula}`: Actualizar un cliente
- `DELETE /api/Clientes/{cedula}`: Eliminar un cliente
- `GET /api/Clientes/search?term={termino}`: Buscar clientes

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

## Datos de prueba

La aplicación incluye datos iniciales para:
- Tipos de póliza (Vida, Auto, Hogar, Salud, Viaje)
- Estados de póliza (Activa, Cancelada, Vencida, En Trámite)
- Coberturas (Responsabilidad Civil, Robo, Incendio, etc.)

## Configuración para el frontend

La API está configurada para aceptar solicitudes desde http://localhost:5173 (Vite React por defecto).

Si necesita cambiar esta configuración, modifique la política CORS en el archivo `Program.cs`.