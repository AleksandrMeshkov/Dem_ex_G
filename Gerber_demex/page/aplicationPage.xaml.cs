using Gerber_demex.helpersClass;
using System.Linq;
using System.Windows.Controls;

namespace Gerber_demex.page
{
    public partial class aplicationPage : Page
    {
        public aplicationPage()
        {
            InitializeComponent();
            LoadAplicationData();
        }

        private void LoadAplicationData()
        {
            using (var context = Helper.GetContext())
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
                                     x.Address.Locality + ", " +
                                     x.Address.Street + ", " +
                                     x.Address.House_number,
                        Phone = x.Phone,
                        RatingString = "Рейтинг: " + x.Rating,
                        TotalCostString = x.Cost.ToString("N2")
                    })
                    .ToList();

                PartnersList.ItemsSource = result;
            }
        }
    }
}