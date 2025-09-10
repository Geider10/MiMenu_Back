# Backend - MiMenu (MVP)

## Descripción 
EL Backend permite realizar pedidos de comida desde la aplicación. El cliente puede pedir para **comer en el local** o **para llevar** (**delivery** no está implementado en este MVP).  
Las funcionalidades y los casos de uso están inspirados en **Mostaza** y en **Mercado Pago Delivery**.


## Características de la API REST
- Autenticación y autorización.
- Validación de datos.
- Encriptación de contraseñas.
- Manejo de errores y validación de datos nulos.
- Implmentación de reglas de negocio por modelo.

## Funcionalidades por rol
**Usuario**
- Registrarse e iniciar sesión
- Agregar comidas al carrito
- Canjear y aplicar cupones
- Confirmar pedidos
- Realizar pagos con Mercado Pago

**Admin**
- Gestionar comidas
- Gestionar cupones
- Gestionar banners
- Gestionar categorías

**Local** (no implementado en este MVP)
- Consultar pedidos recibidos
- Actualizar el estado de los pedidos
- Registrar pagos

## Tecnologias
- Lenguaje: C# + .NET 8.0
- Base de datos + ORM: MySQL + Entity Framework Core
- Validaciones: FluentValidation
- Otros: Linq, JWT, .ENV

## Buenas prácticas aplicadas
- Conventional Commits
- Arquitectura en capas
- POO, Interfaces, Enums

## Futuras mejoras
- Implementar combos personalizados e ingredientes.
- Administración del local y pedidos asociados.
- Implementar pedidos con envío a domicilio y gestionar las direcciones.
- Pruebas unitarias y de integración.





