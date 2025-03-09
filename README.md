--CREATE DATABASE Proyecto_Algoritmos_BDD;

USE Proyecto_Algoritmos_BDD;
USE u22;

----------------------------------------------------- TABLAS -----------------------------------------------------

CREATE TABLE Permisos (
	ID_Permiso INT PRIMARY KEY IDENTITY,
	Nombre_Permiso VARCHAR(80) UNIQUE
);

CREATE TABLE Perfiles (
	ID_Perfil INT PRIMARY KEY IDENTITY,
	Nombre_Perfil VARCHAR(40) UNIQUE
);

CREATE TABLE PermisosXPerfiles (
	PermisoXPerfil INT PRIMARY KEY IDENTITY,
	ID_Perfil INT,
	ID_Permiso INT,
	FOREIGN KEY (ID_Perfil) REFERENCES Perfiles(ID_Perfil),
	FOREIGN KEY (ID_Permiso) REFERENCES Permisos(ID_Permiso)
);

CREATE TABLE Empleados (
	ID_Empleado INT PRIMARY KEY IDENTITY,
	ID_Perfil INT,
	Nombre_Empleado VARCHAR(30) NOT NULL,
	Apellido_Empleado VARCHAR(30) NOT NULL,
	DNI_Empleado INT NOT NULL UNIQUE,
	Domicilio_Calle VARCHAR(40),
	Domicilio_Numero INT,
	Telefono VARCHAR(20),
	Email VARCHAR(50) NOT NULL,
	Usuario_Empleado VARCHAR(50) NOT NULL UNIQUE,
	Contrasenia_Empleado VARCHAR(40) NOT NULL,
	Fecha_Baja DATE NULL,
	Fecha_Alta DATE NULL,
	FOREIGN KEY (ID_Perfil) REFERENCES Perfiles(ID_Perfil),
	--CONSTRAINT Empleados UNIQUE (DNI_Empleado, Usuario_Empleado)
	--CONSTRAINT Empleados NOT NULL (Nombre_Empleado, Apellido_Empleado, DNI_Empleado, Usuario_Empleado, Contrasenia_Empleado, Email),	--Mejor definirlos directamente en los campos
);

CREATE TABLE Alumnos (
	ID_Alumno INT PRIMARY KEY IDENTITY,
	ID_Perfil INT,
	Matricula INT NOT NULL UNIQUE,
	Nombre_Alumno VARCHAR(30) NOT NULL,
	Apellido_Alumno VARCHAR(30) NOT NULL,
	DNI_Alumno INT NOT NULL UNIQUE,
	Domicilio_Calle VARCHAR(40),
	Domicilio_Numero INT,
	Telefono VARCHAR(20),
	Email VARCHAR(50) NOT NULL,
	Usuario_Alumno VARCHAR(50) NOT NULL UNIQUE,
	Contrasenia_Alumno VARCHAR(40) NOT NULL,
	Fecha_Baja DATE NULL,
	Fecha_Alta DATE DEFAULT GETDATE(), --Valor predeterminado: fecha actual (si no se le asigna nada)
	FOREIGN KEY (ID_Perfil) REFERENCES Perfiles(ID_Perfil),
	--CONSTRAINT Alumnos NOT NULL (Matricula, Nombre_Alumno, Apellido_Alumno, DNI_Alumno, Usuario_Alumno, Contrasenia_Alumno, Email),
	--CONSTRAINT Alumnos UNIQUE (Matricula, DNI_Alumno, Usuario_Alumno)
);

CREATE TABLE Instancias (
	ID_Instancia INT PRIMARY KEY IDENTITY,
	Nombre_Instancia VARCHAR(30)
);

CREATE TABLE Materias (
	ID_Materia INT PRIMARY KEY IDENTITY,
	Nombre_Materia VARCHAR(60) NOT NULL,
	--Anio INT NOT NULL CHECK (Anio >= 1)		Lo saque xq ahora el año se saca de AñosDeCarrera
);

CREATE TABLE Examenes (
	ID_Examen INT PRIMARY KEY IDENTITY,
	ID_Materia INT,
	ID_Instancia INT,
	ID_Alumno INT,
	Nota FLOAT NOT NULL CHECK (Nota BETWEEN 0 AND 10),
	Fecha DATE,
	Libro INT,
	Folio INT,
	FOREIGN KEY (ID_Materia) REFERENCES Materias(ID_Materia),
	FOREIGN KEY (ID_Instancia) REFERENCES Instancias(ID_Instancia),
	FOREIGN KEY (ID_Alumno) REFERENCES Alumnos(ID_Alumno)
);

CREATE TABLE Carreras (
	ID_Carrera INT PRIMARY KEY IDENTITY,
	Nombre_Carrera VARCHAR(50) UNIQUE,
	Resolucion VARCHAR(20),
	Programa VARCHAR(50)
);

--Tabla para representar los años de una carrera en particular (cada carrera tendra 3 registros en esta tabla, uno por año)
CREATE TABLE AñosDeCarrera (
    ID_AñoDeCarrera INT PRIMARY KEY IDENTITY,
    ID_Carrera INT,
    Año INT CHECK (Año BETWEEN 1 AND 3) NOT NULL, -- Año de la carrera: solo permite 1, 2 y 3
    FOREIGN KEY (ID_Carrera) REFERENCES Carreras(ID_Carrera)  -- Cada año pertenece a una carrera
);

CREATE TABLE MateriasXCarreras (
	ID_MateriaXCarrera INT PRIMARY KEY IDENTITY,
	ID_Materia INT,
	ID_Carrera INT,
	ID_AñoDeCarrera INT,	--Agregado para manejar las materias por año
	FOREIGN KEY (ID_Materia) REFERENCES Materias(ID_Materia),
	FOREIGN KEY (ID_Carrera) REFERENCES Carreras(ID_Carrera),
	FOREIGN KEY (ID_AñoDeCarrera) REFERENCES AñosDeCarrera(ID_AñoDeCarrera)
);

CREATE TABLE ProfesoresXMaterias (
	ID_EmpleadoXMateria INT PRIMARY KEY IDENTITY,
	ID_Materia INT,
	ID_Empleado INT,
	FOREIGN KEY (ID_Materia) REFERENCES Materias(ID_Materia),
	FOREIGN KEY (ID_Empleado) REFERENCES Empleados(ID_Empleado)
);

CREATE TABLE MateriasXAlumnos (
	ID_AlumnosXMateria INT PRIMARY KEY IDENTITY,
	ID_Materia INT,
	ID_Alumno INT,
	FOREIGN KEY (ID_Materia) REFERENCES Materias(ID_Materia),
	FOREIGN KEY (ID_Alumno) REFERENCES Alumnos(ID_Alumno)
);

----------------------------------------------------- REGISTROS -----------------------------------------------------

INSERT INTO Carreras (Nombre_Carrera, Resolucion, Programa)
VALUES 
    ('Analista de Sistemas', 'Resolución 1234/2020', 'Programa de Análisis y Desarrollo de Sistemas'),
    ('Técnico Superior en Publicidad', 'Resolución 5678/2020', 'Programa de Estrategias Publicitarias');


INSERT INTO Perfiles (Nombre_Perfil)
VALUES 
    ('Administrador'),
    ('Personal Administrativo'),
    ('Docente'),
    ('Alumno');


INSERT INTO Instancias (Nombre_Instancia)
VALUES 
    ('1er Parcial'),
    ('Recup. 1er Parcial'),
    ('2do Parcial'),
    ('Recup. 2do Parcial'),
    ('Final');


