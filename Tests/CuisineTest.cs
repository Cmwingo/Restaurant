using Xunit;
using System.Data;
using System.Data.SqlClient;
using System;

namespace RestaurantReview
{
  public class CuisineTest
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
  }
}
