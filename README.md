# ShopsRUs Discount Calculator API

## Overview

ShopsRUs is a retail outlet that wants to provide discounts to its customers through web/mobile platforms. This project is a .NET Core RESTful API that calculates discounts, generates total costs, and invoices for customers based on specified rules.

## Discounts

The following discounts apply:

1. If the user is an employee of the store, they get a 30% discount.
2. If the user is an affiliate of the store, they get a 10% discount.
3. If the user has been a customer for over 2 years, they get a 5% discount.
4. For every $100 on the bill, there is a $5 discount.
5. Percentage-based discounts do not apply to groceries.
6. A user can get only one of the percentage-based discounts on a bill.

## Project Structure

The project follows an object-oriented approach and is structured as follows:

- `DiscountCalculator`: Contains the logic to calculate discounts based on the given rules.
- `InvoiceGenerator`: Generates the final invoice amount including the discount.
- `Controllers`: API controllers for handling HTTP requests.
- `Models`: Data models used in the project.
- `Services`: Additional services to support the discount calculation and invoice generation.
- `Tests`: Unit tests using xUnit to ensure code correctness.

## Technologies Used

- .NET 6.0
- xUnit for unit testing
- Swagger for API documentation and testing

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/ShopsRUsDiscountCalculator.git
   ```

2. Build the project:

   ```bash
   dotnet build
   ```

3. Run the project:

   ```bash
   dotnet run
   ```

4. Navigate to the Swagger UI to test the API:

   ```bash
   https://localhost:5000/swagger/index.html
   ```

## Testing

The project uses xUnit for unit testing. To run the tests, navigate to the `Tests` folder and run the following command:

```bash
dotnet test
```

## API Endpoints

The following endpoints are available:

- `POST /api/discounts`: Calculates the discount for a given customer and bill amount.

**Request body:**

```json
{
  "products": [
    {
      "name": "Product 1",
      "price": 100,
      "category": 1
    },
    {
      "name": "Product 2",
      "price": 50,
      "category": 1
    },
    {
      "name": "Product 3",
      "price": 100,
      "category": 2
    }
  ],
  "customer": {
    "id": 1,
    "name": "John Doe",
    "isEmployee": false,
    "isAffiliate": true,
    "createdAt": "2023-12-13T09:09:49.702Z"
  }
}
```

**Response body:**

```json
{
  "products": [
    {
      "name": "Product 1",
      "price": 90,
      "category": 1
    },
    {
      "name": "Product 2",
      "price": 45,
      "category": 1
    },
    {
      "name": "Product 3",
      "price": 100,
      "category": 2
    }
  ],
  "customer": {
    "name": "John Doe"
  },
  "totalPriceBeforeDiscount": 250,
  "totalDiscount": 25,
  "finalAmount": 225
}
```