INSERT INTO Empleados (ID_Perfil, Nombre_Empleado, Apellido_Empleado, DNI_Empleado, Domicilio_Calle, Domicilio_Numero, Telefono, Email, Usuario_Empleado, Contrasenia_Empleado, Fecha_Baja, Fecha_Alta)
VALUES
    (1, 'Lautaro', 'Ortiz', 41345678, 'Mitre', 174, '22323456789', 'lauta_ortiz@email.com', 'l', 'l', NULL, GETDATE()),  -- Administrador

    (1, 'Juan', 'Pérez', 12345678, 'Calle A', 123, '01123456789', 'juan.perez@email.com', 'jperez', 'contrasenia123', NULL, GETDATE()),  -- Administrador
    (2, 'Ana', 'Gómez', 23456789, 'Calle B', 456, '01123456780', 'ana.gomez@email.com', 'agomez', 'contrasenia123', NULL, GETDATE()),  -- Personal Administrativo
    (2, 'Luis', 'Fernández', 34567890, 'Calle C', 789, '01123456781', 'luis.fernandez@email.com', 'lfernandez', 'contrasenia123', NULL, GETDATE()),  -- Personal Administrativo
    (3, 'María', 'López', 45678901, 'Calle D', 101, '01123456782', 'maria.lopez@email.com', 'mlopez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Carlos', 'Martínez', 56789012, 'Calle E', 102, '01123456783', 'carlos.martinez@email.com', 'cmartinez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Laura', 'Rodriguez', 67890123, 'Calle F', 103, '01123456784', 'laura.rodriguez@email.com', 'lrodriguez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Diego', 'Hernández', 78901234, 'Calle G', 104, '01123456785', 'diego.hernandez@email.com', 'dhernandez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Sofía', 'Pérez', 89012345, 'Calle H', 105, '01123456786', 'sofia.perez@email.com', 'sperez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Javier', 'Sánchez', 90123456, 'Calle I', 106, '01123456787', 'javier.sanchez@email.com', 'jsanchez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Gabriela', 'Gutiérrez', 12345679, 'Calle J', 107, '01123456788', 'gabriela.gutierrez@email.com', 'ggutierrez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Fernando', 'Cruz', 23456780, 'Calle K', 108, '01123456789', 'fernando.cruz@email.com', 'fcruz', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Martina', 'Ramos', 34567891, 'Calle L', 109, '01123456790', 'martina.ramos@email.com', 'mramos', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Santiago', 'Vázquez', 45678902, 'Calle M', 110, '01123456791', 'santiago.vazquez@email.com', 'svazquez', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Pablo', 'Alonso', 56789123, 'Calle N', 111, '01123456792', 'pablo.alonso@email.com', 'palonso', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Lucía', 'Castro', 67891234, 'Calle O', 112, '01123456793', 'lucia.castro@email.com', 'lcastro', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Roberto', 'Farias', 78912345, 'Calle P', 113, '01123456794', 'roberto.farias@email.com', 'rfarias', 'contrasenia123', NULL, GETDATE()),  -- Docente
    (3, 'Elena', 'Navarro', 89123456, 'Calle Q', 114, '01123456795', 'elena.navarro@email.com', 'enavarro', 'contrasenia123', NULL, GETDATE());  -- Docente


	--Alumnos de Sistemas
INSERT INTO Alumnos (ID_Perfil, Matricula, Nombre_Alumno, Apellido_Alumno, DNI_Alumno, Domicilio_Calle, Domicilio_Numero, Telefono, Email, Usuario_Alumno, Contrasenia_Alumno, Fecha_Baja, Fecha_Alta)
VALUES 
    (4, 1001, 'Pedro', 'González', 11111111, 'Calle A', 1, '01123456700', 'pedro.gonzalez@email.com', 'pgonzalez', 'contrasenia123', NULL, GETDATE()),
    (4, 1002, 'Lucía', 'Martínez', 22222222, 'Calle B', 2, '01123456701', 'lucia.martinez@email.com', 'lmartinez', 'contrasenia123', NULL, GETDATE()),
    (4, 1003, 'Martín', 'López', 33333333, 'Calle C', 3, '01123456702', 'martin.lopez@email.com', 'mlopez', 'contrasenia123', NULL, GETDATE()),
    (4, 1004, 'Camila', 'Gómez', 44444444, 'Calle D', 4, '01123456703', 'camila.gomez@email.com', 'cgomez', 'contrasenia123', NULL, GETDATE()),
    (4, 1005, 'Ignacio', 'Fernández', 55555555, 'Calle E', 5, '01123456704', 'ignacio.fernandez@email.com', 'ifernandez', 'contrasenia123', NULL, GETDATE()),
    (4, 1006, 'Valentina', 'Sánchez', 66666666, 'Calle F', 6, '01123456705', 'valentina.sanchez@email.com', 'vsanchez', 'contrasenia123', NULL, GETDATE()),
    (4, 1007, 'Fernando', 'Cruz', 77777777, 'Calle G', 7, '01123456706', 'fernando.cruz@email.com', 'fcruz', 'contrasenia123', NULL, GETDATE()),
    (4, 1008, 'Isabella', 'Ríos', 88888888, 'Calle H', 8, '01123456707', 'isabella.rios@email.com', 'irio', 'contrasenia123', NULL, GETDATE()),
    (4, 1009, 'Emilio', 'Hernández', 99999999, 'Calle I', 9, '01123456708', 'emilio.hernandez@email.com', 'ehernandez', 'contrasenia123', NULL, GETDATE()),
    (4, 1010, 'Ana', 'Mendoza', 10101010, 'Calle J', 10, '01123456709', 'ana.mendoza@email.com', 'amendoza', 'contrasenia123', NULL, GETDATE()),
    (4, 1011, 'Javier', 'Salazar', 11111112, 'Calle K', 11, '01123456710', 'javier.salazar@email.com', 'jsalazar', 'contrasenia123', NULL, GETDATE()),
    (4, 1012, 'Carla', 'Paredes', 22222223, 'Calle L', 12, '01123456711', 'carla.paredes@email.com', 'cparedes', 'contrasenia123', NULL, GETDATE()),
    (4, 1013, 'Rafael', 'Gutiérrez', 33333334, 'Calle M', 13, '01123456712', 'rafael.gutierrez@email.com', 'rgutierrez', 'contrasenia123', NULL, GETDATE()),
    (4, 1014, 'Lorena', 'Reyes', 44444445, 'Calle N', 14, '01123456713', 'lorena.reyes@email.com', 'lreyes', 'contrasenia123', NULL, GETDATE()),
    (4, 1015, 'Matías', 'Córdoba', 55555556, 'Calle O', 15, '01123456714', 'matias.cordoba@email.com', 'mcordoba', 'contrasenia123', NULL, GETDATE());
	

	--Alumnos de Publicidad
INSERT INTO Alumnos (ID_Perfil, Matricula, Nombre_Alumno, Apellido_Alumno, DNI_Alumno, Domicilio_Calle, Domicilio_Numero, Telefono, Email, Usuario_Alumno, Contrasenia_Alumno)
VALUES 
    (3, 20006, 'Lucía', 'Martínez', 43123456, 'Calle Falsa', 456, '11-2345-6789', 'lucia.martinez@email.com', 'luciam', 'contraseña123'),
    (3, 20007, 'Diego', 'López', 43123457, 'Calle Verdadera', 789, '11-2345-6780', 'diego.lopez@email.com', 'diegol', 'contraseña123'),
    (3, 20008, 'Sofía', 'González', 43123458, 'Calle del Sol', 321, '11-2345-6781', 'sofia.gonzalez@email.com', 'sofiag', 'contraseña123'),
    (3, 20009, 'Joaquín', 'Hernández', 43123459, 'Calle de la Luna', 654, '11-2345-6782', 'joaquin.hernandez@email.com', 'joaquin', 'contraseña123'),
    (3, 20010, 'Camila', 'Pérez', 43123460, 'Calle Nueva', 987, '11-2345-6783', 'camila.perez@email.com', 'camilap', 'contraseña123'),
    (3, 20011, 'Fernando', 'Ramírez', 43123461, 'Calle Vieja', 123, '11-2345-6784', 'fernando.ramirez@email.com', 'fernandor', 'contraseña123'),
    (3, 20012, 'María', 'Torres', 43123462, 'Calle Principal', 234, '11-2345-6785', 'maria.torres@email.com', 'mariat', 'contraseña123'),
    (3, 20013, 'Esteban', 'Gutiérrez', 43123463, 'Calle Secundaria', 345, '11-2345-6786', 'esteban.gutierrez@email.com', 'estebang', 'contraseña123'),
    (3, 20014, 'Valentina', 'Castillo', 43123464, 'Calle del Este', 456, '11-2345-6787', 'valentina.castillo@email.com', 'valentinac', 'contraseña123'),
    (3, 20015, 'Bruno', 'Vargas', 43123465, 'Calle del Oeste', 567, '11-2345-6788', 'bruno.vargas@email.com', 'brunov', 'contraseña123'),
    (3, 20016, 'Nadia', 'Ríos', 43123466, 'Calle del Norte', 678, '11-2345-6789', 'nadia.rios@email.com', 'nadiar', 'contraseña123'),
    (3, 20017, 'Felipe', 'Mendoza', 43123467, 'Calle del Sur', 789, '11-2345-6790', 'felipe.mendoza@email.com', 'felipem', 'contraseña123'),
    (3, 20018, 'Victoria', 'Salazar', 43123468, 'Calle de la Esperanza', 890, '11-2345-6791', 'victoria.salazar@email.com', 'victorias', 'contraseña123'),
    (3, 20019, 'Lucas', 'Serrano', 43123469, 'Calle del Cambio', 901, '11-2345-6792', 'lucas.serrano@email.com', 'lucass', 'contraseña123'),
    (3, 20020, 'Ana', 'Paz', 43123470, 'Calle de la Paz', 123, '11-2345-6793', 'ana.paz@email.com', 'anap', 'contraseña123');


INSERT INTO Permisos (Nombre_Permiso)
VALUES 
    ('Acceder a Reportes de Rendimiento Académico'),
    ('Acceso Total al Sistema sin Restricciones'),
    ('Asignar Perfiles de Usuario'),
    ('Consultar Calificaciones'),
    ('Consultar Historial de Exámenes'),
    ('Crear Usuarios'),
    ('Editar Alumnos'),
    ('Editar Calificaciones de Exámenes'),
    ('Editar Exámenes'),
    ('Editar Usuarios'),
    ('Eliminar Alumnos'),
    ('Eliminar Exámenes'),
    ('Eliminar Usuarios'),
    ('Generar Estadísticas'),
    ('Generar Reportes'),
    ('Generar Reportes de Rendimiento Académico de Alumnos'),
    ('Gestionar Años de Cursada'),
    ('Gestionar Carreras'),
    ('Gestionar Materias'),
    ('Modificar Perfiles de Usuario'),
    ('Registrar Alumnos'),
    ('Registrar Calificaciones de Exámenes'),
    ('Registrar Exámenes'),
    ('Visualizar Estadísticas'),
    ('Visualizar Historial Académico de Alumnos'),
    ('Visualizar Información Académica'),
    ('Visualizar Información de Alumnos'),
    ('Visualizar Información Personal'),
    ('Visualizar Reportes');


INSERT INTO Materias (Nombre_Materia)
VALUES 
    ('Inglés I'),		--De Sistemas
    ('Ciencia, Tecnología y Sociedad'),
    ('Análisis Matemático I'),
    ('Álgebra'),
    ('Algoritmos y Estructura de Datos I'),
    ('Sistemas y Organizaciones'),
    ('Arquitectura de Computadoras'),
    ('Prácticas Profesionalizantes I'),
    ('Inglés II'),
    ('Análisis Matemático II'),
    ('Estadística'),
    ('Ingeniería de Software I'),
    ('Algoritmos y Estructura de Datos II'),
    ('Sistemas Operativos'),
    ('Base de Datos'),
    ('Prácticas Profesionalizantes II'),
    ('Inglés III'),
    ('Aspectos Legales de la Profesión'),
    ('Seminario de Actualización'),
    ('Redes y Comunicaciones'),
    ('Ingeniería de Software II'),
    ('Algoritmos y Estructura de Datos III'),
    ('Prácticas Profesionalizantes III (Auditoría)'),
    ('Prácticas Profesionalizantes III (Economía Empresarial)'),
    ('Prácticas Profesionalizantes III (Php)'),
    ('Marketing General'),		--De Publicidad
    ('Psicología General'),
    ('Fundamentos del Diseño Publicitario'),
    ('Computación I'),
    ('Introducción a la Publicidad'),
    ('Producción Gráfica'),
    ('Producción Radial'),
    ('Producción Audiovisual'),
    ('Gráfica Asistida I'),
    ('Computación II'),
    ('Inglés I'),
    ('Psicología Social'),
    ('Marketing Directo'),
    ('Arte, Cine, Literatura e Historia de la Publicidad'),
    ('Redacción Creativa I'),
    ('Dirección de Arte I'),
    ('Planificación Estratégica de Medios'),
    ('Semiología Publicitaria'),
    ('Técnica Promocional y Pop'),
    ('Gráfica Asistida II'),
    ('Inglés II'),
    ('Investigación de Mercado'),
    ('Redacción Creativa II'),
    ('Dirección de Arte II'),
    ('Seminario de Práctica Profesional'),
    ('Atención de Cuentas'),
    ('Organización y Administración de la Agencia'),
    ('Derecho y Legislación Publicitaria'),
    ('Gráfica Asistida III');
	--54 Materias en total


INSERT INTO AñosDeCarrera (ID_Carrera, Año)
VALUES 
    (1, 1), -- Analista de Sistemas, Año 1
    (1, 2), -- Analista de Sistemas, Año 2
    (1, 3), -- Analista de Sistemas, Año 3
    (2, 1), -- Técnico Superior en Publicidad, Año 1
    (2, 2), -- Técnico Superior en Publicidad, Año 2
    (2, 3); -- Técnico Superior en Publicidad, Año 3
	

	--Examenes de Sistemas
INSERT INTO Examenes (ID_Materia, ID_Instancia, ID_Alumno, Nota, Fecha, Libro, Folio)
VALUES 
    (1, 1, 1, 8.5, '2024-05-15', 1, 1001),  -- Inglés I, 1er Parcial, Alumno 1
    (1, 2, 1, 7.0, '2024-06-16', 1, 2001),  -- Inglés I, Recup. 1er Parcial, Alumno 1
    (2, 1, 2, 7.5, '2024-05-16', 1, 1002),  -- Ciencia, Tecnología y Sociedad, 1er Parcial, Alumno 2
    (2, 2, 2, 6.5, '2024-06-17', 1, 2002),  -- Ciencia, Tecnología y Sociedad, Recup. 1er Parcial, Alumno 2
    (3, 2, 3, 9.0, '2024-06-20', 1, 1003),  -- Análisis Matemático I, Recup. 1er Parcial, Alumno 3
    (3, 3, 3, 8.5, '2024-07-10', 1, 2003),  -- Análisis Matemático I, 2do Parcial, Alumno 3
    (4, 1, 4, 8.0, '2024-05-15', 1, 1004),  -- Álgebra, 1er Parcial, Alumno 4
    (4, 2, 4, 9.5, '2024-06-10', 1, 2004),  -- Álgebra, Recup. 1er Parcial, Alumno 4
    (5, 2, 5, 6.5, '2024-06-20', 1, 1005),  -- Algoritmos y Estructura de Datos I, Recup. 1er Parcial, Alumno 5
    (5, 3, 5, 8.0, '2024-07-15', 1, 2005),  -- Algoritmos y Estructura de Datos I, 2do Parcial, Alumno 5
    (6, 3, 6, 7.5, '2024-07-11', 1, 1006),  -- Sistemas y Organizaciones, 2do Parcial, Alumno 6
    (6, 4, 6, 9.0, '2024-08-20', 1, 2006),  -- Sistemas y Organizaciones, Final, Alumno 6
    (7, 1, 7, 10.0, '2024-05-18', 1, 1007), -- Arquitectura de Computadoras, 1er Parcial, Alumno 7
    (7, 2, 7, 8.0, '2024-06-15', 1, 2007),  -- Arquitectura de Computadoras, Recup. 1er Parcial, Alumno 7
    (8, 1, 8, 9.0, '2024-05-19', 1, 1008),  -- Prácticas Profesionalizantes I, 1er Parcial, Alumno 8
    (8, 2, 8, 8.5, '2024-06-22', 1, 2008),  -- Prácticas Profesionalizantes I, Recup. 1er Parcial, Alumno 8
    (9, 3, 9, 7.0, '2024-07-12', 1, 1009),  -- Inglés II, 2do Parcial, Alumno 9
    (9, 4, 9, 8.0, '2024-08-15', 1, 2009),  -- Inglés II, Final, Alumno 9
    (10, 4, 10, 9.5, '2024-08-25', 1, 1010), -- Análisis Matemático II, Final, Alumno 10
    (10, 1, 11, 8.5, '2024-05-20', 1, 1011), -- Análisis Matemático II, 1er Parcial, Alumno 11
    (10, 2, 12, 7.0, '2024-06-25', 1, 1012), -- Análisis Matemático II, Recup. 1er Parcial, Alumno 12
    (9, 2, 13, 9.0, '2024-06-10', 1, 1013),  -- Inglés II, Recup. 1er Parcial, Alumno 13
    (8, 3, 14, 8.5, '2024-07-20', 1, 1014),  -- Prácticas Profesionalizantes I, 2do Parcial, Alumno 14
    (7, 4, 15, 7.0, '2024-08-30', 1, 1015);  -- Arquitectura de Computadoras, Final, Alumno 15
	
	
	--Examenes de Publicidad
INSERT INTO Examenes (ID_Materia, ID_Instancia, ID_Alumno, Nota, Fecha, Libro, Folio)
VALUES 
    -- Marketing General
    (26, 1, 16, 8.0, '2024-06-10', 3, 3001),
    (26, 1, 17, 7.5, '2024-06-10', 3, 3002),
    (26, 1, 18, 9.0, '2024-06-10', 3, 3003),
    (26, 1, 19, 6.5, '2024-06-10', 3, 3004),
    (26, 1, 20, 8.2, '2024-06-10', 3, 3005),
    -- Psicología General
    (27, 1, 16, 7.0, '2024-06-11', 3, 3006),
    (27, 1, 17, 8.5, '2024-06-11', 3, 3007),
    (27, 1, 18, 9.3, '2024-06-11', 3, 3008),
    (27, 1, 19, 6.8, '2024-06-11', 3, 3009),
    (27, 1, 20, 7.5, '2024-06-11', 3, 3010),
    -- Fundamentos del Diseño Publicitario
    (28, 1, 16, 8.7, '2024-06-12', 3, 3011),
    (28, 1, 17, 9.0, '2024-06-12', 3, 3012),
    (28, 1, 18, 7.2, '2024-06-12', 3, 3013),
    (28, 1, 19, 8.1, '2024-06-12', 3, 3014),
    (28, 1, 20, 6.9, '2024-06-12', 3, 3015),
    -- Computación I
    (29, 1, 16, 8.3, '2024-06-13', 3, 3016),
    (29, 1, 17, 9.5, '2024-06-13', 3, 3017),
    (29, 1, 18, 7.8, '2024-06-13', 3, 3018),
    (29, 1, 19, 8.9, '2024-06-13', 3, 3019),
    (29, 1, 20, 7.1, '2024-06-13', 3, 3020),
	
    -- Producción Gráfica
    (31, 1, 21, 8.4, '2024-06-14', 3, 3021),
    (31, 1, 22, 7.6, '2024-06-14', 3, 3022),
    (31, 1, 23, 9.2, '2024-06-14', 3, 3023),
    -- Producción Radial
    (32, 1, 24, 8.1, '2024-06-15', 3, 3024),
    (32, 1, 25, 7.3, '2024-06-15', 3, 3025),
    (32, 1, 26, 9.0, '2024-06-15', 3, 3026),
    -- Dirección de Arte I
    (41, 1, 27, 7.9, '2024-06-16', 3, 3027),
    (41, 1, 28, 8.5, '2024-06-16', 3, 3028),
    (41, 1, 29, 9.1, '2024-06-16', 3, 3029),
    (41, 1, 30, 6.7, '2024-06-16', 3, 3030);

	
	--De Sistemas
INSERT INTO MateriasXAlumnos (ID_Materia, ID_Alumno)
VALUES
    (1, 1),   -- Inglés I, Alumno 1
    (2, 2),   -- Ciencia, Tecnología y Sociedad, Alumno 2
    (3, 3),   -- Análisis Matemático I, Alumno 3
    (4, 4),   -- Álgebra, Alumno 4
    (5, 5),   -- Algoritmos y Estructura de Datos I, Alumno 5
    (6, 6),   -- Sistemas y Organizaciones, Alumno 6
    (7, 7),   -- Arquitectura de Computadoras, Alumno 7
    (8, 8),   -- Prácticas Profesionalizantes I, Alumno 8
    (9, 9),   -- Inglés II, Alumno 9
    (10, 10), -- Análisis Matemático II, Alumno 10
    (10, 11), -- Análisis Matemático II, Alumno 11
    (10, 12), -- Análisis Matemático II, Alumno 12
    (9, 13),  -- Inglés II, Alumno 13
    (8, 14),  -- Prácticas Profesionalizantes I, Alumno 14
    (7, 15);  -- Arquitectura de Computadoras, Alumno 15


	--De Publicidad
INSERT INTO MateriasXAlumnos (ID_Materia, ID_Alumno)
VALUES  
    -- Marketing General (ID_Materia: 26)
    (26, 16), -- Alumno 16
    (26, 17), -- Alumno 17
    (26, 18), -- Alumno 18
    (26, 19), -- Alumno 19
    (26, 20), -- Alumno 20
    -- Psicología General (ID_Materia: 27)
    (27, 16), -- Alumno 16
    (27, 17), -- Alumno 17
    (27, 18), -- Alumno 18
    (27, 19), -- Alumno 19
    (27, 20), -- Alumno 20
    -- Fundamentos del Diseño Publicitario (ID_Materia: 28)
    (28, 16), -- Alumno 16
    (28, 17), -- Alumno 17
    (28, 18), -- Alumno 18
    (28, 19), -- Alumno 19
    (28, 20), -- Alumno 20
    -- Computación I (ID_Materia: 29)
    (29, 16), -- Alumno 16
    (29, 17), -- Alumno 17
    (29, 18), -- Alumno 18
    (29, 19), -- Alumno 19
    (29, 20), -- Alumno 20

    -- Producción Gráfica
    (31, 21),
    (31, 22),
    (31, 23),
    -- Producción Radial
    (32, 24),
    (32, 25),
    (32, 26),
    -- Dirección de Arte I
    (41, 27),
    (41, 28),
    (41, 29),
    (41, 30);
	
	
INSERT INTO ProfesoresXMaterias (ID_Empleado, ID_Materia)
VALUES
    -- Asignaciones para profesor 1
    (4, 1),   -- Inglés I
    (4, 2),   -- Ciencia, Tecnología y Sociedad
    (4, 10),  -- Análisis Matemático II
    
    -- Asignaciones para profesor 2
    (5, 3),   -- Análisis Matemático I
    (5, 6),   -- Sistemas y Organizaciones
    (5, 14),  -- Sistemas Operativos
    
    -- Asignaciones para profesor 3
    (6, 5),   -- Algoritmos y Estructura de Datos I
    (6, 15),  -- Base de Datos
    (6, 21),  -- Ingeniería de Software II
    
    -- Asignaciones para profesor 4
    (7, 9),   -- Inglés II
    (7, 26),  -- Marketing General
    (7, 27),  -- Psicología General
    
    -- Asignaciones para profesor 5
    (8, 28),  -- Fundamentos del Diseño Publicitario
    (8, 30),  -- Introducción a la Publicidad
    (8, 40),  -- Redacción Creativa I
    
    -- Asignaciones para profesor 6
    (9, 41),  -- Dirección de Arte I
    (9, 45),  -- Gráfica Asistida II
    (9, 53),  -- Derecho y Legislación Publicitaria
    
    -- Asignaciones para profesor 7
    (10, 7),   -- Arquitectura de Computadoras
    (10, 25),  -- Prácticas Profesionalizantes III (Php)
    (10, 16),  -- Prácticas Profesionalizantes II
    
    -- Asignaciones para profesor 8
    (11, 31),  -- Producción Gráfica
    (11, 33),  -- Producción Audiovisual
    (11, 43),  -- Semiología Publicitaria
    
    -- Asignaciones para profesor 9
    (12, 12),  -- Ingeniería de Software I
    (12, 20),  -- Redes y Comunicaciones
    (12, 23),  -- Prácticas Profesionalizantes III (Auditoría)
    
    -- Asignaciones para profesor 10
    (13, 4),   -- Álgebra
    (13, 17),  -- Inglés III
    (13, 47),  -- Investigación de Mercado
    
    -- Asignaciones para profesor 11
    (14, 24),  -- Prácticas Profesionalizantes III (Economía Empresarial)
    (14, 32),  -- Producción Radial
    (14, 44),  -- Técnica Promocional y Pop
    
    -- Asignaciones para profesor 12
    (15, 8),   -- Prácticas Profesionalizantes I
    (15, 29),  -- Computación I

    (16, 39),  -- Arte, Cine, Literatura e Historia de la Publicidad
    (16, 48);  -- Redacción Creativa II
	--Quedaria un profesor sin dar materias

	
-- Asociaciones de permisos para el perfil Administrador
INSERT INTO PermisosXPerfiles (ID_Permiso, ID_Perfil)
VALUES
	(6, 1),  -- Crear Usuarios
	(10, 1), -- Editar Usuarios
	(13, 1), -- Eliminar Usuarios
	(3, 1),  -- Asignar Perfiles de Usuario
	(20, 1), -- Modificar Perfiles de Usuario
	(18, 1), -- Gestionar Carreras
	(17, 1), -- Gestionar Años de Cursada
	(19, 1), -- Gestionar Materias
	(21, 1), -- Registrar Alumnos
	(7, 1),  -- Editar Alumnos
	(11, 1), -- Eliminar Alumnos
	(23, 1), -- Registrar Exámenes
	(9, 1),  -- Editar Exámenes
	(12, 1), -- Eliminar Exámenes
	(15, 1), -- Generar Reportes
	(29, 1), -- Visualizar Reportes
	(14, 1), -- Generar Estadísticas
	(24, 1), -- Visualizar Estadísticas
	(2, 1);  -- Acceso Total al Sistema sin Restricciones
	
-- Asociaciones de permisos para el perfil Personal Administrativo
INSERT INTO PermisosXPerfiles (ID_Permiso, ID_Perfil)
VALUES
	(21, 2), -- Registrar Alumnos
	(7, 2),  -- Editar Alumnos
	(11, 2), -- Eliminar Alumnos
	(18, 2), -- Gestionar Carreras
	(17, 2), -- Gestionar Años de Cursada
	(19, 2), -- Gestionar Materias
	(23, 2), -- Registrar Exámenes
	(9, 2),  -- Editar Exámenes
	(12, 2), -- Eliminar Exámenes
	(15, 2), -- Generar Reportes
	(29, 2), -- Visualizar Reportes
	(14, 2), -- Generar Estadísticas
	(24, 2); -- Visualizar Estadísticas
	
-- Asociaciones de permisos para el perfil Docente
INSERT INTO PermisosXPerfiles (ID_Permiso, ID_Perfil)
VALUES
	(22, 3), -- Registrar Calificaciones de Exámenes
	(8, 3),  -- Editar Calificaciones de Exámenes
	(27, 3), -- Visualizar Información de Alumnos
	(25, 3), -- Visualizar Historial Académico de Alumnos
	(16, 3); -- Generar Reportes de Rendimiento Académico de Alumnos
	
-- Asociaciones de permisos para el perfil Alumno
INSERT INTO PermisosXPerfiles (ID_Permiso, ID_Perfil)
VALUES
	(28, 4), -- Visualizar Información Personal
	(26, 4), -- Visualizar Información Académica
	(5, 4),  -- Consultar Historial de Exámenes
	(4, 4),  -- Consultar Calificaciones
	(1, 4);  -- Acceder a Reportes de Rendimiento Académico


INSERT INTO MateriasXCarreras (ID_Materia, ID_Carrera, ID_AñoDeCarrera)
VALUES 
    -- Materias de Analista de Sistemas
    (1, 1, 1), -- Inglés I, Analista de Sistemas, Año 1
    (2, 1, 1), -- Ciencia, Tecnología y Sociedad, Analista de Sistemas, Año 1
    (3, 1, 1), -- Análisis Matemático I, Analista de Sistemas, Año 1
    (4, 1, 1), -- Álgebra, Analista de Sistemas, Año 1
    (5, 1, 1), -- Algoritmos y Estructura de Datos I, Analista de Sistemas, Año 1
    (6, 1, 1), -- Sistemas y Organizaciones, Analista de Sistemas, Año 1
    (7, 1, 1), -- Arquitectura de Computadoras, Analista de Sistemas, Año 1
    (8, 1, 1), -- Prácticas Profesionalizantes I, Analista de Sistemas, Año 1
    (9, 1, 2), -- Inglés II, Analista de Sistemas, Año 2
    (10, 1, 2), -- Análisis Matemático II, Analista de Sistemas, Año 2
    (11, 1, 2), -- Estadística, Analista de Sistemas, Año 2
    (12, 1, 2), -- Ingeniería de Software I, Analista de Sistemas, Año 2
    (13, 1, 2), -- Algoritmos y Estructura de Datos II, Analista de Sistemas, Año 2
    (14, 1, 2), -- Sistemas Operativos, Analista de Sistemas, Año 2
    (15, 1, 2), -- Base de Datos, Analista de Sistemas, Año 2
    (16, 1, 2), -- Prácticas Profesionalizantes II, Analista de Sistemas, Año 2
    (17, 1, 3), -- Inglés III, Analista de Sistemas, Año 3
    (18, 1, 3), -- Aspectos Legales de la Profesión, Analista de Sistemas, Año 3
    (19, 1, 3), -- Seminario de Actualización, Analista de Sistemas, Año 3
    (20, 1, 3), -- Redes y Comunicaciones, Analista de Sistemas, Año 3
    (21, 1, 3), -- Ingeniería de Software II, Analista de Sistemas, Año 3
    (22, 1, 3), -- Algoritmos y Estructura de Datos III, Analista de Sistemas, Año 3
    (23, 1, 3), -- Prácticas Profesionalizantes III (Auditoría), Analista de Sistemas, Año 3
    (24, 1, 3), -- Prácticas Profesionalizantes III (Economía Empresarial), Analista de Sistemas, Año 3
    (25, 1, 3), -- Prácticas Profesionalizantes III (Php), Analista de Sistemas, Año 3

    -- Materias de Técnico Superior en Publicidad
    (26, 2, 1), -- Marketing General, Técnico Superior en Publicidad, Año 1
    (27, 2, 1), -- Psicología General, Técnico Superior en Publicidad, Año 1
    (28, 2, 1), -- Fundamentos del Diseño Publicitario, Técnico Superior en Publicidad, Año 1
    (29, 2, 1), -- Computación I, Técnico Superior en Publicidad, Año 1
    (30, 2, 1), -- Introducción a la Publicidad, Técnico Superior en Publicidad, Año 1
    (31, 2, 1), -- Producción Gráfica, Técnico Superior en Publicidad, Año 1
    (32, 2, 1), -- Producción Radial, Técnico Superior en Publicidad, Año 1
    (33, 2, 1), -- Producción Audiovisual, Técnico Superior en Publicidad, Año 1
    (34, 2, 1), -- Gráfica Asistida I, Técnico Superior en Publicidad, Año 1
    (35, 2, 2), -- Computación II, Técnico Superior en Publicidad, Año 2
    (36, 2, 2), -- Inglés I, Técnico Superior en Publicidad, Año 2
    (37, 2, 2), -- Psicología Social, Técnico Superior en Publicidad, Año 2
    (38, 2, 2), -- Marketing Directo, Técnico Superior en Publicidad, Año 2
    (39, 2, 2), -- Arte, Cine, Literatura e Historia de la Publicidad, Técnico Superior en Publicidad, Año 2
    (40, 2, 2), -- Redacción Creativa I, Técnico Superior en Publicidad, Año 2
    (41, 2, 2), -- Dirección de Arte I, Técnico Superior en Publicidad, Año 2
    (42, 2, 2), -- Planificación Estratégica de Medios, Técnico Superior en Publicidad, Año 2
    (43, 2, 2), -- Semiología Publicitaria, Técnico Superior en Publicidad, Año 2
    (44, 2, 2), -- Técnica Promocional y Pop, Técnico Superior en Publicidad, Año 2
    (45, 2, 2), -- Gráfica Asistida II, Técnico Superior en Publicidad, Año 2
    (46, 2, 3), -- Inglés II, Técnico Superior en Publicidad, Año 3
    (47, 2, 3), -- Investigación de Mercado, Técnico Superior en Publicidad, Año 3
    (48, 2, 3), -- Redacción Creativa II, Técnico Superior en Publicidad, Año 3
    (49, 2, 3), -- Dirección de Arte II, Técnico Superior en Publicidad, Año 3
    (50, 2, 3), -- Seminario de Práctica Profesional, Técnico Superior en Publicidad, Año 3
    (51, 2, 3), -- Atención de Cuentas, Técnico Superior en Publicidad, Año 3
    (52, 2, 3), -- Organización y Administración de la Agencia, Técnico Superior en Publicidad, Año 3
    (53, 2, 3), -- Derecho y Legislación Publicitaria, Técnico Superior en Publicidad, Año 3
    (54, 2, 3); -- Gráfica Asistida III, Técnico Superior en Publicidad, Año 3


----------------------------------------------------- CONSULTAS -----------------------------------------------------

-- 1)Selecciona el nombre y apellido de los alumnos que están cursando la materia 'Análisis Matemático II'.
SELECT a.Nombre_Alumno, a.Apellido_Alumno
FROM Alumnos AS a
INNER JOIN MateriasXAlumnos AS mxm
	ON a.ID_Alumno = mxm.ID_Alumno
