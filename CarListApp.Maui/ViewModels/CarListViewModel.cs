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
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";
        
        string message = string.Empty;
        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel()
        {
            Title = "Car List";
            AddEditButtonText = createButtonText;
            
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int carId;

        [RelayCommand]
        public async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();

                var cars = new List<Car>();

                cars = App.CarService.GetCars();

                foreach (var car in cars) Cars.Add(car);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await ShowAlert("Failed to retrive list of cars.");
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
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await ShowAlert("Please insert valid data");
                return;
            }

            var car = new Car
            {
                Id = CarId,
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            if (CarId != 0)
            {
                
                App.CarService.UpdateCar(car);
                message = App.CarService.StatusMessage;
            }
            else
            {
                App.CarService.AddCar(car);
                message = App.CarService.StatusMessage;
            }

            await ShowAlert(message);
            await GetCarList();
            await ClearForm();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await ShowAlert("Please try again");
                return;
            }
            App.CarService.DeleteCar(id);
            message = App.CarService.StatusMessage;
            await ShowAlert(message);
            await GetCarList();
        }

        [RelayCommand]
        async Task UpdateCar(int id)
        {
            AddEditButtonText = editButtonText;
            await ShowAlert(message);
            await GetCarList();
            return;
            
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;
            Car car;
            car=App.CarService.GetCar(CarId);
            Make = car.Make;
            Model = car.Model;
            Vin = car.Vin;
            await ShowAlert(message);
            await GetCarList();
        }

        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
            await ShowAlert(message);
            await GetCarList();
        }

        private static async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }
    }
}