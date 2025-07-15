
# 🦘 Sistema de Sucursales - Canguro (Backend)

Aplicación web para administrar sucursales. Este proyecto corresponde al backend desarrollado en ASP.NET Core y SQL Server.

## 📦 Tecnologías
- .NET 7 / ASP.NET Core
- SQL Server
- Dapper (ORM ligero)
- JWT Authentication + Roles
- Procedimientos almacenados
- Logs y validaciones
- Arquitectura con Repositorios

## 🛠️ Instalación

### 1. Clona el repositorio
```bash
git clone https://github.com/TU_USUARIO/canguro-backend.git
cd canguro-backend
```

### 2. Configura `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=CanguroDB;Trusted_Connection=True;"
}
```

### 3. Ejecuta el script SQL
Asegúrate de correr `CanguroDB.sql` para crear tablas, usuarios y procedimientos almacenados.

### 4. Corre el backend
```bash
dotnet run
```
Swagger disponible en: [https://localhost:5001/swagger](https://localhost:5001/swagger)

## 📄 Base de Datos

Incluye:
- Tablas `Sucursal`, `Usuario`, `Moneda`
- SP: `sp_GetSucursales`, `sp_InsertSucursal`, `sp_UpdateSucursal`, `sp_DeleteSucursal`
- Eliminación lógica con campo `Activo`

## 🧪 Usuario de prueba

| Usuario | Contraseña | Rol   |
|---------|------------|-------|
| admin   | 1234       | Admin |

## ✍️ Autor

**Luz Ángela Coronado Martínez**
