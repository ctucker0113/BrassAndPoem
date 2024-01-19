
//create a "products" variable here to include at least five Product instances. Give them appropriate ProductTypeIds.
using System.Diagnostics;

List<Product> products = new List<Product>
{
    new Product()
    {
        Name = "Flugelhorn",
        Price = 1800.00M,
        ProductTypeID = 1
    },

    new Product()
    {
        Name = "Euphonium",
        Price = 2900.00M,
        ProductTypeID = 1
    },

    new Product()
    {
        Name = "Mellophone",
        Price = 2200.00M,
        ProductTypeID = 1
    },

    new Product()
    {
        Name = "Leaves of Grass (Walt Whitman)",
        Price = 20.00M,
        ProductTypeID = 2
    },

    new Product()
    {
        Name = "Shakespeare's Sonnets",
        Price = 30.00M,
        ProductTypeID = 2
    }
};

//create a "productTypes" variable here with a List of ProductTypes, and add "Brass" and "Poem" types to the List.
List<ProductType> productTypes = new()
{
    new ProductType()
    {
        Title = "Brass",
        ID = 1
    },

    new ProductType()
    {
        Title = "Poem",
        ID = 2
    }
};

DisplayMenu();

void DisplayMenu()
{
    while (true)
    {
        Console.WriteLine(@"Welcome to Brass and Poem! Please buy something! We did not pick the hottest items to sell in 2024, and have no money!
Please choose an option below:
a.) Display All Products
b.) Delete a Product
c.) Add a New Product
d.) Update Product Properties
e.) Exit Program");

        string userMenuChoice = Console.ReadLine().Trim().ToLower();

        if (userMenuChoice == "a")
        {
            DisplayAllProducts(products, productTypes);
        }

        else if (userMenuChoice == "b")
        {
            DeleteProduct(products, productTypes);
        }

        else if (userMenuChoice == "c")
        {
            AddProduct(products, productTypes);
        }

        else if (userMenuChoice == "d")
        {
            UpdateProduct(products, productTypes);
        }

        else if (userMenuChoice == "e")
        {
            Console.WriteLine("You have chosen to leave this simulation, and re-enter the real world. Farewell!");
            break;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid Option. Please try again.");
        }
    }
}

void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("You selected 'Display All Products'");
    for (var i = 0; i < products.Count; i++)
    {
        var productType = productTypes.FirstOrDefault(pt => pt.ID == products[i].ProductTypeID);
        var productTypeName = productType.Title;
        Console.WriteLine($@"{i+1}. {products[i].Name}
    Price: ${products[i].Price}
    Product Type: {productTypeName}");
    }
}

void DeleteProduct(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine(@"You selected 'Delete a Product'. Enter the number of the item you would like to delete.");
    for (var i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i+1}. {products[i].Name}");
    }

    int userDeleteInput;
    // Prevent the user from entering a non-integer or an out-of-bounds integer
    while (!int.TryParse(Console.ReadLine(), out userDeleteInput) || userDeleteInput < 0 || userDeleteInput > products.Count)
    {
        Console.WriteLine($"Invalid input. Please enter a number between 0 and {products.Count - 1}.");
    }

    userDeleteInput--;
    products.RemoveAt(userDeleteInput);
}

void AddProduct(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("You selected 'Add a New Product'");

    // User Inputs for new product information
    Console.WriteLine("Please enter the new product's name");
    string productName = Console.ReadLine();
    Console.WriteLine("Please enter the product's price.");
    decimal productPrice;
    while (!decimal.TryParse(Console.ReadLine(), out productPrice) || productPrice < 0)
    {
        Console.WriteLine("Invalid input. Please enter a positive number.");
    }
    Console.WriteLine("Select the number of the corresponding product type");
    int productTypeID;
    for (var i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
    }
    while (!int.TryParse(Console.ReadLine(), out productTypeID) || productTypeID < 0 || productTypeID > productTypes.Count)
    {
        Console.WriteLine($"Invalid input. Please enter a positive number between 1 and {productTypes.Count}.");
    }

    // Create the new Product entity with the user input.
    Product newProduct = new()
    {
        Name = productName,
        Price = productPrice,
        ProductTypeID = productTypeID
    };

    products.Add(newProduct);
}

void UpdateProduct(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("You selected 'Update Product Properties' Please enter the number of the product you would like to update.");
    for (var i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }

    int userUpdateInput;
    // Prevent the user from entering a non-integer or an out-of-bounds integer
    while (!int.TryParse(Console.ReadLine(), out userUpdateInput) || userUpdateInput < 0 || userUpdateInput > products.Count)
    {
        Console.WriteLine($"Invalid input. Please enter a number between 0 and {products.Count - 1}.");
    }

    userUpdateInput--;
    Console.WriteLine($"You have selected {products[userUpdateInput].Name} to update.");

    // Update Product Name
    Console.WriteLine($"The name of the product is currently {products[userUpdateInput].Name}. To change it, type the new name below. To leave the name unchanged, press Enter.");
    string productName = Console.ReadLine();
    // If the user typed anything, make it the new name. Otherwise, continue on to the next item to update.
    if (!string.IsNullOrEmpty(productName))
    {
        products[userUpdateInput].Name = productName;
    }


    // Update Product Price
    Console.WriteLine($"The price of the product is currently listed as {products[userUpdateInput].Price}. To change, type the new price below. To leave it unchanged, press Enter.");
    decimal productPrice;
    string userUpdatedPriceInput = Console.ReadLine();
    if (!string.IsNullOrEmpty(userUpdatedPriceInput))
    {
        while (!decimal.TryParse(userUpdatedPriceInput, out productPrice) || productPrice < 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive number.");
            userUpdatedPriceInput = Console.ReadLine();
        }
        products[userUpdateInput].Price = productPrice;
    }

    // Update ProductType
    var currentProductType = productTypes.FirstOrDefault(pt => pt.ID == products[userUpdateInput].ProductTypeID);
    Console.WriteLine($"The product type is currently '{currentProductType.Title}'.");
    Console.WriteLine("Available Product Types:");
    for (var i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
    }
    Console.WriteLine("To change the product type, enter the corresponding number. To leave it unchanged, press Enter.");

    string userUpdatedTypeInput = Console.ReadLine();
    if (!string.IsNullOrEmpty(userUpdatedTypeInput))
    {
        int newProductTypeId;
        if (int.TryParse(userUpdatedTypeInput, out newProductTypeId) && newProductTypeId > 0 && newProductTypeId <= productTypes.Count)
        {
            products[userUpdateInput].ProductTypeID = productTypes[newProductTypeId - 1].ID;
        }
        else
        {
            Console.WriteLine("Invalid input. No changes made to the product type.");
        }
    }

}

// don't move or change this!
public partial class Program { }