using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                ChangeTotal();
            }
        }

        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value <= 0) throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                ChangeTotal();
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                if (discount > 100) throw new ArgumentException();
                Notify(nameof(Discount));
                ChangeTotal();
            }
        }

        public double Total
        {
            get { return total; }
            set
            {
                total = value;
                if (total < 0) throw new ArgumentException();
                Notify(nameof(Total));
                ChangeDiscount();
            }
        }

        public void ChangeTotal()
        {
            total = price * nightsCount * (1 - discount / 100);
            Notify(nameof(Total));
        }

        public void ChangeDiscount()
        {
            discount = (total / (price * nightsCount) - 1) * -100;
            Notify(nameof(Discount));
        }
    }
}
