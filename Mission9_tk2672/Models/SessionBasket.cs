using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission9_tk2672.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission9_tk2672.Models
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket (IServiceProvider services)
        {
            // pull info about session
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // pull instance of session basket, go get json if there is some, make a new session basket if there isn't one
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();

            // update session information to be equal to the session we have
            basket.Session = session;

            // return basket to wherever called it
            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book book, int qty)
        {
            base.AddItem(book, qty);
            Session.SetJson("Basket", this);
        }

        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Basket", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }

    }
}
