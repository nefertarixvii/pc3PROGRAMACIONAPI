# API Inteligente de Tareas y Análisis

## Descripción

API RESTful desarrollada con ASP.NET Core Web API (.NET 8) para la gestión de tareas internas. La aplicación permite realizar operaciones CRUD, filtrar tareas, consumir una API externa y analizar sentimientos mediante ML.NET.

## Tecnologías utilizadas

* ASP.NET Core Web API (.NET 8)
* Entity Framework Core 8
* SQLite
* Swagger / OpenAPI
* ML.NET
* Git y GitHub

---

## Instalación y ejecución local

### Clonar el repositorio

```bash
https://github.com/nefertarixvii/pc3PROGRAMACIONAPI.git
```

### Ingresar al proyecto

```bash
cd ApiInteligenteTareas
```

### Restaurar dependencias

```bash
dotnet restore
```

### Ejecutar la aplicación

```bash
dotnet run
```

Swagger estará disponible en:

```text
https://localhost:5021/Swagger
```

---

## Migraciones

### Crear migración

```bash
dotnet ef migrations add InitialCreate
```

### Aplicar migración

```bash
dotnet ef database update
```

---

## Base de datos

Se utiliza SQLite como motor de base de datos.

Archivo generado:

```text
app.db
```

---

## Endpoints implementados

### Gestión de tareas

| Método | Endpoint         |
| ------ | ---------------- |
| GET    | /api/tareas      |
| GET    | /api/tareas/{id} |
| POST   | /api/tareas      |
| PUT    | /api/tareas/{id} |
| DELETE | /api/tareas/{id} |

### Filtros

Filtrar por estado:

```http
GET /api/tareas?estado=Pendiente
```

Filtrar por prioridad:

```http
GET /api/tareas?prioridad=Alta
```

Filtrar por rango de fechas:

```http
GET /api/tareas?fechaInicio=2026-05-01&fechaFin=2026-05-31
```

---

## API externa

Se consume la API:

https://jsonplaceholder.typicode.com/todos

### Endpoints

| Método | Endpoint                  |
| ------ | ------------------------- |
| GET    | /api/tareas-externas      |
| GET    | /api/tareas-externas/{id} |

### Ejemplo

Solicitud:

```http
GET /api/tareas-externas/1
```

Respuesta:

```json
{
  "externalId": 1,
  "titulo": "delectus aut autem",
  "completado": false
}
```

---

## ML.NET – Análisis de sentimiento

### Endpoint

```http
POST /api/ml/sentimiento
```

### Request

```json
{
  "comentario": "La tarea fue completada correctamente y el sistema funciona bien"
}
```

### Response

```json
{
  "comentario": "La tarea fue completada correctamente y el sistema funciona bien",
  "sentimiento": "Positivo"
}
```

### Modelo utilizado

Se implementó un modelo de clasificación binaria utilizando ML.NET y el algoritmo SDCA Logistic Regression.

El modelo fue entrenado con un dataset propio que contiene ejemplos de frases positivas y negativas.

---

## Dataset utilizado

Archivo:

```text
Dataset/sentimientos.csv
```

Contiene frases etiquetadas como positivas o negativas para entrenar el modelo de análisis de sentimiento.

---

## Control de versiones

Se trabajó utilizando ramas independientes para cada requerimiento:

* feature/api-tareas
* feature/filtros-tareas
* feature/api-externa-todos
* feature/mlnet-basico

Cada funcionalidad fue integrada mediante Pull Requests hacia la rama principal `main`.

---

## Autor

Proyecto desarrollado para la evaluación de ASP.NET Core Web API, Entity Framework Core, SQLite, consumo de APIs externas y ML.NET.
