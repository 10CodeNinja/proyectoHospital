C:\Users\mmoreira>docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=TuPasswordSegura123!" -p 1433:1433 --name sqlserver-hosp -v sqlserver_data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest
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


no funciona en swagger, pero en postman funciona bien el lector de pacientes por medio de archivo csv










"ConnectionStrings": {
  "CadenaSQL": "Server=localhost,1433;Database=EcuPaquetesDB;User Id=sa;Password=TuPasswordSegura123!;TrustServerCertificate=True;"
}


string cadena = ConfigurationManager.ConnectionStrings["CadenaSQL"].ConnectionString;

    public bool GuardarPaquete(double peso, string destino, string tipo) {
        try {
            using (SqlConnection cn = new SqlConnection(cadena)) {
                // 1. Preparamos el comando (puede ser SQL directo o un SP)
                SqlCommand cmd = new SqlCommand("INSERT INTO Paquetes (Peso, Destino, Tipo) VALUES (@peso, @dest, @tipo)", cn);
                
                // 2. Pasamos los parámetros (Seguridad contra SQL Injection)
                cmd.Parameters.AddWithValue("@peso", peso);
                cmd.Parameters.AddWithValue("@dest", destino);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.CommandType = CommandType.Text;

                // 3. Abrimos y ejecutamos
                cn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        } catch (Exception ex) {
            return false;
        }
    }





    [HttpPost]
public JsonResult Registrar(double peso, string destino, string tipo) {
    RecursoDatos rd = new RecursoDatos();
    bool inserto = rd.GuardarPaquete(peso, destino, tipo);

    if (inserto) {
        return Json(new { success = true, mensaje = "Guardado en Docker con éxito" });
    } else {
        return Json(new { success = false, mensaje = "Error al conectar con SQL" });
    }
}






public List<PaqueteViewModel> ListarPaquetes() {
    List<PaqueteViewModel> lista = new List<PaqueteViewModel>();

    using (SqlConnection cn = new SqlConnection(cadena)) {
        SqlCommand cmd = new SqlCommand("sp_ObtenerPaquetesConRuta", cn);
        cmd.CommandType = CommandType.StoredProcedure;

        try {
            cn.Open();
            // Usamos ExecuteReader para obtener filas
            using (SqlDataReader dr = cmd.ExecuteReader()) {
                while (dr.Read()) {
                    // Creamos el objeto y lo llenamos con lo que dice el "dr" (Data Reader)
                    lista.Add(new PaqueteViewModel {
                        Id = Convert.ToInt32(dr["Id"]),
                        Peso = Convert.ToDouble(dr["Peso"]),
                        Destino = dr["Destino"].ToString(),
                        NombreRuta = dr["NombreRuta"].ToString()
                    });
                }
            }
        } catch (Exception ex) {
            // Manejo de error
        }
    }
    return lista;
}
