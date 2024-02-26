
﻿using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;


﻿using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;

using CarListApp.Maui.Models;
using CarListApp.Maui.Services;

using CarListApp.Maui.Views;




using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Text.Json;


using System.Text.Json;



using System.Threading.Tasks;


namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel:BaseViewModel
    {
        private readonly CarService carService;
        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel()
        {
            Title = "Car List";
            GetCarList().Wait();

        public CarListViewModel(CarService carService)
        {
            Title = "Car List";
            this.carService = carService;

        }

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;



        [RelayCommand]
        async Task GetCarList()
        {
            if (IsLoading)
            {
                return;
            }
            try
            {
                IsLoading = true;
                if (Cars.Any())
                {
                    Cars.Clear();
                }

                var cars=App.CarService.GetCars();


                var cars=App.CarService.GetCars();

                var cars=carService.GetCars();


                    foreach (var car in cars)
                    {
                        Cars.Add(car);
                    }







                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get cars: {0}", ex.Message);
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list of cars.", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }
        [RelayCommand]

        async Task GetCarDetails(int id)
        {
            if (id ==0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}",true);
        }
        [RelayCommand]
        async Task AddCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
                return;
            }
            var car = new Car
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            App.CarService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            await GetCarList();
        }
        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id ==0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            var result=App.CarService.DeleteCar(id);
            if (result==0)
            {
                await Shell.Current.DisplayAlert("Failed", "Please insert valid data", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
                await GetCarList();
            }
        }
        async Task UpdateCar(int id)
        {
            return;

        async Task GetCarDetails(Car car)
        {

            if (car == null) return;

            await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object> 
            {
                {nameof(Car), car}

            if(car == null) return;

            await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
            {
                {nameof(Car), car }

            });

        }
    }
}
