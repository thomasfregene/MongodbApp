using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongodbApp.Contracts;
using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Services
{
    public class ProductArrService : IProductArrService
    {
        private readonly IConfiguration _configuration;
        IMongoCollection<ProductArr> productarrCollection;
        public ProductArrService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("SampleConnection");
            var databaName = MongoUrl.Create(connectionString).DatabaseName;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaName);
            productarrCollection = database.GetCollection<ProductArr>("ProductArr");
        }

        public async Task<ResponseModel<bool>> Create(ProductArrDto productArrDto)
        {
            var productList = new List<ProductArr>
            {
                new ProductArr
                {
                    ProductCode = "001",
                    ProductName = "Product 001",
                    ProductVariantList = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Color = "Black",
                            Price = 20
                        },
                        new ProductVariant
                        {
                            Color = "White",
                            Price = 30
                        },
                        new ProductVariant
                        {
                            Color = "Blue",
                            Price = 40
                        }
                    }
                },
                new ProductArr
                {
                    ProductCode = "002",
                    ProductName = "Product 002",
                   ProductVariantList = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Color = "Yellow",
                            Price = 20
                        },
                        new ProductVariant
                        {
                            Color = "Purple",
                            Price = 30
                        }
                    }
                }
            };

            var product = new ProductArr
            {
                ProductCode = productArrDto.ProductCode,
                ProductName = productArrDto.ProductName,
                ProductVariantList = new List<ProductVariant>
                {
                    new ProductVariant
                    {
                        Color = "Yellow",
                        Price = 20
                    }
                }
            };

            await productarrCollection.InsertManyAsync(productList);
            //await productarrCollection.InsertOneAsync(product);

            return new ResponseModel<bool>
            {
                Code = "00",
                Data = true,
                Message = "Operation Successful"
            };
        }

        public async Task<ResponseModel<dynamic>> GetAll()
        {
            var filterDefinition = Builders<ProductArr>.Filter.Empty;
            var productList = await productarrCollection.Find(filterDefinition).ToListAsync();

            return new ResponseModel<dynamic>
            {
                Code = "00",
                Message = "Operation Succesful",
                Data = productList
            };
        }

        /*public async Task<ResponseModel<bool>> Update(ProductArrDto productArrDto)
        {
            var filterDefinition = Builders<ProductArr>.Filter.Eq(a => a.ProductId, "65917e12dc1ecf220ff94fef");
            var colors = new List<string> { "Orange", "Purple" };
            var updateDefiniton = Builders<ProductArr>.Update
                .Set(a => a.Colors, colors);

            await productarrCollection.UpdateOneAsync(filterDefinition, updateDefiniton);

            return new ResponseModel<bool>
            {
                Code = "00",
                Message = "Operation Successful",
                Data = true,
            };

        }*/

        public async Task<ResponseModel<dynamic>> UnWind()
        {
            var filterDefinition = Builders<ProductArr>.Filter.Eq(a => a.ProductCode, "001");
            var result = productarrCollection.Aggregate()
                //.Match(filterDefinition)
                .Unwind<ProductArr, ProductArrUnwindResult>(a => a.ProductVariantList)
                .ToEnumerable()
                .Select(a => new
                {
                    a.ProductId,
                    a.ProductCode,
                    a.ProductName,
                    a.ProductVariant.Color,
                    a.ProductVariant.Price
                })
                .ToList();

            var response = new ResponseModel<dynamic>
            {
                Code = "00",
                Message = "Operation Successful",
                Data = result
            };

            return  await Task.FromResult(response);

        }
    }
}
