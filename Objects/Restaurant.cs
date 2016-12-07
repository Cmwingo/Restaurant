using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace RestaurantReview.Objects
{
  public class Restaurant
  {
    private int _id;
    private string _location;
    private string _description;
    private string _avgCost;
    private bool _delivery;
    private int _cuisineId;

    public Restaurant(string location, string description, string avgCost, bool delivery, int cuisineId)
    {
      _location = location;
      _description = description;
      _avgCost = avgCost;
      _delivery = delivery;
      _cuisineId = cuisineId;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> AllRestaurants = new List<Restaurant>();

      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantLocation = rdr.GetString(1);
        string restaurantDescription = rdr.GetString(2);
        string restaurantAvgCost = rdr.GetString(3);
        bool restaurantDelivery = rdr.GetBoolean(4);
        int restaurantCusineId = rdr.GetInt32(5);
        Restaurant newRestaurant = new Restaurant(restaurantLocation, restaurantDescription, restaurantAvgCost, restaurantDelivery, restaurantCusineId);
        AllRestaurants.Add(newRestaurant);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return AllRestaurants;
    }
  }
}
