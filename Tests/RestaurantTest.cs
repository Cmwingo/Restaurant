using System.Collections.Generic;
using System;
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

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_SavesRestaurantToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Rae's", "Northwest", "Casual family", "$", true, 1);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Restaurant testRestaurant = new Restaurant("Rae's", "Northwest", "Casual family", "$", true, 1);
      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      Restaurant testRestaurant = new Restaurant("Rae's", "Northwest", "Casual family", "$", true, 1);
      testRestaurant.Save();

      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      Assert.Equal(testRestaurant, foundRestaurant);
    }
  }
}
