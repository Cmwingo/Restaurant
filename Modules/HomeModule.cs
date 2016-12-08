using System.Collections.Generic;
using System;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace RestaurantReview
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["index.cshtml", AllCuisines];
      };

      Get["/cuisines"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", AllCuisines];
      };

      Get["/cuisines/new"] = _ => {
        return View["cuisines_form.cshtml"];
      };

      Post["/cuisines/new"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        return View["success.cshtml"];
      };

      Get["/restaurants"] = _ => {
        List<Restaurant> AllRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", AllRestaurants];
      };
    }
  }
}