INNER JOIN Materias AS m 
	ON mxm.ID_Materia = m.ID_Materia
WHERE m.Nombre_Materia = 'Análisis Matemático II';


--Seleccionar los permisos de los alumnos
SELECT perf.Nombre_Perfil, p.Nombre_Permiso 
FROM Permisos as p
INNER JOIN PermisosXPerfiles as pxp
	ON p.ID_Permiso = pxp.ID_Permiso
INNER JOIN Perfiles as perf
	ON pxp.ID_Perfil = perf.ID_Perfil
WHERE perf.Nombre_Perfil = 'Alumno';


--Historial completo de exámenes por alumno (Todos los exámenes que ha rendido un alumno)
SELECT 
    a.ID_Alumno,
    a.Nombre_Alumno,
    a.Apellido_Alumno,
    m.Nombre_Materia,
    e.Fecha,
    e.Nota
FROM 
    Examenes e
JOIN 
    Alumnos a ON e.ID_Alumno = a.ID_Alumno
JOIN 
    Materias m ON e.ID_Materia = m.ID_Materia
WHERE 
    a.ID_Alumno = 16 -- Reemplaza con el ID del alumno deseado
ORDER BY 
    e.Fecha DESC;


--Promedio general de un alumno en todas las materias
SELECT 
    a.ID_Alumno,
    a.Nombre_Alumno,
    a.Apellido_Alumno,
    AVG(e.Nota) AS Promedio_General
