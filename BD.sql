select * from TUsuario


CREATE PROCEDURE RegistroUsuario 
	@Nombre VARCHAR(255),
	@Correo VARCHAR(255),
	@Contrasenna VARCHAR(255),
	@NombreUsuario VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO TUsuario (Nombre, Correo, Contrasenna, NombreUsuario)
	VALUES (@Nombre, @Correo, @Contrasenna, @NombreUsuario);
END;


CREATE PROCEDURE ValidaeIniciarSesion
@Contrasenna VARCHAR(255),
	@NombreUsuario VARCHAR(255)


AS
BEGIN
	SELECT IdUsuario, Nombre, Correo, NombreUsuario, Contrasenna FROM TUsuario
	WHERE NombreUsuario = @NombreUsuario AND Contrasenna = @Contrasenna
END