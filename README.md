# Gestión de Ventas – Tienda de productos electrónicos

**Laboratorio 3 – Programación Orientada a Eventos**
Universidad de Oriente · Ingeniería en Desarrollo de Software

Aplicación de escritorio en **C# con Windows Forms (.NET Framework 4.7.2)** para gestionar productos, clientes y ventas de una tienda de electrónicos. Usa **SQL Server** y **RepoDb** como ORM, con arquitectura por capas y repositorios genéricos.

## Requisitos del laboratorio

| Requisito | Dónde se cumple |
|---|---|
| Login con SHA-256 | `Clases/Seguridad.cs`, `EmpleadoRepositorio.Validar`, `frmLogin.cs` |
| RepoDb con repositorios genéricos | `IRepository.cs`, `RepositorioBase.cs` y un repositorio por tabla |
| Formulario principal con menú | `Formularios/Panel.cs` (MDI) |
| Formulario hijo modal (`ShowDialog()`) | `frmProductoModal`, `frmNuevaVenta`, `frmDetalleVenta` |
| Reportes de productos vendidos y stock | `frmReportes.cs`, `RepositorioReporteVentas.cs`, `Reportes/` (PDF) |
| **Bonus:** semáforo de stock | Rojo con 5 unidades o menos, ámbar de 6 a 10 (también en el PDF) |

## Tecnologías

| Tecnología | Uso |
|---|---|
| C# / Windows Forms (.NET 4.7.2) | Interfaz de escritorio |
| SQL Server Express + SSMS | Base de datos `GestionVentasDb` |
| RepoDb 1.17.0 | ORM y acceso a datos |
| Microsoft.Data.SqlClient 7.1.0 | Conexión con SQL Server |
| PDFsharp-MigraDoc-GDI 1.50.5147 | Exportación a PDF |

Los paquetes NuGet se restauran solos al abrir la solución.

## Cómo ejecutarlo

1. **Base de datos.** En SSMS ejecutar, **en orden**, los scripts de `Scripts/`:
   1. `Script_Lab2_GestionVentasDb.sql`: crea la base con tablas y datos de ejemplo (ES DEL LAB 2 DE POE).
   2. `Script_Lab3_Modificaciones.sql`: se modifica `usuario` y `clave` (SHA-256) a `Empleados` y deja productos con stock bajo.
2. **Proyecto.** Abrir `GestionVentas.sln` en Visual Studio.
3. **Conexión.** En `App.config` cambiar `.\SQLEXPRESS` por tu servidor SQL.
4. **Ejecutar** con F5.

### Usuarios de prueba

| Usuario | Clave | Empleado |
|---|---|---|
| `rcruz` | `rafael123` | Rafael Cruz (Vendedor) |
| `mcorvera` | `meybeline123` | Meybeline Corvera (Vendedora) |
| `fcruz` | `fernando123` | Fernando Cruz (Cajero) |

## Arquitectura

```
GestionVentas/
├── Formularios/     Presentación (login, panel MDI, productos, clientes, ventas, reportes)
├── Clases/          Modelos, clases para grillas/reportes y utilidades (Seguridad, Sesion, Notificador)
├── Repositorios/    Acceso a datos con RepoDb
├── Reportes/        Reportes en PDF
└── Program.cs       Configura RepoDb y controla el ciclo login → panel
```

**Flujo:** formulario valida → repositorio → RepoDb → SQL Server. Al guardar, el `Notificador` refresca las grillas abiertas.

## Seguridad: login con SHA-256

La contraseña **nunca se guarda en texto plano**; se almacena su hash SHA-256 (64 caracteres hex).

1. El empleado escribe usuario y clave.
2. `Seguridad.CalcularHash` calcula el hash de la clave.
3. `EmpleadoRepositorio.Validar` busca usuario y hash coincidentes.
4. Si coinciden se abre el panel; si no, se niega el acceso.

## RepoDb y repositorio genérico

- Los modelos se mapean con atributos (`[Map]`, `[Primary]`, `[Identity]`).
- `IRepository<T>` define `ObtenerTodos`, `ObtenerPorId`, `Insertar`, `Actualizar` y `Eliminar`; `RepositorioBase<T>` los implementa una sola vez.
- `VentaRepositorio.RegistrarVenta` guarda cabecera, detalle y descuento de stock en **una sola transacción**.

## Módulos del sistema

- **Productos:** catálogo con buscador, semáforo de stock y alta/edición en ventana **modal**.
- **Clientes:** CRUD con validación de DUI (`########-#`), correo y duplicados.
- **Ventas:** historial, nueva venta con carrito y control de existencias, detalle en ventana modal.
- **Reportes:** productos vendidos y stock, por producto o catálogo completo, exportables a **PDF**.

## Autora

**Meybeline Abigail Corvera Cruz** – U20261631
Cátedra: Programación Orientada a Eventos · Docente: Ing. Ingrid Roxana Argueta de Contreras
