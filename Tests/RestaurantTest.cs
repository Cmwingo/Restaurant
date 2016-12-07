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

    public void Dispose()
    {

    }
  }
}
