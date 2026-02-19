C:\Users\mmoreira>docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=TuPasswordSegura123!" -p 1435:1433 --name sqlserver-hosp -v sqlserver_data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest
ee61ed1e93c72a347cd0e7a5408048b5a86597a26cfadca818a66359e0c2f684


create database hospital;


create table Medico(
id int primary key identity(1,1),
nombre varchar(100) not null,
telefono varchar(20) not null,
especialidad varchar(50) not null,
correo varchar(100)not null unique,
fechanNacimiento date not null,
fechaCreacion date default getDate()
);
create table Paciente(
id int primary key identity(1,1),
nombre varchar(100) not null,
telefono varchar(20) not null,
correo varchar(100)not null unique,
idMedico int,
fechaCreacion date default getDate(),
constraint FK_paciente_medico foreign key (idMedico) references Medico (id)
);
