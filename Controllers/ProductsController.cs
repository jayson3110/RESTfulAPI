using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RestfulAPIASP.Models;

namespace RestfulAPIASP.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: Products
        static readonly IProductRepository repository = new ProductRepository();

        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if(item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);

            return response;


        }


        public HttpResponseMessage PutProduct( Product product)
        {
                product = repository.Update(product);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, product);
                string uri = Url.Link("DefaultApi", new { id = product.Id });
                response.Headers.Location = new Uri(uri);

                return response;


        }
        public HttpResponseMessage DeleteUser(Product product)
        {
            product = repository.Remove(product);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, product);
            string uri = Url.Link("DefaultApi", new { id = product.Id });
            response.Headers.Location = new Uri(uri);

            return response;


        }






    }
}