# Práctica Programada 2 - Programación Avanzada

## Integrantes

- Heiner David Calderón Montero
- Alex Felipe Bolaños Alfaro
- Kendal Andres Salas Gonzalez
- Jessica Paola Porras Canales

## Repositorio

https://github.com/1621j/Practica2grupo7

## Arquitectura del Proyecto

La solución fue desarrollada utilizando una arquitectura por capas para mantener la separación de responsabilidades.

### Proyectos

#### Practica2Grupo7.UI
Capa de presentación desarrollada con ASP.NET Core MVC.

#### Practica2Grupo7.BLL
Capa de lógica de negocio.

Contiene:
- DTOs
- Services

#### Practica2Grupo7.DAL
Capa de acceso a datos.

Contiene:
- Entidades
- Repositorios
- DbContext
- Migraciones

## Base de Datos

Se utilizó SQLite como motor de base de datos.

Archivo:

ProductosMedicos.db

## Paquetes NuGet Utilizados

- Microsoft.EntityFrameworkCore.Sqlite (8.0.28)
- Microsoft.EntityFrameworkCore.Design (8.0.28)
- Microsoft.EntityFrameworkCore.Tools (8.0.28)

## Patrones de Diseño Utilizados

### Repository Pattern

Se implementó un repositorio genérico para centralizar las operaciones CRUD y reducir la duplicación de código.

### MVC Pattern

La capa de presentación utiliza el patrón Model View Controller (MVC).

## Funcionalidades Implementadas

### Categorías

- Crear categoría
- Consultar categorías
- Editar categoría
- Eliminar categoría

### Productos

- Crear producto
- Consultar productos
- Editar producto
- Eliminar producto
- Asociación de productos con categorías

## Tecnologías Utilizadas

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- C#
- GitHub