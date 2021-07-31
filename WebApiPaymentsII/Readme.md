# WebApiPaymentsII
***
## Contenido
1. [Información General](#general-info)
2. [Tecnologías](#technologies)
3. [Instalación](#installation)
4. [Por mejorar](#collaboration)

### Información General
***
Simular una operación de pago típica en una solución de comercio electrónico. 

## Tecnologías
***
* [Visual Studio](https://visualstudio.microsoft.com/vs/preview/vs2022/): Version 2019 (Se recomienda Visual Stdio 2022 Preview)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/): Version 6.0.0
* [Sql Server Data Tools](https://docs.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-ver15): Version 17.0 (Descargue si no está instalado)

## Installation
***
Siga estos pasos para instalar la aplicación. 
```
$ git clone https://github.com/FabianMendoza77/.NET.git
$ cd WebApiPaymentsII
```
Abra la solución WebApiPaymentsII.sln en Visual Studio 2019 en adelante (preferiblemente Visual Studio 2022 Preview) y luego en Package Manager Console:
```
$ Add-Migration Inicial
$ Update-Database
```
Puede observarse la Base de datos creada, mediante Sql Server Object Explorer.

## Por mejorar
***
Este proyecto es una simulación sencilla de una API de pagos. Sinembargo podría mejorarse de la siguiente forma:

> Validación de stock, cuando se superan las existencias de productos.
> Tener un endpoint para mostrar la factura del pedido (por id del pedido).
> Calcular el total de la factura y mostrarlo en el endpoint anterior. 
> Aunque se recomienda que los cálculos se hagan directamente por una capa de alto nivel, como un front-end,  por ejemplo. Estos cálculos no se deberían persistir.
> Estructurarse en capas siguiendo los lineamientos Clean Architecture. Se tenía pensado hacer de la siguiente forma pero por cuestiones de tiempo no fue posible:
