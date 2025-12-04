CREATE TABLE Medicamentos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    categoria VARCHAR(100) NOT NULL,
    cantidad INT NOT NULL,
    fecha_expiracion DATE NOT NULL
);
