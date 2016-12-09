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

      Get["/cuisine/edit/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_edit.cshtml", SelectedCuisine];
      };

      Patch["cuisine/edit/{id}"] = parameters => {
      Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
      SelectedCuisine.Update(Request.Form["cuisine-name"]);
      return View["success.cshtml"];
    };

      Get["/cuisines/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedCuisine = Cuisine.Find(parameters.id);
        var CuisineRestaurants = SelectedCuisine.GetRestaurants();
        model.Add("cuisine", SelectedCuisine);
        model.Add("restaurants", CuisineRestaurants);
        return View["cuisine.cshtml", model];
      };

      Get["/restaurants"] = _ => {
        List<Restaurant> AllRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", AllRestaurants];
      };

      Get["/restaurants/new"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["restaurants_form.cshtml", AllCuisines];
      };

      Post["/restaurants/new"] = _ => {
        bool delivery = Request.Form["delivery"];
        Console.WriteLine(delivery);
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-location"], Request.Form["restaurant-description"], Request.Form["avg-cost"], delivery, Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["success.cshtml"];
      };

      Post["/restaurants/delete"] = _ => {
        Restaurant.DeleteAll();
        return View["cleared.cshtml"];
      };
    }
  }
}
