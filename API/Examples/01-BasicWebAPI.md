# ASP.NET Web API 基礎範例

## 1. 建立基本 Controller

這是最基本的 Web API Controller 範例。

```csharp
// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "筆記型電腦", Price = 30000 },
        new Product { Id = 2, Name = "滑鼠", Price = 500 },
        new Product { Id = 3, Name = "鍵盤", Price = 1500 }
    };

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_products);
    }

    // GET: api/products/1
    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound(new { error = "找不到商品" });

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // PUT: api/products/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == id);

        if (existingProduct == null)
            return NotFound();

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;

        return NoContent();
    }

    // DELETE: api/products/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        _products.Remove(product);
        return NoContent();
    }
}
```

## 2. HTTP 狀態碼說明

```csharp
// 2xx 成功
return Ok(data);                    // 200 OK
return Created(uri, data);          // 201 Created
return CreatedAtAction(...);        // 201 Created
return NoContent();                 // 204 No Content

// 4xx 客戶端錯誤
return BadRequest(error);           // 400 Bad Request
return Unauthorized();              // 401 Unauthorized
return Forbidden();                 // 403 Forbidden
return NotFound(error);             // 404 Not Found

// 5xx 伺服器錯誤
return StatusCode(500, error);      // 500 Internal Server Error
```

## 3. 測試 API

### 使用 Swagger
開啟瀏覽器: `https://localhost:5001/swagger`

### 使用 curl
```bash
# GET 所有商品
curl https://localhost:5001/api/products

# GET 單一商品
curl https://localhost:5001/api/products/1

# POST 新增商品
curl -X POST https://localhost:5001/api/products \
  -H "Content-Type: application/json" \
  -d '{"name":"耳機","price":2000}'

# PUT 更新商品
curl -X PUT https://localhost:5001/api/products/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"筆電","price":35000}'

# DELETE 刪除商品
curl -X DELETE https://localhost:5001/api/products/1
```

### 使用 HTTP 檔案
```http
### GET 所有商品
GET https://localhost:5001/api/products

### GET 單一商品
GET https://localhost:5001/api/products/1

### POST 新增商品
POST https://localhost:5001/api/products
Content-Type: application/json

{
  "name": "耳機",
  "price": 2000
}

### PUT 更新商品
PUT https://localhost:5001/api/products/1
Content-Type: application/json

{
  "name": "筆電",
  "price": 35000
}

### DELETE 刪除商品
DELETE https://localhost:5001/api/products/1
```
