# API REST Registro Usuario

Este es un proyecto API REST creado en .Net Core version .NET 8 con la herramienta Visual Studio para registrar un usuario por medio al endpoint `user` donde sera guardado en una base de datos utilizando Entity Framework **(Code First)** y retornar el mismo como `JSON`.

## Diagrama de Solución

![Diagrama de Registrar Usuario](/Solucion_Registro.jpg "Diagrama")

## Requisitos
* Visual Studio 2022
* .NET 8
* Git
* Postman (Opcional)


## Instalación

1. Clonar el repositorio `RegistroUsuario`
2. Descargar paquetes NuGet `DotNetEnv v3.1.1`,`Microsoft.EntityFrameworkCore v9.0.1`,`Microsoft.EntityFrameworkCore.SqlServer v9.0.1`, `Microsoft.EntityFrameworkCore.Tools v9.0.1`
3. Configurar su conexión de Base de Datos SQL Server en el **ConnectionString** del `appsettings.json`
4. Configurar el archivo .env con las variables `HOST`,`CLIENT_SECRET`,`PASSWORD_REGEXP`.
   > El `HOST` puede ser el local.
   
   > El `CLIENT_SECRET` debe ser un token valido string, puede utilizar un generador de tokens como por ejemplo [mkjwk.org](https://mkjwk.org) y elegir una de las key -> p.
   
5. Abrir el *Package Manager Console** de Visual Studio `Tools > NuGet Package Manager > Package Manager Console`.
6. En la consola ejecutar el comando `Add-Migration [NombreSnapshot]`.
7. Terminada la ejecución del comando anterior, ejecutar el siguiente comando `Update-Database`.

## Ejecución

Luego de la instalación de manera satisfactoria, puede ejecutar el proyecto, viene con un Swagger integrado, sin embargo en caso de probarlo en una herramienta de servicios APIs (Como postman) probarlo con el host `http://localhost:5054` utilizando el endpoint `user`.

Al momento de probar el endpoint de tipo **POST** debe enviar un json con la siguiente estructura:
```json
{
  "name": "string",
  "email": "string",
  "password": "string",
  "phone": [
    {
      "number": "string",
      "citycode": "string",
      "countrycode": "string"
    }
  ]
}
```
