# Tablero Kanban
- El proyecto, consiste en una aplicación web para organizar tareas con el método Kanban. Al iniciar la aplicación, lo primero que debemos hacer es loguearnos, para lo cual debemos poner uno de los usuarios existentes: 
```
USUARIOS:               CONTRASEÑAS:    ROL:
gonzaloRobinson             1234        operativo
CristianoRonaldo7           1234        operativo
LionelMessi10               1234        admin
edinson cavani              12345       admin   
```
- Luego de loguearse, el usuario podrá tener distintas funcionalidades dependiendo de si es admin o operativo:
## Un usuario Operativo puede:
### Usuarios
- Modificar sus datos (nombreUsuario, contraseña o rol)
    - En caso de cambiar su rol a Administrador, deberá volver a loguearse para poder usar las funciones de administrador
- Eliminar su cuenta
- No puede modificar o eliminar otros usuarios
- Puede crear nuevos Usuarios
### Tableros
- Ver y listar tableros propios y ajenos
- Crear tableros de los que el será propietario (al presionar el boton crear tablero, automáticamente se creará un tablero para dicho usuario). Un usuario operativo, no puede crear tableros para otros usuarios.
- Eliminar y modificar (nombre o descripcion) de tableros propios
### Tareas
- Listar tareas de tableros propios y ajenos
#### Tareas dentro de Tableros de los que es propietario
- Crear tareas, solamente dentro de algún tablero de los que es propietario. (Si un usuario quiere crear tareas y no tiene ningún tablero, no lo podrá hacer, le saltará todo el tiempo que debe ingresar un tablero)
- Asignar a un usuario una tarea de algún tablero de los que es propietario
- Eliminar una tarea de algún tablero de los que es propietario
#### Tareas asignadas a dicho usuario
- Modificar estado de tareas que le fueron asignadas (estén dentro de tableros propios o ajenos)

## Un Usuario Admin  puede:
- Realizar todas las operaciones que puede realizar un usuario operativo y además:
### Usuario
- Eliminar o modificar datos de otros usuarios
### Tableros
- Eliminar o modificar tableros de otros usuarios
- Solo podrá crear tableros propios(al igual que un usuario operativo)
### Tareas
- Crear tareas para tableros de los que no es propietario (a diferencia de un admin que solo lo podrá hacer para tableros de los que es propietario)
- Eliminar o modificar tareas de otros usuarios
- Cambiar estado de tareas de otros usuarios
- Modificar cualquier dato de una tarea, ya sea descripción, color, estado, usuario asignado, tablero propietario o nombre.
- Solo podrá crear tareas dentro de tableros propios (al igual que un usuario operativo)
