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
Ejecute desde Visual Studio, se le pedirá crear un certificado local de seguridad.
Se abrirá Swagger. El endpoint que permite realizar el pago es el POST: api/clientes/{clienteId}/pedidos/{pedidoId}


## Por mejorar
***
Este proyecto es una simulación sencilla de una API de pagos. Sinembargo podría mejorarse de la siguiente forma:

1. Validación de stock, cuando se superan las existencias de productos.
2. Tener un endpoint para mostrar la factura del pedido (por id del pedido).
3. Calcular el total de la factura y mostrarlo en el endpoint anterior. 
   Aunque se recomienda que los cálculos se hagan directamente por una capa de alto nivel, como un front-end,  por ejemplo. Estos cálculos no se deberían persistir.
4. Estructurarse en capas siguiendo los lineamientos de Clean Architecture. Se estaba refactorizando de esta forma, pero por cuestiones de tiempo no fue posible terminarlo:

* Adaptadores: 
  * Tuya.Pagos.API
  * Tuya.Pagos.Repositorio
* Comun:
  * Tuya.Pagos.Comun
* Dominio:
  * Tuya.Pagos.UseCase
  * Tuya.Pagos.Dominio


Siguiendo esta estructura:

![Image text](http://xurxodev.com/content/images/2020/03/bloc-clean-architecture.png)

