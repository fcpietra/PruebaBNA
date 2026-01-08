# PruebaBNA - Integración Bancaria con .NET 8

Este proyecto es una solución backend desarrollada con **.NET 8** siguiendo los principios de **Clean Architecture**. Implementa un sistema de gestión de clientes y cuentas bancarias, integrándose con la API pública del **BCRA (Banco Central de la República Argentina)** para consultar la situación crediticia de los clientes.

## 🚀 Tecnologías y Patrones

* **.NET 8** (LTS)
* **Clean Architecture** (Domain, Application, Infrastructure, Api)
* **Entity Framework Core** (con SQLite)
* **Serilog** (Logging estructurado en consola y archivo)
* **MemoryCache** (Caché en memoria con Options Pattern)
* **Health Checks** (Monitoreo de DB y API externa)
* **HttpClientFactory** (Consumo resiliente de APIs)

---

## 🛠️ Requisitos Previos

Asegurate de tener instalado:

1.  [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2.  Git
3.  Un cliente HTTP (Insomnia, Postman) o navegador web.
4.  (Opcional) VS Code con extensión **SQLite** o **DBeaver** para ver la base de datos.

---

## ⚙️ Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone [https://github.com/fcpietra/PruebaBNA.git](https://github.com/fcpietra/PruebaBNA.git)
cd PruebaBNA
```
### 2. Restaurar dependencias
```bash
dotnet restore
```

### 3. Crear la Base de Datos (SQLite)

Como la base de datos no se sube al repositorio (está en .gitignore), debés ejecutar las migraciones para crear el archivo local PruebaBNA.db.
```bash
dotnet tool install --global dotnet-ef  # Solo si no lo tenés instalado
dotnet ef database update --project PruebaBNA.Infrastructure --startup-project PruebaBNA.Api
```

Esto creará el archivo PruebaBNA.db dentro de la carpeta PruebaBNA.Api (o en la raíz, dependiendo de tu entorno).
## ▶️ Instrucciones de Ejecución

Para iniciar la API, ejecutá el siguiente comando desde la raíz de la solución:
```bash
dotnet run --project PruebaBNA.Api
```

Verás logs de Serilog indicando que la aplicación inició.

    URL Base: http://localhost:5100 (o el puerto que indique tu consola).

    Logs: Se guardan automáticamente en la carpeta /Logs dentro del proyecto API.

## 🧪 Datos de Prueba (Seeding)

Para probar la integración real con el BCRA, necesitás un cliente con un CUIT válido. Podés ejecutar este script SQL en tu base de datos usando tu visor de SQLite preferido:
```sql
-- Insertar Cliente (Banco Nación para probar integración BCRA)
INSERT INTO Clientes (Id, Nombre, Apellido, Cuit, CreatedAt) 
VALUES (1, 'Entidad', 'Prueba', '30500010912', datetime('now'));

-- Insertar Cuenta
INSERT INTO Cuentas (Id, ClienteId, Numero, CodSucursal, Saldo, CreatedAt) 
VALUES (1, 1, '0000123456', '011', 150000.50, datetime('now'));

-- Insertar Movimientos
INSERT INTO Movimientos (Id, CuentaId, Fecha, Tipo, Descripcion, Importe, CreatedAt) 
VALUES (1, 1, datetime('now', '-5 days'), 'HaberesSueldoAcreditado', 'Sueldo Mensual', 200000.00, datetime('now'));
```
##📡 Endpoints Disponibles
1. Health Check

Verifica el estado de la base de datos y la conexión con la API del BCRA.

    GET /health

    Respuesta:

```json

{
  "status": "Healthy",
  "checks": [ ... ]
}
```
2. Consultar Cliente (Integración BCRA)

Devuelve datos del cliente, sus cuentas y consulta en tiempo real (o caché) la situación en la Central de Deudores.

    GET /api/clientes/{cuit}

    Ejemplo: http://localhost:5100/api/clientes/30500010912

3. Listar Todos los Clientes

    GET /api/clientes

4. Consultar Cuenta y Movimientos

Devuelve el saldo y los últimos movimientos de una cuenta específica.

    GET /api/cuentas/{id}

    Ejemplo: http://localhost:5100/api/cuentas/1

## 🧩 Arquitectura

La solución está dividida en 4 proyectos para asegurar desacoplamiento:

    Domain: Entidades del núcleo (Cliente, Cuenta, Movimiento) y Enums. Sin dependencias externas.

    Application: Interfaces (IBcraService, IApplicationDbContext), DTOs y Servicios de Lógica de Negocio.

    Infrastructure: Implementación de EF Core, Migraciones, HttpClient para BCRA y MemoryCache.

    Api: Controladores REST, configuración de Inyección de Dependencias y Health Checks.