FROM 
    Examenes e
JOIN 
    Alumnos a ON e.ID_Alumno = a.ID_Alumno
WHERE 
    a.ID_Alumno = 16 -- Reemplaza con el ID del alumno deseado
GROUP BY 
    a.ID_Alumno, a.Nombre_Alumno, a.Apellido_Alumno;


--Promedio de notas por materia para un alumno específico
SELECT 
    a.ID_Alumno,
    a.Nombre_Alumno,
    a.Apellido_Alumno,
    m.Nombre_Materia,
    AVG(e.Nota) AS Promedio_Materia
FROM 
    Examenes e
JOIN 
    Alumnos a ON e.ID_Alumno = a.ID_Alumno
JOIN 
    Materias m ON e.ID_Materia = m.ID_Materia
WHERE 
    a.ID_Alumno = 16 -- Reemplaza con el ID del alumno deseado
GROUP BY 
    a.ID_Alumno, a.Nombre_Alumno, a.Apellido_Alumno, m.Nombre_Materia
ORDER BY 
    m.Nombre_Materia;


--Historial de exámenes y promedios por alumno (VERSION RARA)
WITH HistorialExamenes AS (
    SELECT 
        a.ID_Alumno,
        a.Nombre_Alumno,
        a.Apellido_Alumno,
        m.Nombre_Materia,
        e.Fecha,
        e.Nota
    FROM 
        Examenes e
    JOIN 
        Alumnos a ON e.ID_Alumno = a.ID_Alumno
    JOIN 
        Materias m ON e.ID_Materia = m.ID_Materia
    WHERE 
        a.ID_Alumno = 16
)
SELECT 
    Nombre_Alumno,
    Apellido_Alumno,
    Nombre_Materia,
    AVG(Nota) OVER (PARTITION BY Nombre_Materia) AS Promedio_Materia,
    Fecha,
    Nota
