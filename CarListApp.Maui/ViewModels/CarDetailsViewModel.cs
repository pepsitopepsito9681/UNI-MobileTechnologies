using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarListApp.Maui.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
    {

        

        public CarDetailsViewModel()
        {
            this.car = new Car();
        }


        [ObservableProperty]
        Car car;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        }

        public async Task GetCarData()
        {
            
                Car = App.CarService.GetCar(Id);
            await Task.Run(() => { });
        }
    }
}
