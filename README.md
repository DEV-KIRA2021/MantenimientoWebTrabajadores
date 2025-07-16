- Listado de trabajadores (desde stored procedure)
- Crear trabajador (mediante modal Bootstrap)
- Editar trabajador (mediante modal Bootstrap)
- Eliminar trabajador (con mensaje de confirmación)
- Filtro por sexo (M/F)
- Colores por sexo: Azul (M), Naranja (F)

## 🛠 Tecnologías utilizadas

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Bootstrap 5
- jQuery

## 🗃 Base de datos

Se utiliza la base de datos `TrabajadoresPrueba` provista por el equipo técnico.  

El store procedure utilizado es:
CREATE PROCEDURE sp_ListarTrabajadores
AS
BEGIN
    SELECT 
        t.Id,
        t.TipoDocumento,
        t.NumeroDocumento,
        t.Nombres,
        t.Sexo,
        d.NombreDepartamento AS Departamento,
        p.NombreProvincia AS Provincia,
        di.NombreDistrito AS Distrito
    FROM Trabajadores t
    INNER JOIN Departamento d ON t.IdDepartamento = d.Id
    INNER JOIN Provincia p ON t.IdProvincia = p.Id
    INNER JOIN Distrito di ON t.IdDistrito = di.Id
END