FROM 
    HistorialExamenes
ORDER BY 
    Nombre_Materia, Fecha;



----------------------------------------------------- STORE PROCEDURES -----------------------------------------------------


---------------------------- STORE EMPLEADOS ----------------------------
CREATE PROCEDURE sp_AgregarEmpleado
	@ID_Perfil INT,
	@Nombre_Empleado VARCHAR(30),
	@Apellido_Empleado VARCHAR(30),
	@DNI_Empleado INT,
	@Domicilio_Calle VARCHAR(40) = NULL,
	@Domicilio_Numero INT = NULL,
	@Telefono VARCHAR(20) = NULL,
	@Email VARCHAR(50),
	@Usuario_Empleado VARCHAR(50),
	@Contrasenia_Empleado VARCHAR(40),
	@Fecha_Baja DATE = NULL,
	@Fecha_Alta DATE = NULL
AS
BEGIN
	INSERT INTO Empleados (ID_Perfil, Nombre_Empleado, Apellido_Empleado, DNI_Empleado, Domicilio_Calle, Domicilio_Numero, Telefono, Email, Usuario_Empleado, Contrasenia_Empleado, Fecha_Baja, Fecha_Alta)
	VALUES (@ID_Perfil, @Nombre_Empleado, @Apellido_Empleado, @DNI_Empleado, @Domicilio_Calle, @Domicilio_Numero, @Telefono, @Email, @Usuario_Empleado, @Contrasenia_Empleado, @Fecha_Baja, @Fecha_Alta)
