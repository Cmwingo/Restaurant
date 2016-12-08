using Xunit;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace RestaurantReview
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=RestaurantReview_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisinesEmptyAtFirst()
    {
      //Arrange
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Cuisine testCuisine1 = new Cuisine("Mexican");
      Cuisine testCuisine2 = new Cuisine("Mexican");

      Assert.Equal(testCuisine1, testCuisine2);
    }

    [Fact]
    public void Test_Save_SavesCuisineToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Mexican");

      //Act
      testCuisine.Save();
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine> {testCuisine};

      //Assert
      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      
    }
  }
}
