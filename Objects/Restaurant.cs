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

    public static List<Restaurant> GetAll()
    {
      return null;
    }
  }
}
