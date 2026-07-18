# Sans Limit

E-commerce full-stack de indumentaria y perfumería streetwear. Catálogo de productos con variantes (talle/color/stock), carrito de compras, checkout, cupones de descuento (con una ruleta interactiva para ganarlos), autenticación de usuarios y un panel de administración con métricas de ventas.

**Demo en vivo:**
- Web: [sanslmit.vercel.app](https://sanslmit.vercel.app)
- API: [sanslimt.onrender.com](https://sanslimt.onrender.com) (la primera request puede tardar ~30-50s si el servicio estaba dormido — plan free de Render)

## Stack

| Capa | Tecnología |
|---|---|
| Frontend | React 19 + Vite, Axios, Recharts (dashboard), SweetAlert2 |
| Backend | .NET 8 Web API (C#) |
| Base de datos | MongoDB (Atlas en producción) |
| Hosting | Vercel (frontend) · Render (API, vía Docker) · MongoDB Atlas (DB) |

## Funcionalidades

- Catálogo de productos filtrable por categoría, con variantes de talle/color y control de stock.
- Carrito de compras y checkout.
- Sistema de cupones de descuento, incluyendo una ruleta de la suerte que otorga cupones al azar.
- Registro e inicio de sesión de usuarios (clientes y administradores).
- Panel de administración: alta/edición/baja de productos y variantes, listado de pedidos, y dashboard de métricas de ventas (ingresos por mes, métodos de pago, ticket promedio).

## Estructura del repo

```
SansLimt.Api/SansLimt.Api/   API .NET 8 (Controllers, Services, Models)
sans-limt-web/                Frontend React + Vite
```

## Correr el proyecto en local

Requisitos: .NET 8 SDK, Node.js, MongoDB corriendo en `localhost:27017`.

**API:**
```bash
cd SansLimt.Api/SansLimt.Api
dotnet run
```
Por defecto levanta en `https://localhost:7094`. La cadena de conexión a Mongo se configura en `appsettings.json` (sección `SansLimitDatabase`).

**Frontend:**
```bash
cd sans-limt-web
npm install
npm run dev
```
Levanta en `http://localhost:5173`. La URL de la API se toma de la variable de entorno `VITE_API_URL` (ver `.env.example`), con `https://localhost:7094` como default para desarrollo.

## Despliegue

- **MongoDB Atlas**: cluster M0 free tier.
- **Render**: Web Service dockerizado (`SansLimt.Api/SansLimt.Api/Dockerfile`), variables de entorno `SansLimitDatabase__ConnectionString`, `SansLimitDatabase__DatabaseName` y `FRONTEND_URL` (para CORS).
- **Vercel**: proyecto apuntando a `sans-limt-web` como root directory, variable de entorno `VITE_API_URL` con la URL pública de Render.
