# Backend - MiMenu (MVP)

## Descripción 
La API REST permite a la aplicación móvil gestionar pedidos de comida. El cliente puede pedir comida para **comer en el local** o **para llevar** (**delivery** no está implementado en este MVP).  
Las funcionalidades y los casos de uso están inspirados en **Mostaza** y en **Mercado Pago Delivery**.


## Características principales
- Autenticación y autorización.
- Validación de datos.
- Encriptación de contraseñas.
- Control de errores y de datos nulos.
- Aplique reglas de negocio segun el modelo.

## Funcionalidades por rol
**Usuario**
- Registrarse e iniciar sesión
- Agregar comidas al carrito
- Canjear y aplicar cupones
- Confirmar pedidos
- Pagar con Mercado Pago

**Admin**
- Gestionar comidas
- Gestionar cupones
- Gestionar banners
- Gestionar categorías

**Local** (no implementado en este MVP)
  - Ver pedidos recibidos
  - Cambiar estado de pedidos
  - Recibir pagos

## Tecnologias
- Lenguaje: C# + .NET 8.0
- Base de datos + ORM: MySQL + Entity Framework Core
- Validaciones: FluentValidation
- Otros: Linq, JWT, .ENV

## Buenas prácticas aplicadas
- Convetional Commits
- Arquitectura en capas
- POO, Interfaces, Enums

## Futuras mejoras
- Implementar combos personalizados e ingredientes.
- Panel del local y pedidos asociados.
- Gestión de direcciones del usuario y pedidos a domicilio.
- Pruebas unitarias y de integración.





