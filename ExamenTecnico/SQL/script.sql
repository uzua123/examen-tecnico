CREATE DATABASE ExamenTecnicoDB;
GO

USE ExamenTecnicoDB;
GO

CREATE TABLE tbResultado (
    IdResultado INT IDENTITY(1,1) PRIMARY KEY,
    ValorResultado VARCHAR(MAX)
);
GO

-- SELECT * FROM [dbo].[tbResultado]

CREATE ALTER PROC [dbo].[resultado_insert] (
    @ValorResultado VARCHAR(MAX)
)
AS
BEGIN
    -- Tabla temporal para almacenar los valores divididos
    DECLARE @TablaTemporal TABLE (Valor VARCHAR(MAX));

    -- Dividir el string en valores individuales usando STRING_SPLIT
    INSERT INTO @TablaTemporal (Valor)
    SELECT value
    FROM STRING_SPLIT(@ValorResultado, ',');

    -- Insertar cada valor en la tabla tbResultado
    INSERT INTO tbResultado (ValorResultado)
    SELECT Valor
    FROM @TablaTemporal;

    -- Devuelve el número de registros insertados
    SELECT @@ROWCOUNT AS RegistrosInsertados;
END;
GO

CREATE TABLE tbCargo (
    idCargo INT IDENTITY(1,1) PRIMARY KEY,
    ValorCargo VARCHAR(50)
);

-- Listar todos los cargos
CREATE PROCEDURE cargo_select_all
AS
BEGIN
    SELECT idCargo, ValorCargo FROM tbCargo;
END;
GO

-- Buscar un cargo por ID
CREATE PROCEDURE cargo_select_by_id
    @idCargo INT
AS
BEGIN
    SELECT idCargo, ValorCargo FROM tbCargo WHERE idCargo = @idCargo;
END;
GO

-- Insertar un nuevo cargo
CREATE PROCEDURE cargo_insert
    @ValorCargo VARCHAR(50)
AS
BEGIN
    INSERT INTO tbCargo (ValorCargo) VALUES (@ValorCargo);
END;
GO

-- Actualizar un cargo existente
CREATE PROCEDURE cargo_update
    @idCargo INT,
    @ValorCargo VARCHAR(50)
AS
BEGIN
    UPDATE tbCargo SET ValorCargo = @ValorCargo WHERE idCargo = @idCargo;
END;
GO

-- Eliminar un cargo
CREATE PROCEDURE cargo_delete
    @idCargo INT
AS
BEGIN
    DELETE FROM tbCargo WHERE idCargo = @idCargo;
END;
GO