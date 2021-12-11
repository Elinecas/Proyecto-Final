create database Proyecto_Final

drop database Proyecto_Final;

use Proyecto_Final

create table Usuarios
(

	Id int identity primary key,
	Nombre varchar(50),
	Apellido varchar(50),
	Correo varchar(50),
	Nombre_Usuario varchar(25),
	Contra varchar(50),
	Rol varchar(25)

)

select  * from Usuarios

insert into Usuarios values('Enmanuel','Santos','enmasd02@gmail.com','Enmasdiaz','12345','Administrador');
insert into Usuarios values('David','Rivas','enmasd02@gmail.com','David','12345','Medico');
insert into Usuarios values('Patita',' de Rivas','patitadepato@gmail.com','pato','12345','Medico');

insert into Usuarios (Nombre, Apellido, Correo, Nombre_Usuario, Contra, Rol) values(@nombre, @apellido, @correo, @usuario, @contra, @rol)

update Usuarios set Nombre=@nombre, Apellido=@apellido, Correo=@correo, Nombre_Usuario=@usuario, Contra=@contra, Rol=@rol where Id=@id

delete Usuarios where Id=@id

select Id, Nombre, Apellido, Correo, Nombre_Usuario, Contra, Rol  from Usuarios

select Id, Nombre, Apellido, Correo, Nombre_Usuario, Contra, Rol  from Usuarios where Id=@id

select count(*) as Resultado from Usuarios where Nombre_Usuario=@usuario and Contra=@contra

select Rol from Usuarios where Nombre_Usuario=@usuario

create table Medicos
(

	Id int identity primary key,
	Cedula varchar(50),
	Nombre varchar(50),
	Apellido varchar(50),
	Correo varchar(50),
	Telefono varchar(50),
	Foto varchar(50)

)
-----------------------------------------------
-----------------------------------------------
-----------------------------------------------
-----------------------------------------------


select * from Medicos;


insert into Medicos  values('001', 'Marcos', 'Perez', 'mar@gmail.com', '809', '')

insert into Medicos  values('002', 'Robert', 'Pozo', 'mar2@gmail.com', '829', '')

insert into Medicos values('003', 'David', 'Sanchez', 'mar3@gmail.com', '849', '')

-----------------------------------------------
-----------------------------------------------
-----------------------------------------------






ALTER TABLE Citas
ADD FOREIGN KEY (Id_Medico) REFERENCES Medicos(Id);





update Medicos set Cedula=@cedula, Nombre=@nombre, Apellido=@apellido, Correo=@correo, Telefono=@telefono, Foto=@foto where Id=@id

update Medicos set Foto=@foto where Id = @id

delete Medicos where Id=@id

select Id, Cedula, Nombre, Apellido, Correo, Telefono, Foto  from Medicos

select Id, Cedula, Nombre, Apellido, Correo, Telefono, Foto  from Medicos where Id=@id

select max(Id) as Id from Medicos

alter table Medicos add default 'No hay foto' for Foto -- Mane ejecuta esto

truncate table Medicos;




create table Pruebas_Laboratorio
(

	Id int identity primary key,
	Nombre varchar(50)

)


-------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------


Insert into Pruebas_Laboratorio values('Prueba Orina');
Insert into Pruebas_Laboratorio values('Prueba Pecho');
Insert into Pruebas_Laboratorio values('Prueba Genitales');


select * from Pruebas_Laboratorio;

-------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------



insert into Pruebas_Laboratorio (Nombre) values(@nombre)

update Pruebas_Laboratorio set Nombre=@nombre where Id=@id

delete Pruebas_Laboratorio where Id=@id

select Id, Nombre from Pruebas_Laboratorio

select Id, Nombre from Pruebas_Laboratorio where Id=@id

create table Pacientes
(

	Id int identity primary key,
	Cedula varchar(50),
	Nombre varchar(50),
	Apellido varchar(50),
	Telefono varchar(50),
	Direccion varchar(50),
	Fecha_Nacimiento datetime,
	Fumador varchar(50),
	Alergias varchar(50),
	Foto varchar(50)

)




ALTER TABLE Citas
ADD FOREIGN KEY (Id_Paciente) REFERENCES Pacientes(Id);





insert into Pacientes (Cedula, Nombre, Apellido, Telefono, Direccion, Fecha_Nacimiento, Fumador, Alergias, Foto) values('2020', 'Eline', 'Castillo', '849', 'Pimentel','2020','Si', 'No', null)

update Pacientes set Cedula=@cedula, Nombre=@nombre, Apellido=@apellido, Telefono=@telefono, Direccion=@direccion, Fecha_Nacimiento=@nacimiento, Fumador=@fumador, Alergias=@alergias, Foto=@foto where Id=@id

update Pacientes set Foto='aaaaa' where Id = 3;