END;

--------
CREATE PROCEDURE sp_EliminarEmpleado
    @ID_Empleado INT
AS
BEGIN
    DELETE FROM Empleados
    WHERE ID_Empleado = @ID_Empleado;
END;

--------
CREATE PROCEDURE sp_EditarEmpleado
	@ID_Empleado INT,
	@ID_Perfil INT = NULL,
    @Nombre_Empleado VARCHAR(30),
    @Apellido_Empleado VARCHAR(30),
    @DNI_Empleado INT,
    @Domicilio_Calle VARCHAR(40) = NULL,
    @Domicilio_Numero INT = NULL,
    @Telefono VARCHAR(20) = NULL,
    @Email VARCHAR(50),
    @Usuario_Empleado VARCHAR(50),
    @Contrasenia_Empleado VARCHAR(40),
    @Fecha_Baja DATE = NULL,
	@Fecha_Alta DATE = NULL
AS
BEGIN
	UPDATE Empleados
	SET
		ID_Perfil = COALESCE(@ID_Perfil, ID_Perfil),  
		Nombre_Empleado = @Nombre_Empleado, 
		Apellido_Empleado = @Apellido_Empleado, 
		DNI_Empleado = @DNI_Empleado, 
		Domicilio_Calle = COALESCE(@Domicilio_Calle, Domicilio_Calle),	-- Mantiene el valor existente si es NULL
		Domicilio_Numero = COALESCE(@Domicilio_Numero, Domicilio_Numero),
		Telefono = COALESCE(@Telefono, Telefono),
		Email = @Email, 
		Usuario_Empleado = @Usuario_Empleado, 
		Contrasenia_Empleado = @Contrasenia_Empleado, 
		Fecha_Baja = @Fecha_Baja, 
		Fecha_Alta = COALESCE(@Fecha_Alta, Fecha_Alta)
	WHERE ID_Empleado = @ID_Empleado;
END;

--------
CREATE PROCEDURE sp_SeleccionarEmpleadoAvanzado		--Busca por Nombre, Apellido o DNI (cualquiera que se le pase)
    @Nombre_Empleado VARCHAR(30) = NULL,
    @Apellido_Empleado VARCHAR(30) = NULL,
    @DNI_Empleado INT = NULL
AS
BEGIN
    SELECT *
    FROM Empleados
    WHERE
        (@Nombre_Empleado IS NULL OR Nombre_Empleado LIKE '%' + @Nombre_Empleado + '%') AND
        (@Apellido_Empleado IS NULL OR Apellido_Empleado LIKE '%' + @Apellido_Empleado + '%') AND
        (@DNI_Empleado IS NULL OR DNI_Empleado = @DNI_Empleado);
END;


---------------------------- STORE ALUMNOS ----------------------------
CREATE PROCEDURE sp_AgregarAlumno
	@ID_Perfil INT,
    @Matricula INT,
    @Nombre_Alumno VARCHAR(30),
    @Apellido_Alumno VARCHAR(30),
    @DNI_Alumno INT,
    @Domicilio_Calle VARCHAR(40) = NULL,
    @Domicilio_Numero INT = NULL,
    @Telefono VARCHAR(20) = NULL,
    @Email VARCHAR(50),
    @Usuario_Alumno VARCHAR(50),
    @Contrasenia_Alumno VARCHAR(40),
    @Fecha_Baja DATE = NULL,
	@Fecha_Alta DATE = NULL		--si no se le asigna nada, sql le pasa la fecha actual
AS
BEGIN
    INSERT INTO Alumnos (ID_Perfil, Matricula, Nombre_Alumno, Apellido_Alumno, DNI_Alumno, Domicilio_Calle, Domicilio_Numero, Telefono, Email, Usuario_Alumno, Contrasenia_Alumno, Fecha_Baja, Fecha_Alta)
    VALUES (@ID_Perfil, @Matricula, @Nombre_Alumno, @Apellido_Alumno, @DNI_Alumno, @Domicilio_Calle, @Domicilio_Numero, @Telefono, @Email, @Usuario_Alumno, @Contrasenia_Alumno, @Fecha_Baja, 
			COALESCE(@Fecha_Alta, GETDATE()) )--Hace que SQL tome el valor de @Fecha_Alta si se especifica; sinó, usa la fecha actual GETDATE()
END;

--------
CREATE PROCEDURE sp_EliminarAlumno
	@ID_Alumno INT
AS
BEGIN
    DELETE FROM Alumnos
    WHERE ID_Alumno = @ID_Alumno;
END;

--------
CREATE PROCEDURE sp_EditarAlumno
	@ID_Alumno INT,
	@ID_Perfil INT = NULL,
    @Matricula INT,
    @Nombre_Alumno VARCHAR(30),
    @Apellido_Alumno VARCHAR(30),
    @DNI_Alumno INT,
    @Domicilio_Calle VARCHAR(40) = NULL,
    @Domicilio_Numero INT = NULL,
    @Telefono VARCHAR(20) = NULL,
    @Email VARCHAR(50),
    @Usuario_Alumno VARCHAR(50),
    @Contrasenia_Alumno VARCHAR(40),
    @Fecha_Baja DATE = NULL,
	@Fecha_Alta DATE = NULL
AS
BEGIN
	UPDATE Alumnos
	SET
		ID_Perfil = COALESCE(@ID_Perfil, ID_Perfil), 
		Matricula = @Matricula, 
		Nombre_Alumno = @Nombre_Alumno, 
		Apellido_Alumno = @Apellido_Alumno, 
		DNI_Alumno = @DNI_Alumno, 
		Domicilio_Calle = COALESCE(@Domicilio_Calle, Domicilio_Calle),	-- Mantiene el valor existente si es NULL
		Domicilio_Numero = COALESCE(@Domicilio_Numero, Domicilio_Numero),
		Telefono = COALESCE(@Telefono, Telefono),
		Email = @Email, 
		Usuario_Alumno = @Usuario_Alumno, 
		Contrasenia_Alumno = @Contrasenia_Alumno, 
		Fecha_Baja = @Fecha_Baja, 
		Fecha_Alta = COALESCE(@Fecha_Alta, Fecha_Alta)
	WHERE ID_Alumno = @ID_Alumno;
END;

--------
CREATE PROCEDURE sp_SeleccionarAlumnoAvanzado		--Busca por Matricula, Nombre, Apellido o DNI (cualquiera que se le pase)
    @Matricula INT = NULL,
    @Nombre_Alumno VARCHAR(30) = NULL,
    @Apellido_Alumno VARCHAR(30) = NULL,
    @DNI_Alumno INT = NULL
AS
BEGIN
    SELECT *
    FROM Alumnos
    WHERE
        (@Matricula IS NULL OR Matricula = @Matricula) AND
        (@Nombre_Alumno IS NULL OR Nombre_Alumno LIKE '%' + @Nombre_Alumno + '%') AND
        (@Apellido_Alumno IS NULL OR Apellido_Alumno LIKE '%' + @Apellido_Alumno + '%') AND
        (@DNI_Alumno IS NULL OR DNI_Alumno = @DNI_Alumno);
END;

--Asignar un alumno a una materia
--Obtener las notas de un alumno en una materia
--Obtener todos los alumnos en una materia
--Contar cuántos alumnos están en cada materia
--Cambiar el estado de un alumno (por ejemplo, dar de baja)
--Listar materias sin alumnos inscritos

--Ejemplo de cómo podría ser un procedimiento que permite buscar por apellido, matrícula o DNI
--(dependiendo de cuáles parámetros se le pasen)    HAY QUE ADAPTARLO A ESTE SISTEMA


---------------------------- STORE MATERIAS ----------------------------
CREATE PROCEDURE sp_AgregarMateria
	@Nombre_Materia VARCHAR(60)
AS
BEGIN
	INSERT INTO Materias (Nombre_Materia)
	VALUES (@Nombre_Materia)
END;

--------
CREATE PROCEDURE sp_EliminarMateria
	@ID_Materia INT
AS
BEGIN
	DELETE FROM Materias
	WHERE ID_Materia = @ID_Materia;
END;

--------
CREATE PROCEDURE sp_EditarMateria
	@ID_Materia INT,
	@Nombre_Materia VARCHAR(60)
AS
BEGIN
	UPDATE Materias
	SET
		Nombre_Materia = @Nombre_Materia
	WHERE ID_Materia = @ID_Materia;
END;

--------
CREATE PROCEDURE sp_SeleccionarMateriasAvanzado		--Selecciona la materias depende el dato pasado (si no se le pasa nada, muestra todas)
    @ID_Materia INT = NULL,
    @Nombre_Materia VARCHAR(60) = NULL
AS
BEGIN
    SELECT ID_Materia, Nombre_Materia
    FROM Materias
    WHERE (@ID_Materia IS NULL OR ID_Materia = @ID_Materia)
      AND (@Nombre_Materia IS NULL OR Nombre_Materia LIKE '%' + @Nombre_Materia + '%');
END;


---------------------------- STORE CARRERAS ----------------------------
CREATE PROCEDURE sp_AgregarCarrera
	@Nombre_Carrera VARCHAR(50) = NULL,
	@Resolucion VARCHAR(20) = NULL,
	@Programa VARCHAR(50) = NULL
AS
BEGIN
	INSERT INTO Carreras (Nombre_Carrera, Resolucion, Programa)
	VALUES (@Nombre_Carrera, @Resolucion, @Programa)
