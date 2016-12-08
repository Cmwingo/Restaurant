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
    public string GetDescription()
    {
      return _description;
    }
    public string GetLocation()
    {
      return _location;
    }
    public string GetAvgCost()
    {
      return _avgCost;
    }
    public bool GetDelivery()
    {
      return _delivery;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
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
        bool restaurantDelivery = rdr.GetBoolean(rdr.GetOrdinal("delivery"));
        int restaurantCusineId = rdr.GetInt32(6);
        Console.WriteLine(restaurantId);
        Console.WriteLine(restaurantName);
        Console.WriteLine(restaurantLocation);
        Console.WriteLine(restaurantDescription);
        Console.WriteLine(restaurantAvgCost);
        Console.WriteLine(restaurantDelivery);
        Console.WriteLine(restaurantCusineId);


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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, location, description, avg_cost, delivery, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantLocation, @RestaurantDescription, @RestaurantAvgCost, @RestaurantDelivery, @RestaurantCuisineId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();

      SqlParameter locationParameter = new SqlParameter();
      locationParameter.ParameterName = "@RestaurantLocation";
      locationParameter.Value = this.GetLocation();

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@RestaurantDescription";
      descriptionParameter.Value = this.GetDescription();

      SqlParameter avgCostParameter = new SqlParameter();
      avgCostParameter.ParameterName = "@RestaurantAvgCost";
      avgCostParameter.Value = this.GetAvgCost();

      SqlParameter deliveryParameter = new SqlParameter();
      deliveryParameter.ParameterName = "@RestaurantDelivery";
      deliveryParameter.Value = this.GetDelivery();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(locationParameter);
      cmd.Parameters.Add(descriptionParameter);
      cmd.Parameters.Add(avgCostParameter);
      cmd.Parameters.Add(deliveryParameter);
      cmd.Parameters.Add(cuisineIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

  }
}