delete Pacientes where Id=@id

select Id, Cedula, Nombre, Apellido, Telefono, Direccion, Fecha_Nacimiento, Fumador, Alergias, Foto from Pacientes

select Id, Cedula, Nombre, Apellido, Telefono, Direccion, Fecha_Nacimiento, Fumador, Alergias, Foto from Pacientes where Id=3;

select max(Id) as Id from Pacientes

alter table Pacientes add default 'No hay foto' for Foto -- Mane ejecuta esto

drop table Pacientes;

create table Resultados_Laboratorio
(

	Id int identity primary key,
	Id_Paciente int,
	Id_Cita int,
	Id_PruebaLaboratorio int,
	Id_Medico int,
	Resultado_Prueba varchar(50),
	Estado_Resultado varchar(10) 

)

------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------



INSERT INTO Resultados_Laboratorio VALUES(1,1,1,1,'Positivo a Droga','Completado');
INSERT INTO Resultados_Laboratorio VALUES(2,2,2,2,'Positivo a Alcohol','Pendiente');
INSERT INTO Resultados_Laboratorio VALUES(3,3,3,3,'Positivo a Metanfetamina','Completado');
INSERT INTO Resultados_Laboratorio VALUES(4,4,1,4,'Negativa a Droga','Pendiente');



------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------





insert into Resultados_Laboratorio (Id_Paciente, Id_Cita, Id_PruebaLaboratorio, Id_Medico, Resultado_Prueba, Estado_Resultado) values(@resultado, @estado)

select rl.Id, p.Nombre, p.Apellido, p.Cedula, rl.Resultado_Prueba, rl.Estado_Resultado from Resultados_Laboratorio rl inner join Pacientes p on rl.Id_Paciente = p.Id

alter table Resultados_Laboratorio add default 'Pendiente' for Estado_Resultado -- Mane ejecuta esto

create table Citas
(

	Id int identity primary key,
	Id_Paciente int,
	Id_Medico int,
	Fecha_Cita datetime,
	Causa_Cita varchar(50),
	Estado_Cita varchar (50)

)
INSERT INTO Citas (Id_Paciente, Id_Medico, Fecha_Cita, Causa_Cita, Estado_Cita) values(4,4,11-12-2021,'Enfermo2','');

insert into Citas values (3,1,'11-11-2021','Gripe','');

select * from Citas;



SELECT Citas.Id, Pacientes.Nombre, Medicos.Nombre, Citas.Fecha_Cita, Citas.Causa_Cita, Citas.Estado_Cita
FROM ((Citas
INNER JOIN Pacientes ON Citas.Id_Paciente = Pacientes.Id)
INNER JOIN Medicos ON Citas.Id_Medico = Medicos.Id);




truncate table Citas;

insert into Citas (Id_Paciente, Id_Medico, Fecha_Cita, Causa_Cita, Estado_Cita) values(@paciente, @medico, @fecha, @causa, @estado)

insert into Citas values (1,1,);


drop table Citas




------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------




select * from Pruebas_Laboratorio;



select * from Resultados_Laboratorio;


select * from Medicos;

select * from Citas;



SELECT Citas.Id, Pacientes.Nombre as 'Paciente', Medicos.Nombre as 'Doctor', Citas.Fecha_Cita as 'Fecha y Hora de la Cita', Citas.Causa_Cita as 'Causa de la Cita', Citas.Estado_Cita as 'Estado de la Cita' FROM ((Citas INNER JOIN Pacientes ON Citas.Id_Paciente = Pacientes.Id) INNER JOIN Medicos ON Citas.Id_Medico = Medicos.Id);



SELECT Resultados_Laboratorio.Id, Pacientes.Nombre, Pacientes.Apellido, Pacientes.Cedula, Pruebas_Laboratorio.Nombre as 'Prueba'
FROM ((Resultados_Laboratorio
INNER JOIN Pacientes ON Resultados_Laboratorio.Id_Paciente = Pacientes.Id)
INNER JOIN Pruebas_Laboratorio ON Resultados_Laboratorio.Id_PruebaLaboratorio = Pruebas_Laboratorio.Id)
WHERE Estado_Resultado='Pendiente  de consulta';


Select * from Pacientes where Cedula like '%u%';


Select Cedula From Pacientes WHERE Cedula='u';


select * from Pacientes;








------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
alter table Citas add default 'Pendiente de consulta' for Estado_Cita -- Mane ejecuta esto




select Id_Paciente, Id_Cita, Id_PruebaLaboratorio, Id_Medico, Resultado_Prueba, Estado_Resultado from Resultados_Laboratorio where Cedula like '@cedula%' order by Cedula Desc

select Id_Paciente, Id_Medico, Fecha_Cita, Causa_Cita, Estado_Cita from Citas where Cedula like '@cedula%' order by Cedula Desc