END;

--------
CREATE PROCEDURE sp_EliminarCarrera
	@ID_Carrera INT
AS
BEGIN
	DELETE FROM Carreras
	WHERE ID_Carrera = @ID_Carrera;
END;

--------
CREATE PROCEDURE sp_EditarCarrera
	@ID_Carrera INT,
	@Nombre_Carrera VARCHAR(50),
	@Resolucion VARCHAR(20),
	@Programa VARCHAR(50)
AS
BEGIN
	UPDATE Carreras
	SET
		Nombre_Carrera = @Nombre_Carrera,
		Resolucion = @Resolucion,
		Programa = @Programa
	WHERE ID_Carrera = @ID_Carrera;
END;

--------
CREATE PROCEDURE sp_SeleccionarCarrerasAvanzado			--Selecciona la materias depende el dato pasado (si no se le pasa nada, 
    @ID_Carrera INT = NULL,								--omite el WHERE y muestra todas)
    @Nombre_Carrera VARCHAR(50) = NULL,
    @Resolucion VARCHAR(20) = NULL,
    @Programa VARCHAR(50) = NULL
AS
BEGIN
    SELECT ID_Carrera, Nombre_Carrera, Resolucion, Programa
    FROM Carreras
    WHERE (@ID_Carrera IS NULL OR ID_Carrera = @ID_Carrera)
      AND (@Nombre_Carrera IS NULL OR Nombre_Carrera LIKE '%' + @Nombre_Carrera + '%')
      AND (@Resolucion IS NULL OR Resolucion = @Resolucion)
      AND (@Programa IS NULL OR Programa LIKE '%' + @Programa + '%');
END;


---------------------------- STORE INSTANCIAS ----------------------------
CREATE PROCEDURE sp_AgregarInstancia
	@Nombre_Instancia VARCHAR(30)
AS
BEGIN
	INSERT INTO Instancias (Nombre_Instancia)
	VALUES (@Nombre_Instancia)
END;

--------
CREATE PROCEDURE sp_EliminarInstancia
	@ID_Instancia INT
AS
BEGIN
	DELETE FROM Instancias
	WHERE ID_Instancia = @ID_Instancia;
END;

--------
CREATE PROCEDURE sp_EditarInstancia
	@ID_Instancia INT,
	@Nombre_Instancia VARCHAR(30)
AS
BEGIN
	UPDATE Instancias
	SET
		Nombre_Instancia = @Nombre_Instancia
	WHERE ID_Instancia = @ID_Instancia;
END;

--------
CREATE PROCEDURE sp_SeleccionarInstanciasAvanzado		--Selecciona la Instancia depende el dato pasado (si no se le pasa nada, 
    @ID_Instancia INT = NULL,							--omite el WHERE y muestra todas)
    @Nombre_Instancia VARCHAR(30) = NULL
AS
BEGIN
    SELECT ID_Instancia, Nombre_Instancia
    FROM Instancias
    WHERE (@ID_Instancia IS NULL OR ID_Instancia = @ID_Instancia) AND 
		(@Nombre_Instancia IS NULL OR Nombre_Instancia LIKE '%' + @Nombre_Instancia + '%');
END;


---------------------------- STORE EXAMENES ----------------------------
CREATE PROCEDURE sp_AgregarExamen
	@ID_Materia INT, 
	@ID_Instancia INT, 
	@ID_Alumno INT, 
	@Nota FLOAT,
	@Fecha DATE = NULL, 
	@Libro INT, 
	@Folio INT 
AS
BEGIN
	INSERT INTO Examenes (ID_Materia, ID_Instancia, ID_Alumno, Nota, Fecha, Libro, Folio)
	VALUES (@ID_Materia, @ID_Instancia, @ID_Alumno, @Nota, 
			COALESCE(@Fecha, GETDATE()), -- Si no se especifica, se usa la fecha actual
			@Libro, @Folio)
END;

--------
CREATE PROCEDURE sp_EliminarExamen
	@ID_Examen INT
AS
BEGIN
	DELETE FROM Examenes
	WHERE ID_Examen = @ID_Examen;
END;

--------
CREATE PROCEDURE sp_EditarExamen
	@ID_Examen INT,
	@ID_Materia INT,
	@ID_Instancia INT,
	@ID_Alumno INT,
	@Nota FLOAT,
	@Fecha DATE = NULL,
	@Libro INT,
	@Folio INT
AS
BEGIN
	UPDATE Examenes
	SET
		ID_Materia = @ID_Materia,
		ID_Instancia = @ID_Instancia,
		ID_Alumno = @ID_Alumno,
		Nota = @Nota,
		Fecha = COALESCE(@Fecha, Fecha),
		Libro = @Libro,
		Folio = @Folio
	WHERE ID_Examen = @ID_Examen;
END;

--------
CREATE PROCEDURE sp_SeleccionarExamenesAvanzado		--Muestra examenes depenede el dato pasado 
    @ID_Examen INT = NULL,
    @ID_Alumno INT = NULL,
    @ID_Materia INT = NULL,
    @NotaMin FLOAT = NULL,
    @NotaMax FLOAT = NULL,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL
AS
BEGIN
    SELECT *
    FROM Examenes
    WHERE (@ID_Examen IS NULL OR ID_Examen = @ID_Examen)
      AND (@ID_Alumno IS NULL OR ID_Alumno = @ID_Alumno)
      AND (@ID_Materia IS NULL OR ID_Materia = @ID_Materia)
      AND (@NotaMin IS NULL OR Nota >= @NotaMin)	--Nota Minima y Maxima para buscar Examenes (si no se aclara, muestra todo)
      AND (@NotaMax IS NULL OR Nota <= @NotaMax)
      AND (@FechaDesde IS NULL OR Fecha >= @FechaDesde)		--Fecha Minima y Maxima para buscar Examenes (si no se aclara, muestra todo)
      AND (@FechaHasta IS NULL OR Fecha <= @FechaHasta);
END;


---------------------------- STORE MATERIAXCARRERA ----------------------------
CREATE PROCEDURE sp_AgregarMateriaACarrera
	@ID_Materia INT,
    @ID_Carrera INT,
    @ID_AñoDeCarrera INT
AS
BEGIN 
	INSERT INTO MateriasXCarreras (ID_Materia, ID_Carrera, ID_AñoDeCarrera)
    VALUES (@ID_Materia, @ID_Carrera, @ID_AñoDeCarrera)
END;

--------
CREATE PROCEDURE sp_EliminarMateriaDeCarrera
	@ID_Materia INT,
    @ID_Carrera INT,
    @ID_AñoDeCarrera INT
AS
BEGIN
	DELETE FROM MateriasXCarreras
	WHERE ID_Materia = @ID_Materia AND ID_Carrera = @ID_Carrera AND ID_AñoDeCarrera = @ID_AñoDeCarrera
END;

--------
CREATE PROCEDURE sp_SeleccionarMateriasDeCarreraPorAño		--Consultar Materias de una Carrera en un Año
	@ID_Carrera INT,
	@ID_AñoDeCarrera INT
AS
BEGIN
	SELECT m.ID_Materia, m.Nombre_Materia
	FROM Materias m
	INNER JOIN MateriasXCarreras mxc
		ON m.ID_Materia = mxc.ID_Materia
	WHERE mxc.ID_Carrera = @ID_Carrera AND mxc.ID_AñoDeCarrera = @ID_AñoDeCarrera;
END;


---------------------------- STORE MATERIAXALUMNO ----------------------------
CREATE PROCEDURE sp_AgregarAlumnoEnMateria
	@ID_Alumno INT,
	@ID_Materia INT
AS
BEGIN
	INSERT INTO MateriasXAlumnos (ID_Alumno, ID_Materia)
	VALUES (@ID_Alumno, @ID_Materia);
END;

--------
CREATE PROCEDURE sp_EliminarAlumnoDeMateria
	@ID_Alumno INT,
	@ID_Materia INT
AS
BEGIN
	DELETE FROM MateriasXAlumnos
	WHERE ID_Alumno = @ID_Alumno AND ID_Materia = @ID_Materia;
END;

--------
CREATE PROCEDURE sp_SeleccionarMateriasDeAlumno		--Consultar Materias Inscritas de un Alumno
	@ID_Alumno INT
AS
BEGIN
	SELECT m.ID_Materia, m.Nombre_Materia
    FROM Materias m
    INNER JOIN MateriasXAlumnos mxa 
		ON m.ID_Materia = mxa.ID_Materia
    WHERE mxa.ID_Alumno = @ID_Alumno;
END;


---------------------------- STORE PROFESORXMATERIA ----------------------------
CREATE PROCEDURE sp_AgregarProfesorAMateria
	@ID_Empleado INT,
    @ID_Materia INT
AS
BEGIN
	INSERT INTO ProfesoresXMaterias (ID_Empleado, ID_Materia)
	VALUES (@ID_Empleado, @ID_Materia)
END;

--------
CREATE PROCEDURE sp_EliminarProfesorDeMateria
	@ID_Empleado INT,
	@ID_Materia INT
AS
BEGIN
	DELETE FROM ProfesoresXMaterias
	WHERE ID_Empleado = @ID_Empleado AND ID_Materia = @ID_Materia;
END;

--------
CREATE PROCEDURE sp_SeleccionarMateriasDeProfesor		--Consultar Materias de un Profesor
	@ID_Empleado INT
