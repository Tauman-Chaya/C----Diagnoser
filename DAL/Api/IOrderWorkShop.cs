using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IOrderWorkShop:Icrud<OrderWorkShop>
    {
        Diagnoser Diagnoser(OrderWorkShop orderWorkShop);
        Customer Customer(OrderWorkShop orderWorkShop);
        string Type(OrderWorkShop orderWorkShop);
    }
}
