using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace RestaurantReview.Objects
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _location;
    private string _description;
    private string _avgCost;
    private bool _delivery;
    private int _cuisineId;

    public Restaurant(string name, string location, string description, string avgCost, bool delivery, int cuisineId)
    {
      _name = name;
      _location = location;
      _description = description;
      _avgCost = avgCost;
      _delivery = delivery;
      _cuisineId = cuisineId;
    }

    public override bool Equals(System.Object restaurant)
    {
      if(!(restaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) restaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        return idEquality;
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
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
        string restaurantName = rdr.GetString(1);
        string restaurantLocation = rdr.GetString(2);
        string restaurantDescription = rdr.GetString(3);
        string restaurantAvgCost = rdr.GetString(4);
        bool restaurantDelivery = rdr.GetBoolean(5);
        int restaurantCusineId = rdr.GetInt32(6);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantLocation, restaurantDescription, restaurantAvgCost, restaurantDelivery, restaurantCusineId);
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
