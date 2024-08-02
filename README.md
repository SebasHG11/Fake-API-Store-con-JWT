Este es un proyecto personal realizado con C#, .Net, Entity Framework Core y SQL Server.
Trata sobre una Fake API Store en la cual se pueden realizar todas las operaciones CRUD en las tablas Producto, Categoria y Usuario.
La API tiene autenticación JWT, que se realiza consumiendo un endpoint de LOGIN.
Los usuarios se crean por medio de un endpoint de REGISTRO.
Los usuarios pueden tener dos roles "admin" y "basico", segun su rol pueden consumir endpoints.
Tambien los usuarios pueden realizar Ordenes que se guardan asociando el id del usuario, el id del producto y la cantidad del producto.
