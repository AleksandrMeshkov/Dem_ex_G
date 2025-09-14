using Gerber_demex.helpersClass;
using Gerber_demex.models;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
namespace Gerber_demex.page
{
    public partial class aplicationPage : Page
    {
        private tehnEntities context = new tehnEntities(); //экземпляр контекста 
        public aplicationPage()
        {
            InitializeComponent();
            LoadAplicationData();
            
        }

        private void LoadAplicationData()
        {

                var query = from pr in context.ProductRequest
                            select new
                            {
                                Type = pr.Partners.PartnerType.Name,
                                Name = pr.Partners.Name,
                                Address = pr.Partners.LegalAdresses,
                                Phone = pr.Partners.Phone,
                                Rating = pr.Partners.Rating,
                                Cost = pr.Count * pr.Product.Min_cost
                            };

                var result = query.ToList()
                    .Select(x => new
                    {
                        PartnerTypeName = x.Type + " | " + x.Name,
                        FullAddress = x.Address.Legal_index + ", " +
                                     x.Address.Region + ", " +
                                     x.Address.Locality + ", " + //запрос на вывод данных
                                     x.Address.Street + ", " +
                                     x.Address.House_number,
                        Phone = x.Phone,
                        RatingString = "Рейтинг: " + x.Rating,
                        TotalCostString = x.Cost.ToString("N2")
                    })
                    .ToList();// вывод в лист

                PartnersList.ItemsSource = result;
            
        }
    }
}