AS
BEGIN
	SELECT m.ID_Materia, m.Nombre_Materia
    FROM Materias m
    INNER JOIN ProfesoresXMaterias pxm 
		ON m.ID_Materia = pxm.ID_Materia
    WHERE pxm.ID_Empleado = @ID_Empleado;
END;


---------------------------- STORE PERMISOS ----------------------------
CREATE PROCEDURE sp_AgregarPermiso
	@Nombre_Permiso VARCHAR(80)
AS
BEGIN
	INSERT INTO Permisos (Nombre_Permiso)
	VALUES (@Nombre_Permiso)
END;

--------
CREATE PROCEDURE sp_EliminarPermiso
	@ID_Permiso INT
AS
BEGIN
	DELETE FROM Permisos
	WHERE ID_Permiso= @ID_Permiso;
END;

--------
CREATE PROCEDURE sp_EditarPermiso
	@ID_Permiso INT,
	@Nombre_Permiso VARCHAR(80)
AS
BEGIN
	UPDATE Permisos
	SET
		Nombre_Permiso = @Nombre_Permiso
	WHERE ID_Permiso = @ID_Permiso;
END;

--------
CREATE PROCEDURE sp_SeleccionarPermisosAvanzado
    @ID_Permiso INT = NULL  -- Si es NULL omite el WHERE (para obtener todos los permisos)
AS
BEGIN
    SELECT *
    FROM Permisos
    WHERE (@ID_Permiso IS NULL OR ID_Permiso = @ID_Permiso);
END;


---------------------------- STORE PERFILES ----------------------------
CREATE PROCEDURE sp_AgregarPerfil
	@Nombre_Perfil VARCHAR(40)
AS
BEGIN
	INSERT INTO Perfiles (Nombre_Perfil)
	VALUES (@Nombre_Perfil)
END;

--------
CREATE PROCEDURE sp_EliminarPerfil
	@ID_Perfil INT
AS
BEGIN
	DELETE FROM Perfiles
	WHERE ID_Perfil = @ID_Perfil;
END;

--------
CREATE PROCEDURE sp_EditarPerfil
	@ID_Perfil INT,
	@Nombre_Perfil VARCHAR(40)
AS
BEGIN
	UPDATE Perfiles
	SET
		Nombre_Perfil = @Nombre_Perfil
	WHERE ID_Perfil = @ID_Perfil;
END;

--------
CREATE PROCEDURE sp_SeleccionarPerfilesAvanzado		--Muestra Perfiles de acuerdo al dato pasado (si no se pasa nada, muestra todos)
    @ID_Perfil INT = NULL,               -- Parámetro opcional para buscar por ID
    @Nombre_Perfil VARCHAR(40) = NULL    -- Parámetro opcional para buscar por nombre
AS
BEGIN
    SELECT *
    FROM Perfiles
    WHERE (@ID_Perfil IS NOT NULL AND ID_Perfil = @ID_Perfil)
       OR (@Nombre_Perfil IS NOT NULL AND Nombre_Perfil = @Nombre_Perfil);
END;


---------------------------- STORE PERMISOSXPERFILES ----------------------------
CREATE PROCEDURE sp_AgregarPermisoAPerfil
	@ID_Perfil INT,
    @ID_Permiso INT
AS
BEGIN
	INSERT INTO PermisosXPerfiles (ID_Perfil, ID_Permiso)
    VALUES (@ID_Perfil, @ID_Permiso)
END;

--------
CREATE PROCEDURE sp_EliminarPermisoDePerfil
	@ID_Perfil INT,
    @ID_Permiso INT
AS
BEGIN
	DELETE FROM PermisosXPerfiles
	WHERE ID_Perfil = @ID_Perfil AND ID_Permiso = @ID_Permiso;
END;

--NO lleva STORE de Editar

--------
CREATE PROCEDURE sp_SeleccionarPermisosDePerfil		--Consultar Permisos de un Perfil
	@ID_Perfil INT
AS
BEGIN
	SELECT p.ID_Permiso, p.Nombre_Permiso
	FROM Permisos p
	INNER JOIN PermisosXPerfiles pxp
		ON p.ID_Permiso = pxp.ID_Permiso
	WHERE pxp.ID_Perfil = @ID_Perfil;
END;


---------------------------- STORE AÑOSDECARRERA ----------------------------
CREATE PROCEDURE sp_AgregarAñoDeCarrera
    @ID_Carrera INT,
    @Año INT
AS
BEGIN
	INSERT INTO AñosDeCarrera (ID_Carrera, Año)
    VALUES (@ID_Carrera, @Año)
END;

--------
CREATE PROCEDURE sp_EliminarAñoDeCarrera
    @ID_AñoDeCarrera INT
AS
BEGIN
    DELETE FROM AñosDeCarrera
    WHERE ID_AñoDeCarrera = @ID_AñoDeCarrera;
END;

--------
CREATE PROCEDURE sp_EditarAñoDeCarrera
    @ID_AñoDeCarrera INT,
    @ID_Carrera INT,
    @Año INT
AS
BEGIN
    UPDATE AñosDeCarrera
    SET 
        ID_Carrera = @ID_Carrera,
        Año = @Año
    WHERE ID_AñoDeCarrera = @ID_AñoDeCarrera;
END;

--------
CREATE PROCEDURE sp_SeleccionarAñosDeCarreraAvanzado	--permite consultar todos los años de carrera asociados a una carrera específica
    @ID_Carrera INT = NULL								--pero también todos los años de carrera si no se proporciona un ID_Carrera.
AS
BEGIN
    SELECT *
    FROM AñosDeCarrera
    WHERE @ID_Carrera IS NULL OR ID_Carrera = @ID_Carrera;
END;




---------------------------------------------------------------

CREATE PROCEDURE sp_BuscarUsuarioYContraseñaAlumno
	@Usuario_Alumno VARCHAR(50),
	@Contrasenia_Alumno VARCHAR(40)
	--@ID_Perfil INT OUTPUT
AS
BEGIN	
	-- Inicializamos @ID_Perfil con un valor nulo
	--SET @ID_Perfil = NULL;

	-- Obtenemos el ID_Perfil si las credenciales son correctas
	SELECT /*@ID_Perfil =*/ ID_Perfil 
	FROM Alumnos
	WHERE Usuario_Alumno = @Usuario_Alumno AND Contrasenia_Alumno = @Contrasenia_Alumno;

	-- Si no se encuentra coincidencia, @ID_Perfil permanecerá en NULL
END;

CREATE PROCEDURE sp_BuscarUsuarioYContraseñaEmpleado
	@Usuario_Empleado VARCHAR(50),
	@Contrasenia_Empleado VARCHAR(40)
	--@ID_Perfil INT OUTPUT
AS
BEGIN
	-- Inicializamos @ID_Perfil con un valor nulo
	--SET @ID_Perfil = NULL;

	-- Obtenemos el ID_Perfil si las credenciales son correctas
	SELECT /*@ID_Perfil =*/ ID_Perfil
	FROM Empleados
	WHERE Usuario_Empleado = @Usuario_Empleado AND Contrasenia_Empleado = @Contrasenia_Empleado;

	-- Si no se encuentra coincidencia, @ID_Perfil permanecerá en NULL
END;


--Ejemplo para ejecutar los store de arriba
DECLARE @ID_Perfil INT;  -- Declaramos una variable para el parámetro de salida

EXEC sp_BuscarUsuarioYContraseñaAlumno
    @Usuario_Alumno = 'luciam',        -- Aquí colocas el nombre de usuario a buscar
    @Contrasenia_Alumno = 'contraseña123', -- Aquí colocas la contraseña correspondiente
    @ID_Perfil = @ID_Perfil OUTPUT;            -- Definimos la variable como parámetro de salida

-- Verificamos el resultado
SELECT @ID_Perfil AS ID_Perfil;




--------------------------- Stores Generales ------------------------------------ FALTA

CREATE PROCEDURE spConsultarExamenesPorAlumno
    @ID_Alumno INT  -- Parámetro para identificar al alumno
AS
BEGIN
    SELECT 
        e.ID_Examen,
        e.ID_Materia,
        m.Nombre_Materia,
        e.ID_Instancia,
        i.Nombre_Instancia,
        e.Nota,
        e.Fecha,
        e.Libro,
        e.Folio
    FROM Examenes e
    INNER JOIN Materias m ON e.ID_Materia = m.ID_Materia
    INNER JOIN Instancias i ON e.ID_Instancia = i.ID_Instancia
    WHERE e.ID_Alumno = @ID_Alumno;  -- Filtra por alumno específico
END;











	--CONSULTA PARA EDITAR EXAMENES
SELECT 
    --e.ID_Examen,
    --e.ID_Alumno,
    a.Nombre_Alumno AS Nombre, 
	a.Apellido_Alumno AS Apellido,
    --e.ID_Materia,
    m.Nombre_Materia AS Materia,
    --e.ID_Instancia,
    i.Nombre_Instancia AS Instancia,
    e.Nota,
    e.Fecha,
    e.Libro,
    e.Folio
FROM Examenes e
INNER JOIN Alumnos a 
	ON e.ID_Alumno = a.ID_Alumno
INNER JOIN Materias m 
	ON e.ID_Materia = m.ID_Materia
INNER JOIN Instancias i 
	ON e.ID_Instancia = i.ID_Instancia;
