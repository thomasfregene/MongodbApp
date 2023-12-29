using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongodbApp.Contracts;
using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        IMongoCollection<Product> productCollection;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("MongodbConnection");
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            productCollection = database.GetCollection<Product>("product");
        }

       
        public async Task<ResponseModel<bool>> Insert(ProductDto productDto)
        {
            var response = new ResponseModel<bool>();
            if (productDto == null)
            {
                response.Code = "99";
                response.Message = "Invalid request";
                response.Data = false;
                return response;
            }
            var product = new Product
            {
                ProductCode = productDto.ProductCode,
                Price = productDto.Price,
                ProductName = productDto.ProductName,
            };

            await productCollection.InsertOneAsync(product);

            response.Message = "Operation Successful";
            response.Code = "00";
            response.Data = true;
            return response;
        }

        public async Task<ResponseModel<bool>> Update(ProductDto productDto)
        {
            var response = new ResponseModel<bool>();

            var filterDefinition = Builders<Product>.Filter.Eq(a => a.ProductCode, productDto.ProductCode);
            if (filterDefinition == null)
            {
                response.Code = "99";
                response.Message = $"Record with product code {productDto.ProductCode} was not found";
                response.Data = false;
                return response;
            }
            var updateDefinition = Builders<Product>.Update
                .Set(a => a.ProductName, productDto.ProductName)
                //.Set(a => a.Price, productDto.Price);
                //.Inc(a => a.Price, productDto.Price); //increase current value by specified amount 
                //.Mul(a => a.Price, productDto.Price); //multiple current value by specified amount 
                //.Max(a => a.Price, productDto.Price); //checks specified amount if greater than current value then update 
                .Min(a => a.Price, productDto.Price); //checks specified amount if less than current value then update 

            await productCollection.UpdateOneAsync(filterDefinition, updateDefinition);

            response.Code = "00";
            response.Data = true;
            response.Message = "Operation Successful";
            return response;

        }

        public async Task<ResponseModel<bool>> Delete(string productCode)
        {
            var response = new ResponseModel<bool>();

            var filterDefinition = Builders<Product>.Filter.Eq(a=>a.ProductCode, productCode);
            if (!filterDefinition == null) 
            {
                response.Code = "99";
                response.Message = $"Record with product code {productCode } was not found";
                response.Data = false;
                return response;
            }

            await productCollection.DeleteOneAsync(filterDefinition);

            response.Code = "00";
            response.Message = $"Operation Successful";
            response.Data = true;
            return response;
        }

        public async Task<ResponseModel<List<ProductResponseDto>>> GetAll()
        {
            var response = new ResponseModel<List<ProductResponseDto>>();
            var productCodeList = new List<string> { "101", "102" }; 

            //var filterDefiniton = Builders<Product>.Filter.Empty;
            //var filterDefiniton = Builders<Product>.Filter.Gte(a=>a.Price, 50);
            //var filterDefiniton = Builders<Product>.Filter.Lte(a=>a.Price, 50);
            //var filterDefiniton = Builders<Product>.Filter.Lt(a=>a.Price, 50);
            //var filterDefiniton = Builders<Product>.Filter.Gt(a=>a.Price, 50);
            //var filterDefiniton = Builders<Product>.Filter.In(a=>a.ProductCode, productCodeList);
            //var filterDefiniton = Builders<Product>.Filter.Nin(a=>a.ProductCode, productCodeList);
            //var filterDefiniton = Builders<Product>.Filter.Ne(a=>a.ProductCode, "102") & 
            //    Builders<Product>.Filter.Gt(a=> a.Price, 50);

            var filterDefiniton = Builders<Product>.Filter.Ne(a => a.ProductCode, "102") |
                Builders<Product>.Filter.Gt(a => a.Price, 50);
            var products = await productCollection.Find(filterDefiniton).ToListAsync();
            if (products.Count == 0)
            {
                response.Code = "99";
                response.Message = $"No Record Found";
                return response;
            }

            var productsList = new List<ProductResponseDto>();
            var prdlist = products.Select(a=>new ProductResponseDto
            {
                ProductId = a.ProductId,
                ProductCode = a.ProductCode,
                ProductName= a.ProductName,
                Price = a.Price,
            }).ToList();

            //foreach (var item in products)
            //{
            //    productsList.Add(new ProductResponseDto
            //    {
            //        ProductId = item.ProductId,
            //        ProductName = item.ProductName,
            //        Price = item.Price,
            //        ProductCode = item.ProductCode,
            //    });
            //}

            response.Code = "00";
            response.Message = $"Operation Successful";
            response.Data = prdlist;
            //response.Data = productsList;
            return Task.FromResult(response).Result;
        }

        public async Task<ResponseModel<bool>> BulkInsert(IFormFile file)
        {
            var response = new ResponseModel<bool>();

            var fileName = file.FileName;
            if (!fileName.Contains("csv"))
            {
                response.Code = "99";
                response.Message = $"Invalid file type";
                response.Data = false;
                return response;
            }
            var filePath = @"C:\Data\Products.csv";
            var csvFile = await File.ReadAllLinesAsync(filePath);
            var products = csvFile.Select(a => new Product
            {
                ProductName = a.Split(',')[0],
                ProductCode = a.Split(",")[1],
                Price = decimal.Parse(a.Split(",")[2]),
            }).ToList();
            await productCollection.InsertManyAsync(products);

            response.Code = "00";
            response.Message = $"Operation Successful";
            response.Data = true;
            return response;
        }

        public async Task<ResponseModel<bool>> BulkUpdate()
        {
            var response = new ResponseModel<bool>();
            var filterDefinition = Builders<Product>.Filter.Gte(a => a.Price, 90);
            var updateDefinition = Builders<Product>.Update
                .Set(a => a.Price, 300);

            await productCollection.UpdateManyAsync(filterDefinition, updateDefinition);

            response.Code = "00";
            response.Message = $"Operation Successful";
            response.Data = true;
            return response;
        }

        public async Task<ResponseModel<bool>> BulkDelete()
        {
            
            var filterDefinition = Builders<Product>.Filter.Eq(a => a.Price, 300);
            await productCollection.DeleteManyAsync(filterDefinition);
            return new ResponseModel<bool>
            {
                Code = "00",
                Message = "Operation Successful",
                Data = true
            };
        }

        public async Task<ResponseModel<bool>> Upsert(ProductDto productDto)
        {
            //var response = new ResponseModel<bool>();
            var filterDefinition = Builders<Product>.Filter.Eq(a => a.ProductCode, productDto.ProductCode);
            var updateDefinition = Builders<Product>.Update
                .Set(a => a.ProductName, productDto.ProductName)
                .Set(a => a.Price, productDto.Price);
            await productCollection.UpdateOneAsync(filterDefinition, updateDefinition, new UpdateOptions { IsUpsert = true});

            return new ResponseModel<bool>
            {
                Code = "00",
                Message = "Operation Successful",
                Data = true
            };
        }
    }
}
