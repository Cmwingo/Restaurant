using System.Collections.Generic;
using System;
using RestaurantReview.Objects;
using Xunit;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantReview
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=RestaurantReview_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Restaurant.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualityMethodOverrideWorks()
    {
      //Arrange
      Restaurant firstRestaurant = new Restaurant("Rae's", "Northwest", "Casual family", "$", true, 1);
      Restaurant secondRestaurant = new Restaurant("Rae's", "Northwest", "Casual family", "$", true, 1);

      Assert.Equal(firstRestaurant, secondRestaurant);

    }

    public void Dispose()
    {

    }
  }
}
