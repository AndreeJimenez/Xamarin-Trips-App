using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTrips.Models
{
    public class TripModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime TripDate { get; set; }
        public string Notes { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }

        public TripModel()
        {
        }

        public ObservableCollection<TripModel> GetAll(string connectionString)
        {
            ObservableCollection<TripModel> list = new ObservableCollection<TripModel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Trip";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {   
                            while (reader.Read())
                            {
                                list.Add(new TripModel
                                {
                                    ID = (int)reader["IDTrip"],
                                    Title = reader["Title"].ToString(),
                                    Rating = (int)reader["Rating"],
                                    Notes = reader["Notes"].ToString(),
                                    TripDate = (DateTime)reader["TripDate"],
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"],
                                    ImageUrl = reader["ImageUrl"].ToString()
                                });
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }

            /*return new ObservableCollection<TripModel>
            {
                new TripModel
                {
                    ID = 1,
                    Title = "Nueva York desde Api",
                    Rating = 4,
                    Notes = "Estatua de la libertad",
                    TripDate = new DateTime(2020, 2, 18),
                    Latitude = 23.45453675,
                    Longitude = -12.454666,
                    ImageUrl = "https://larepublica.es/wp-content/uploads/2017/06/Captura-de-pantalla-2017-06-21-a-las-14.12.05.jpg"
                },
                new TripModel
                {
                    ID = 2,
                    Title = "Paris",
                    Rating = 5,
                    Notes = "Torre Eiffel",
                    TripDate = new DateTime(2019, 12, 24),
                    Latitude = 31.3556537,
                    Longitude = -16.354666,
                    ImageUrl = "https://i.ytimg.com/vi/AlnmjcIzdOU/maxresdefault.jpg"
                },
                new TripModel
                {
                    ID = 3,
                    Title = "Checoslovaquia",
                    Rating = 3,
                    Notes = "Checoslovacos",
                    TripDate = new DateTime(2017, 5, 10),
                    Latitude = 19.34665653,
                    Longitude = -10.565657890,
                    ImageUrl = "https://i.blogs.es/56ded0/ral128531876.800x600w/original.jpg"
                },
                new TripModel
                {
                    ID = 4,
                    Title = "Sin foto",
                    Rating = 3,
                    Notes = "No tengo la foto todavia",
                    TripDate = new DateTime(2017, 5, 10),
                    Latitude = 19.34665653,
                    Longitude = -10.565657890,
                    ImageUrl = ""
                }
            };*/
        }

        public TripModel Get(string connectionString, int id)
        {
            TripModel obj = new TripModel();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Trip WHERE IDTrip = @IDTrip";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDTrip", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                obj = new TripModel
                                {
                                    ID = (int)reader["IDTrip"],
                                    Title = reader["Title"].ToString(),
                                    Rating = (int)reader["Rating"],
                                    Notes = reader["Notes"].ToString(),
                                    TripDate = (DateTime)reader["TripDate"],
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"],
                                    ImageUrl = reader["ImageUrl"].ToString()
                                };
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResponse Insert(string connectionString)
        {
            try
            {
                object newID;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "INSERT INTO Trip " +
                                    "(" +
                                        "Title, " +
                                        "Rating, " +
                                        "Notes, " +
                                        "TripDate, " +
                                        "Latitude, " +
                                        "Longitude, " +
                                        "ImageUrl " +
                                    ")" +
                                    "VALUES " +
                                    "(" +
                                        "@Title, " +
                                        "@Rating, " +
                                        "@Notes, " +
                                        "@TripDate, " +
                                        "@Latitude, " +
                                        "@Longitude, " +
                                        "@ImageUrl " +
                                    "); " + 
                                    "SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Title", Title);
                        cmd.Parameters.AddWithValue("@Rating", Rating);
                        cmd.Parameters.AddWithValue("@Notes", Notes);
                        cmd.Parameters.AddWithValue("@TripDate", TripDate);
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                        cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                        newID = cmd.ExecuteScalar();
                    }
                }
                if (newID != null && newID.ToString().Length > 0)
                {
                    return new ApiResponse {
                        IsSuccess = true,
                        Result = int.Parse(newID.ToString()),
                        Message = "El viaje fue generado correctamente"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Result = 0,
                        Message = "Hubo un error al generar el viaje"
                    };
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
            }
        }

        public ApiResponse Update(string connectionString)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "UPDATE Trip SET " +
                                   "Title = @Title, " +
                                   "Rating = @Rating, " +
                                   "Notes = @Notes, " +
                                   "TripDate = @TripDate, " +
                                   "Latitude = @Latitude, " +
                                   "Longitude = @Longitude, " +
                                   "ImageUrl = @ImageUrl " +
                                   "WHERE IDTrip = @IDTrip";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Title", Title);
                        cmd.Parameters.AddWithValue("@Rating", Rating);
                        cmd.Parameters.AddWithValue("@Notes", Notes);
                        cmd.Parameters.AddWithValue("@TripDate", TripDate);
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                        cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                        cmd.Parameters.AddWithValue("@IDTrip", ID);
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = ID,
                    Message = "El viaje fue actualizado correctamente"
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
            }
        }

        public ApiResponse Delete(string connectionString, int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "DELETE FROM Trip " +
                                   "WHERE IDTrip = @IDTrip";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@IDTrip", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = id,
                    Message = "El viaje fue eliminado correctamente"
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
            }
        }

    }
}
