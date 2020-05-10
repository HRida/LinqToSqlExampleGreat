using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LinqToSqlExampleGreat
{
    public partial class Form1 : Form
    {
        private int OrderPosition;
        public Form1()
        {
            InitializeComponent();
            OrderPosition = 0;
        }
        NorthWindDataContext dc = new NorthWindDataContext();
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Data.Linq.Table<Employee> emp = dc.GetTable<Employee>();
            dataGridView1.DataSource = emp;
        }

        private void shippersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Shipper> ship = dc.GetTable<Shipper>();
            dataGridView1.DataSource = ship;

        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Order> orders = dc.GetTable<Order>();
            dataGridView1.DataSource = orders;
        }

        private void employeeTerritoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<EmployeeTerritory> empTerrs = dc.GetTable<EmployeeTerritory>();
            dataGridView1.DataSource = empTerrs;
        }

        private void territoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Territory> empTerrs = dc.GetTable<Territory>();
            dataGridView1.DataSource = empTerrs;
        }

        private void regionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Region> regs = dc.GetTable<Region>();
            dataGridView1.DataSource = regs;
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Customer> cust = dc.GetTable<Customer>();
            dataGridView1.DataSource = cust;
        }

        private void customerDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<CustomerCustomerDemo> custdemo = dc.GetTable<CustomerCustomerDemo>();
            dataGridView1.DataSource = custdemo;
        }

        private void customerDemographicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<CustomerDemographic> custdemograph = dc.GetTable<CustomerDemographic>();
            dataGridView1.DataSource = custdemograph;
        }

        private void orderDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Order_Detail> ordDetails = dc.GetTable<Order_Detail>();
            dataGridView1.DataSource = ordDetails;
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Product> prods = dc.GetTable<Product>();
            dataGridView1.DataSource = prods;
        }

        private void supplierProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Supplier> prods = dc.GetTable<Supplier>();
            dataGridView1.DataSource = prods;
        }

        private void categoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.Linq.Table<Category> cats = dc.GetTable<Category>();
            dataGridView1.DataSource = cats;
        }

        private void insertOrUpdateCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var matchedCustomer = (from c in dc.GetTable<Customer>()
                                       where c.CustomerID == "AAAAA"
                                       select c).SingleOrDefault();

                if (matchedCustomer == null)
                {
                    try
                    {
                        // create new customer record since customer ID
                        // does not exist
                        System.Data.Linq.Table<Customer> customers = dc.GetTable<Customer>();
                        Customer cust = new Customer();

                        cust.CustomerID = "AAAAA";
                        cust.CompanyName = "BXSW";
                        cust.ContactName = "Mookie Carbunkle";
                        cust.ContactTitle = "Chieftain";
                        cust.Address = "122 North Main Street";
                        cust.City = "Wamucka";
                        cust.Region = "DC";
                        cust.PostalCode = "78888";
                        cust.Country = "USA";
                        cust.Phone = "244-233-8977";
                        cust.Fax = "244-438-2933";

                        customers.InsertOnSubmit(cust);
                        customers.Context.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    try
                    {
                        matchedCustomer.CompanyName = "BXSW";
                        matchedCustomer.ContactName = "Mookie Carbunkle";
                        matchedCustomer.ContactTitle = "Chieftain";
                        matchedCustomer.Address = "122 North Main Street";
                        matchedCustomer.City = "Wamucka";
                        matchedCustomer.Region = "DC";
                        matchedCustomer.PostalCode = "78888";
                        matchedCustomer.Country = "USA";
                        matchedCustomer.Phone = "244-233-8977";
                        matchedCustomer.Fax = "244-438-2933";

                        dc.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void deleteCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var matchedCustomer = (from c in dc.GetTable<Customer>()
                                       where c.CustomerID == "AAAAA"
                                       select c).SingleOrDefault();
                try
                {
                    dc.Customers.DeleteOnSubmit(matchedCustomer);
                    dc.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void employeesByHireDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Employee> emps = (from emp in dc.GetTable<Employee>()  // or dc.Employees
                                   orderby emp.HireDate ascending
                                   select emp).ToList<Employee>();

            dataGridView1.DataSource = emps;

        }

        private void ordersByIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Order> orders = (from ord in dc.GetTable<Order>()
                                  where (ord.OrderID == 10248)
                                  select ord).ToList<Order>();

            dataGridView1.DataSource = orders;

        }

        private void ordersAndDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<OrdersAndDetailsResult> oad = (from ords in dc.GetTable<Order>()
                                                join dets in dc.GetTable<Order_Detail>()
                                                    on ords.OrderID equals dets.OrderID
                                                orderby ords.CustomerID ascending
                                                select new OrdersAndDetailsResult
                                                {
                                                    CustomerID = ords.CustomerID,
                                                    OrderDate = ords.OrderDate,
                                                    RequiredDate = ords.RequiredDate,
                                                    ShipAddress = ords.ShipAddress,
                                                    ShipCity = ords.ShipCity,
                                                    ShipCountry = ords.ShipCountry,
                                                    ShipZip = ords.ShipPostalCode,
                                                    ShippedTo = ords.ShipName,
                                                    OrderID = ords.OrderID,
                                                    NameOfProduct = dets.Product.ProductName,
                                                    QtyPerUnit = dets.Product.QuantityPerUnit,
                                                    Price = dets.Product.UnitPrice,
                                                    QtyOrdered = dets.Quantity,
                                                    Discount = dets.Discount
                                                }
                    ).ToList<OrdersAndDetailsResult>();

            dataGridView1.DataSource = oad;

        }

        private void ordersAndDetailsEntityRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<OrderandPricingResult> opr = (from ords in dc.Orders			    // orders table
                                               from dets in ords.Order_Details	    // entity set in orders table
                                               select new OrderandPricingResult
                                               {
                                                   OrderID = ords.OrderID,
                                                   Company = ords.Customer.CompanyName,
                                                   OrderCountry = ords.Customer.Country,
                                                   ProductName = dets.Product.ProductName,
                                                   UnitPrice = dets.Product.UnitPrice,
                                                   UnitsOrder = dets.Quantity,
                                                   ShipperName = ords.Shipper.CompanyName,
                                                   SalesFirstName = ords.Employee.FirstName,
                                                   SalesLastName = ords.Employee.LastName,
                                                   SalesTitle = ords.Employee.Title
                                               }).ToList<OrderandPricingResult>();

            dataGridView1.DataSource = opr;
        }

        private void ordersAndDetailsByOrderIDEntityRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<OrderandPricingResult> opr = (from ords in dc.Orders			    // orders table
                                               from dets in ords.Order_Details	    // entity set in orders table
                                               where ords.OrderID == 10248
                                               select new OrderandPricingResult
                                               {
                                                   OrderID = ords.OrderID,
                                                   Company = ords.Customer.CompanyName,
                                                   OrderCountry = ords.Customer.Country,
                                                   ProductName = dets.Product.ProductName,
                                                   UnitPrice = dets.Product.UnitPrice,
                                                   UnitsOrder = dets.Quantity,
                                                   ShipperName = ords.Shipper.CompanyName,
                                                   SalesFirstName = ords.Employee.FirstName,
                                                   SalesLastName = ords.Employee.LastName,
                                                   SalesTitle = ords.Employee.Title
                                               }).ToList<OrderandPricingResult>();
            dataGridView1.DataSource = opr;
        }

        private void orderValueByOrderIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal? d = (from od in dc.GetTable<Order_Detail>()
                          where od.OrderID == 10248
                          select od.Product.UnitPrice * od.Quantity).Sum();// get the dollar value 

            string dollarValue = string.Format("{0:c}", d); // convert the decimal value to currency

            // display the dollar value
            MessageBox.Show("The total dollar value of order 10248 is " + dollarValue, "Order 10248 Value");

        }

        private void getTopFiveOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                // get the top five orders starting at the current position
                List<Order> ords = (from ord in dc.GetTable<Order>()
                                    orderby ord.OrderID ascending
                                    select ord).Skip(OrderPosition).Take(5).ToList<Order>();

                dataGridView1.DataSource = ords;

                // increment the formwide variable used tokeep track of the position within the 
                // list of orders
                OrderPosition += 5;

                // change the text in the menu strip item to show that it will retrieve the next
                // five values after the current position of th last value shown in the grid
                getTopFiveOrdersToolStripMenuItem.Text = "Get Next Five Orders";
            }
            catch
            {
                MessageBox.Show("Cannot increment an higher, starting list over.");
                OrderPosition = 0;
            }

        }

        private void employeeByIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee emp = (from e1 in dc.GetTable<Employee>()
                            where (e1.EmployeeID == 1)
                            select e1).SingleOrDefault<Employee>();

            StringBuilder sb = new StringBuilder();
            sb.Append("Employee 1: " + Environment.NewLine);
            sb.Append("Name: " + emp.FirstName + " " + emp.LastName + Environment.NewLine);
            sb.Append("Hire Date: " + emp.HireDate + Environment.NewLine);
            sb.Append("Home Phone: " + emp.HomePhone + Environment.NewLine);

            MessageBox.Show(sb.ToString(), "Employee ID Search");

        }

        private void orderByIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order ord = (from ord1 in dc.GetTable<Order>()
                         where (ord1.OrderID == 10248)
                         select ord1).SingleOrDefault<Order>();
            StringBuilder sb = new StringBuilder();
            sb.Append("Order: " + Environment.NewLine);
            sb.Append("Order ID: " + ord.OrderID + Environment.NewLine);
            sb.Append("Date Shipped: " + ord.ShippedDate + Environment.NewLine);
            sb.Append("Shipping Address: " + ord.ShipAddress + Environment.NewLine);
            sb.Append("         City: " + ord.ShipCity + Environment.NewLine);
            sb.Append("         Region: " + ord.ShipRegion + Environment.NewLine);
            sb.Append("         Country: " + ord.ShipCountry + Environment.NewLine);
            sb.Append("         Postal Code: " + ord.ShipPostalCode + Environment.NewLine);
            sb.Append("Shipping Name: " + ord.ShipName + Environment.NewLine);

            MessageBox.Show(sb.ToString(), "Shipping Information");

        }

        private void salesByYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime start = new DateTime(1990, 1, 1);
            DateTime end = new DateTime(2000, 1, 1);

            List<Sales_by_YearResult> result = dc.Sales_by_Year(start, end).ToList<Sales_by_YearResult>();
            dataGridView1.DataSource = result;

        }

        private void tenMostExpensiveProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Ten_Most_Expensive_ProductsResult> result = dc.Ten_Most_Expensive_Products().ToList<Ten_Most_Expensive_ProductsResult>();
            dataGridView1.DataSource = result;
        }


    }
    public class OrdersAndDetailsResult
    {
        public System.String CustomerID
        { get; set; }
        public System.Nullable<System.DateTime> OrderDate
        { get; set; }
        public System.Nullable<System.DateTime> RequiredDate
        { get; set; }
        public System.String ShipAddress
        { get; set; }
        public System.String ShipCity
        { get; set; }
        public System.String ShipCountry
        { get; set; }
        public System.String ShipZip
        { get; set; }
        public System.String ShippedTo
        { get; set; }
        public System.Int32 OrderID
        { get; set; }
        public System.String NameOfProduct
        { get; set; }
        public System.String QtyPerUnit
        { get; set; }
        public System.Nullable<System.Decimal> Price
        { get; set; }
        public System.Int16 QtyOrdered
        { get; set; }
        public System.Single Discount
        { get; set; }
    }

    public class OrderandPricingResult
    {
        public System.Int32 OrderID
        { get; set; }
        public System.String Company
        { get; set; }
        public System.String OrderCountry
        { get; set; }
        public System.String ProductName
        { get; set; }
        public System.Nullable<System.Decimal> UnitPrice
        { get; set; }
        public System.Int16 UnitsOrder
        { get; set; }
        public System.String ShipperName
        { get; set; }
        public System.String SalesFirstName
        { get; set; }
        public System.String SalesLastName
        { get; set; }
        public System.String SalesTitle
        { get; set; }
    }
}
