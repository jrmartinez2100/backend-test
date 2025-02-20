# **Backend Test**  

Este es un proyecto de API REST en C# que utiliza:  

- **.NET** como framework backend  
- **Entity Framework Core** para acceso a datos  
- **PostgreSQL** como base de datos  
- **XUnit** para pruebas unitarias  
- **Docker Compose** para la configuración del entorno  

---

## **⚙ Requisitos previos**  

Antes de ejecutar el proyecto, asegúrate de tener instalados:  

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)  
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)  
- [Git](https://git-scm.com/)  
- (Opcional) [PostgreSQL](https://www.postgresql.org/download/) si no usarás Docker  

---

## *Instrucciones de ejecución**  

### **1️ Abrir la solución**  
- Abre **Visual Studio** y carga la solución `backend-test.sln`.  
- Alternativamente, puedes abrir la terminal y navegar al directorio del proyecto:  

  ```sh
  cd backend-test
  ```

---

### **2️ Generar los contenedores con Docker**  
Ejecuta el siguiente comando para iniciar los contenedores de la base de datos, el backend API y las pruebas unitarias:  

```sh
docker-compose up --build
```

> **Nota:** Este comando iniciará todos los servicios definidos en `docker-compose.yml`.  

---

### **3️ Verificar los contenedores en ejecución**  
Para comprobar que los contenedores están activos, usa:  

```sh
docker ps
```

Deberías ver los contenedores para:  
- **postgres-db** (base de datos PostgreSQL)
- **backend-api** (API REST)
- **test-backend** (pruebas unitarias)

---

### **4️ Probar la API**  

Accede a **Swagger** para probar los endpoints:  

👉 [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)  



